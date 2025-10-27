using System;
using UnityEngine;

public class LoseHealthOverTime : MonoBehaviour, IHealth
{
    [SerializeField] private int startingHealth = 100;
    [SerializeField] private int lostHealth = -1;

    private int currentHealth;
    private bool canTakeDamage = true;

    public event Action<float> OnHPPctChanged = delegate { };
    public event Action OnDied = delegate { };

    private void Start()
    {
        currentHealth = startingHealth;
    }

    private void FixedUpdate()
    {
        if (!canTakeDamage)
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

        if (canTakeDamage)
        {
            currentHealth -= amount;
            OnHPPctChanged(CurrentHpPct);
            canTakeDamage = false;
        }
    }
    
    private void HealHealth()
    {
        if (lostHealth > 0)
            lostHealth *= -1;
        
        currentHealth += lostHealth;
        OnHPPctChanged(CurrentHpPct);

        if (CurrentHpPct <= 0)
            Die();
    }

    private void Die()
    {
        OnDied();
        GameObject.Destroy(this.gameObject);
    }

}