using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableSkills/Active/HealthPoition")]
public class HealthPoition : ActiveSkill
{
    public float healPercent;
    private PlayerHealth health;
    public override void Init()
    {
        health = skillController.GetComponent<PlayerHealth>();
    }

    public override void Activate()
    {
        if (health != null)
        {
            float healPoints = health.maxHealth * healPercent * 100;
            health.TakeHealth(healPoints);
        }
        else ActivateError("не найден компонент Health");
    }
}
