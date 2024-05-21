using System;
using UnityEngine;
using UnityEngine.Events;

namespace MyPetProject
{

    public class MovementController : MonoBehaviour, IMovementController
    {
        [SerializeField] private ScriptableStats _stats;
        public Animator animator;
        private Rigidbody2D _rb;
        private CapsuleCollider2D _col;

        public FrameInput frameInput
        {
            get { return _frameInput; }
            set { _frameInput = value; }
        }

        private FrameInput _frameInput;
        private Vector2 _frameVelocity;
        private bool _cachedQueryStartInColliders;
        private float _time;

        // COLLISION
        public float wallCheckHeight;
        public float wallCheckDistance;
        public LayerMask platformLayer;
        private float _frameLeftGrounded = float.MinValue;
        private bool _grounded;
        private bool _hangingOnTheWall;

        // JUMP
        private bool _jumpToConsume;
        private bool _bufferedJumpUsable;
        private bool _endedJumpEarly;
        private bool _coyoteUsable;
        private float _timeJumpWasPressed;
        private bool HasBufferedJump => _bufferedJumpUsable && _time < _timeJumpWasPressed + _stats.JumpBuffer;
        private bool CanUseCoyote => _coyoteUsable && !_grounded && _time < _frameLeftGrounded + _stats.CoyoteTime;
        private int _maxJumps => _stats.MaxAmountJump;
        private int _jumps;

        [Tooltip("Вправо = 1, Влево = -1")]
        public float _currentDirection = 1;

        // Данные интерфейса
        public UnityEvent<bool, float> GroundedChanged;
        public UnityEvent Jumped { get; set; }

        private void Awake()
        {
            animator = GetComponent<Animator>();
            _rb = GetComponent<Rigidbody2D>();
            _col = GetComponent<CapsuleCollider2D>();
            _cachedQueryStartInColliders = Physics2D.queriesStartInColliders;
            _frameInput = new FrameInput();
        }

        private void Update()
        {
            _time += Time.deltaTime;

            AnimationCheck();
            GatherInput();
            FlipAnimation();
        }

        private void AnimationCheck()
        {
            animator.SetBool("isHangingOnTheWall", _hangingOnTheWall);
            animator.SetBool("isGrounded", _grounded);
            animator.SetFloat("MoveHorizontal", Math.Abs(_frameInput.Move.x));
            animator.SetFloat("MoveVertical", _frameInput.Move.y);
            animator.SetFloat("VerticalVelocity", _rb.velocity.y);
            if (_frameInput.Move.y < 1)
            { animator.SetBool("PrisedTrigger", true); }
            else { animator.SetBool("PrisedTrigger", false); }
            if (_frameInput.JumpDown) { animator.SetTrigger("JumpDown"); }
        }
        private void GatherInput()
        {
            if (_stats.SnapInput)
            {
                _frameInput.Move.x = Mathf.Abs(_frameInput.Move.x) < _stats.HorizontalDeadZoneThreshold
                    ? 0
                    : Mathf.Sign(_frameInput.Move.x);
                _frameInput.Move.y = Mathf.Abs(_frameInput.Move.y) < _stats.VerticalDeadZoneThreshold
                    ? 0
                    : Mathf.Sign(_frameInput.Move.y);
            }

            if (_frameInput.JumpDown)
            {
                _jumpToConsume = true;
                _timeJumpWasPressed = _time;
            }
        }
        private void FixedUpdate()
        {
            CheckCollision();
            WallCheckCollision();
            HandleJump();
            HandleDirection();
            HandleGravity();
            ApplyMovement();
        }

        private void CheckCollision()
        {
            Physics2D.queriesStartInColliders = false;

            bool groundHit = Physics2D.CapsuleCast(_col.bounds.center, _col.size, _col.direction, 0, Vector2.down,
                _stats.GrounderDistance, ~_stats.PlayerLayer);
            bool ceilingHit = Physics2D.CapsuleCast(_col.bounds.center, _col.size, _col.direction, 0, Vector2.up,
                _stats.GrounderDistance, ~_stats.PlayerLayer);

            if (ceilingHit) _frameVelocity.y = Mathf.Min(0, _frameVelocity.y);

            if (!_grounded && groundHit)
            {
                _jumps = _maxJumps;
                _grounded = true;
                _coyoteUsable = true;
                _bufferedJumpUsable = true;
                _endedJumpEarly = false;
                GroundedChanged?.Invoke(true, Mathf.Abs(_frameVelocity.y));
            }


            else if (_grounded && !groundHit)
            {
                _grounded = false;
                _frameLeftGrounded = _time;
                GroundedChanged?.Invoke(false, 0);
            }
            Physics2D.queriesStartInColliders = _cachedQueryStartInColliders;
        }

