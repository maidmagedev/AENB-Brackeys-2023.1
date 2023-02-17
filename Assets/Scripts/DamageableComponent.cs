using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IKillable
{
    public void Die();
    public void NotifyDamage();
}

public class DamageableComponent : MonoBehaviour
{
    private int maxHealth = 100;
    private int currentHealth = 100;
    IKillable parent;
    bool dead = false;

    private void Start()
    {
        parent = GetComponentInParent<IKillable>();
    }

    public void SetMaxHealth(int maxHealth)
    {
        this.currentHealth = maxHealth;
        this.maxHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        this.currentHealth -= damage;
        if(currentHealth <= 0 && !dead)
        {
            this.dead = true;
            parent.Die(); 
        }
        else
        {
            parent.NotifyDamage();
        }
    }

    public void Heal(int healAmount)
    {
        currentHealth = Mathf.Min(currentHealth + healAmount, maxHealth);
    }

    public float GetHealthPercentage()
    {
        return (float)currentHealth/maxHealth;
    }

}
