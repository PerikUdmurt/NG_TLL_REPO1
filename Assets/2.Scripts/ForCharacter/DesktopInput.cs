using UnityEngine;

namespace MyPetProject
{
    public class DesktopInput : MonoBehaviour, IInput
    {
        public FrameInput frameInput { get; set; }
        private void Update()
        {
            frameInput = new FrameInput
            {
                JumpDown = Input.GetButtonDown("Jump"),
                JumpHeld = Input.GetButton("Jump"),
                Move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")),
                DashDown = Input.GetButtonDown("Dash"),
                FastAttack = Input.GetButtonDown("FastAttack"),
                SlowAttack = Input.GetButtonDown("SlowAttack"),
                FirstSkill = Input.GetButtonDown("FirstSkill"),
                SecondSkill = Input.GetButtonDown("SecondSkill"),
                UseButtonPressed = Input.GetButtonDown("Use"),
                DimensionSwitchPressed = Input.GetButton("DimensionSwitch"),
                DimensionSwitchUpped = Input.GetButtonUp("DimensionSwitch")
            };
        }
    }
    public struct FrameInput
    {
        public bool JumpDown;
        public bool JumpHeld;
        public Vector2 Move;
        public bool DashDown;
        public bool FirstSkill;
        public bool SecondSkill;
        public bool FastAttack;
        public bool SlowAttack;
        public bool UseButtonPressed;
        public bool DimensionSwitchPressed;
        public bool DimensionSwitchUpped;
    }
    public interface IInput
    {
        public FrameInput frameInput { get; set; }
    }
}