using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MyPetProject
    {
    public class Attack : MonoBehaviour, IAttacker
{
    public bool DamageFromStats;
    public ScriptableStats stats;
    public float damage;
    public Collider2D attackCollider;
    public List<Effect> effects = new List<Effect>();
    public UnityEvent FastAttackActivated;
    public UnityEvent SlowAttackActivated;
    
    public FrameInput frameInput { get; set; } 

    private float attackDamage;
    private float _comboDamageModificator = 1;
    private void Awake()
    {
        if (DamageFromStats)
        {
            attackDamage = stats.Damage;
        }
        else attackDamage = damage;
        
    }

    void Update()
    {
        if (frameInput.FastAttack)
        {
            FastAttackActivated?.Invoke();
        }
        if(frameInput.SlowAttack)
        {
            SlowAttackActivated?.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerHealth>())
        {
            float finalAttackDamage = attackDamage * _comboDamageModificator;
            PlayerHealth health = collision.GetComponent<PlayerHealth>();
            health.TakeDamage(finalAttackDamage, "Player");
        }
        if (effects.Count > 0)
        {
            if (collision.GetComponent<EffectReceiver>())
            {
                EffectReceiver receiver = collision.GetComponent<EffectReceiver>();
                foreach (Effect effect in effects)
                {
                    receiver.AddEffect(effect);
                }
            }
        }
    }

    public void SetComboDamageModificator(float mod)
    {
        _comboDamageModificator = mod;
    }

    
}
public interface IAttacker
{
    public FrameInput frameInput { get; set; }
}
}
