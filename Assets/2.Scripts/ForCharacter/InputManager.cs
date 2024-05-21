using UnityEngine;
using Zenject;

namespace MyPetProject
{
    public class InputManager : MonoBehaviour
    {
        private IAttacker m_Attacker;
        private IMovementController m_MovementController;
        private IInput m_InputHandler;
        private ISkillController m_SkillController;
        private UserController m_UserController;
        private DimensionSwitcher m_DimensionSwitcher;

        private void Awake()
        {
            m_MovementController = GetComponent<MovementController>();
            m_InputHandler = GetComponent<IInput>();
            m_Attacker = GetComponent<IAttacker>();
            m_SkillController = GetComponent<ISkillController>();
            m_UserController = gameObject.GetComponentInChildren<UserController>();
            m_DimensionSwitcher = gameObject.GetComponent<DimensionSwitcher>();
        }
        private void Update()
        {
            if (m_InputHandler != null)
            {
                if (m_MovementController != null) {m_MovementController.frameInput = m_InputHandler.frameInput; }
                if (m_Attacker != null) { m_Attacker.frameInput = m_InputHandler.frameInput; }
                if (m_SkillController != null) { m_SkillController.frameInput = m_InputHandler.frameInput; }
                if (m_UserController != null) { m_UserController.frameInput = m_InputHandler.frameInput; }
                if (m_DimensionSwitcher != null) { m_DimensionSwitcher.frameInput = m_InputHandler.frameInput; }
            }
        }
    }
}