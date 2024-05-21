using UnityEngine;

namespace MyPetProject
{
    public abstract class AIController : MonoBehaviour
    {
        [SerializeField] private AIState _currentState;
        [HideInInspector] public bool jumpDown;
        [HideInInspector] public bool jumpHeld;
        [HideInInspector] public float horizontal;
        [HideInInspector] public float vertical;
        [HideInInspector] public bool dashDown;
        [HideInInspector] public bool firstSkillDown;
        [HideInInspector] public bool secondSkillDown;
        [HideInInspector] public bool fastAttack;
        [HideInInspector] public bool slowAttack;
        [HideInInspector] public FrameInput aiFrameInput;

        //ИИ контроллер передает целую структуру данных скрипту Character КАЖДЫЙ КАДР. Является ли это целесообразным? Проверь.

        protected void Update()
        {
            if (_currentState == null)
            {
                ResetParametres();

                FindNewState();
            }

            AiGatherInput();
            _currentState.Play();
        }
        protected void AiGatherInput()
        {
            aiFrameInput = new FrameInput
            {
                JumpDown = jumpDown,
                JumpHeld = jumpHeld,
                Move = new Vector2(horizontal, vertical),
                DashDown = dashDown,
                FirstSkill = firstSkillDown,
                SecondSkill = secondSkillDown,
                FastAttack = fastAttack,
                SlowAttack = slowAttack,
            };
        }
        protected void SetState(AIState state)
        {
            state.AI = this;
            _currentState = state;
            state.Init();
        }

        public AIState GetCurrentState()
        {
            return _currentState;
        }

        protected void ResetParametres()
        {
            jumpDown = false;
            jumpHeld = false;
            horizontal = 0;
            vertical = 0;
            dashDown = false;
            firstSkillDown = false;
            secondSkillDown = false;
            fastAttack = false;
            slowAttack = false;
        }


        [Tooltip("Здесь должна будет описана логика персонажа. Для смены состояние используй SetState")]
        public abstract void FindNewState();

        public void Jump()
        {

        }
    }
}

