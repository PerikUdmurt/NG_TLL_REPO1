using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/EffectType/Poison")]
public class PoisonEffect : Effect
{
    public float damagePerSecond;
    private PlayerHealth _health;
    public override void Init()
    {
        _health = effectReceiver.GetComponent<PlayerHealth>();
        Debug.Log("Init");
    }

    public override void Run()
    {
        Debug.Log("Run");
    }

    public override void End()
    {
        Debug.Log("End");
    }
    public override void EverySecondRun()
    {
        _health.TakeDamage(damagePerSecond, effectName);
    }
}
