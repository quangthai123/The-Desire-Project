 using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public Stat damage;
    public Stat maxHealth;

    public int currentHealth;
    
    protected virtual void Start()
    {
        currentHealth = maxHealth.GetValue();
    }
    public virtual void TakeDamage(int _damage)
    {
        currentHealth -= _damage;
            
        if(currentHealth <= 0 )
        {
            Die();
        }
    }
    protected virtual void Die()
    {
        GetComponent<Entity>().isDead = true;
    }
}
