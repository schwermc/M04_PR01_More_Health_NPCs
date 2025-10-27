using System;
using UnityEngine;

public class RegenerateHealth : MonoBehaviour, IHealth
{
    [SerializeField] private int startingHealth = 100;
    [SerializeField] private int healHealth = 1;

    private int currentHealth;

    public event Action<float> OnHPPctChanged = delegate { };
    public event Action OnDied = delegate { };

    private void Start()
    {
        currentHealth = startingHealth;
    }

    private void FixedUpdate()
    {
        HealHealth();
    }

    public float CurrentHpPct
    {
        get { return (float)currentHealth / (float)startingHealth; }
    }

    public void TakeDamage(int amount)
    {
        if (amount <= 0)
            throw new ArgumentOutOfRangeException("Invalid Damage amount specified: " + amount);

        currentHealth -= amount;
        OnHPPctChanged(CurrentHpPct);

        if (CurrentHpPct <= 0)
            Die();
    }
    
    private void HealHealth()
    {
        if (healHealth < 0)
            healHealth *= -1;
        
        if (currentHealth < startingHealth)
        {
            currentHealth += healHealth;
            OnHPPctChanged(CurrentHpPct);
        }
    }

    private void Die()
    {
        OnDied();
        GameObject.Destroy(this.gameObject);
    }

}
