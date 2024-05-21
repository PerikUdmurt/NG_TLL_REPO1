using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    //Здоровье
    public ScriptableStats _stats;
    private float currentHealth;

    public float CurrentHealth
    {
        get { return currentHealth; }
        set { 
            currentHealth = value;
            if (value >= maxHealth) { currentHealth = maxHealth; }
        }
    }
    public float maxHealth { get; private set; }
    public UnityEvent<float, float> Damaged;
    public UnityEvent<float, float> Healed;
    public UnityEvent Died;

    //Эффекты
    
    public bool immuneToBaseDamage;
    /*
    public bool immuneToFire;
    public bool immuneToIce;
    public bool immuneToElectricity;
    public bool immuneToPoison;
    */
    void Awake()
    {
        maxHealth = _stats.Health;
        currentHealth = _stats.Health;
    }
    public void TakeDamage(float damage)
    {
        TakeDamage(damage, "Unknown");
    }
   

    public void TakeDamage(float damage,string sourceName)
    {
        if (immuneToBaseDamage) { return; }
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            currentHealth = 0;
            Died?.Invoke();
        }
        Debug.Log("Получен урон " + damage + " от " + sourceName +". Здоровье равно " + currentHealth);
        Damaged?.Invoke(currentHealth, maxHealth);
    }

    public void TakeHealth(float healPoint)
    {
        currentHealth += healPoint;
        if (currentHealth > maxHealth) 
        { currentHealth = maxHealth; }
        Debug.Log($"Health {currentHealth}");
        Healed?.Invoke(currentHealth, maxHealth);
    }

    public void AddMaxHealthPoint(float extraHealthPoints)
    {
        maxHealth += extraHealthPoints;
    }
}