        private float WallCheckCollision()
        {
            Vector3 height = new Vector3(0, wallCheckHeight, 0);
            bool leftWallHit = Physics2D.Raycast(((_col.bounds.center + new Vector3(0, _col.bounds.size.y / 2, 0)) - height), Vector2.left, wallCheckDistance, platformLayer);
            bool rightWallHit = Physics2D.Raycast(((_col.bounds.center + new Vector3(0, _col.bounds.size.y / 2, 0)) - height), Vector2.right, wallCheckDistance, platformLayer);

            bool leftLedgeHit = Physics2D.Raycast((_col.bounds.center + new Vector3(0, _col.bounds.size.y / 2, 0)), Vector2.left, wallCheckDistance, platformLayer);
            bool rightLedgeHit = Physics2D.Raycast((_col.bounds.center + new Vector3(0, _col.bounds.size.y / 2, 0)), Vector2.right, wallCheckDistance, platformLayer);

            if ((leftWallHit || rightWallHit) && !_grounded && (leftLedgeHit || rightLedgeHit))
            {
                _hangingOnTheWall = true;
                _jumps = _maxJumps;
                if (leftWallHit) return 1f;
                else return -1f;
            }
            else
            {
                _hangingOnTheWall = false;
            }


            return 0f;
        }

        private void HandleJump()
        {
            if (!_endedJumpEarly && !_grounded && !_frameInput.JumpHeld && _rb.velocity.y > 0) _endedJumpEarly = true;

            if (!_jumpToConsume && !HasBufferedJump) return;

            if (_grounded || _jumps != 0 || CanUseCoyote) ExecuteJump();

            _jumpToConsume = false;
        }

        private void ExecuteJump()
        {
            _jumps--;
            _endedJumpEarly = false;
            _timeJumpWasPressed = 0;
            _bufferedJumpUsable = false;
            _coyoteUsable = false;
            _frameVelocity.y = _stats.JumpPower;
            if (_hangingOnTheWall) _frameVelocity.x = _stats.HorizontalJumpAfterHanging * WallCheckCollision();
            Jumped?.Invoke();
        }

        private void HandleDirection()
        {
            if (_frameInput.Move.x == 0)
            {
                var deceleration = _grounded ? _stats.GroundDeceleration : _stats.AirDeceleration; _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x, 0, deceleration * Time.fixedDeltaTime);
            }
            else
            {

                _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x, _frameInput.Move.x * _stats.MaxSpeed, _stats.Acceleration * Time.fixedDeltaTime);
            }
        }
        private void HandleGravity()
        {
            if (_grounded && _frameVelocity.y <= 0f) // Гравитация на земле
            {
                _frameVelocity.y = _stats.GroundingForce;
            }
            else // Гравитация в воздухе
            {
                float inAirGravity;
                if (_hangingOnTheWall) inAirGravity = _stats.wallFallAcceleration;
                else inAirGravity = _stats.FallAcceleration;

                if (_endedJumpEarly && _frameVelocity.y > 0) inAirGravity *= _stats.JumpEndEarlyGravityModifier;
                _frameVelocity.y = Mathf.MoveTowards(_frameVelocity.y, -_stats.MaxFallSpeed, inAirGravity * Time.fixedDeltaTime);
            }
        }


        private void ApplyMovement()
        {
            if (animator.GetBool("DontMove")) { _rb.velocity = new Vector2(0, 0); return; }
            _rb.velocity = _frameVelocity;
        }

        private void FlipAnimation()
        {
            if (_frameVelocity.x > 0)
            {
                gameObject.transform.localScale = new Vector3(1, 1, 1);
                _currentDirection = 1;
            }

            if (_frameVelocity.x < 0)
            {
                gameObject.transform.localScale = new Vector3(-1, 1, 1);
                _currentDirection = -1;
            }
        }


#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_stats == null)
                Debug.Log("Please assign a ScriptableStats asset to the Player Controller's Stats slot", this);
        }
#endif
    }
    public interface IMovementController
    {
        public FrameInput frameInput { get; set; }
        public UnityEvent Jumped { get; set; }
    }
}



