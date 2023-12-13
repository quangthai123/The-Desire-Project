using System.Collections;
using System.Collections.Generic;
using UnityEditor.Sequences;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : CharacterStats
{
    public Stat maxMana;
    public Stat currentMaxFlask;
    public Stat healthInFlask;
    public int currentMana;
    public int maxFlask;
    public int currentFlask;

    protected override void Start()
    {
        base.Start();
        currentMana = 0;
        currentFlask = currentMaxFlask.GetValue();
    }
    public void DecreaseManaFromSkills(int mana)
    {
        if (currentMana < mana)
        {
            // AudioManager (am thanh het mana)
            return;
        }
        else
            currentMana -= mana;
    }
    public void IncreaseManaFromAttack(int mana)
    {
        if (currentMana + mana > maxMana.GetValue())
        {
            currentMana = maxMana.GetValue();
        }
        else currentMana += mana;
    }
    public void IncreaseHealthInFlask(int heal)
    {
        healthInFlask.AddModifier(heal);
    }

    public void IncreaseDamage(int damageToAdd)
    {
        damage.AddModifier(damageToAdd);
    }

    public void DecreaseDamage(int damageToAdd)
    {
        damage.RemoveModifier(damageToAdd);
    }

    public void DecreaseHealthInFlask(int heal)
    {
        healthInFlask.RemoveModifier(heal);
    }
    public void Healing()
    {
        currentFlask--;
        if (currentHealth + healthInFlask.GetValue() > maxHealth.GetValue())
        {
            currentHealth = maxHealth.GetValue();
        }
        else currentHealth += healthInFlask.GetValue();
    }
    public void IncreaseMaxHealth(int healthToAdd)
    {
        maxHealth.AddModifier(healthToAdd);
        currentHealth = maxHealth.GetValue();
    }
    public void IncreaseMaxMana(int manaToAdd)
    {
        maxMana.AddModifier(manaToAdd);
    }
    public void DecreaseMaxMana(int manaToAdd)
    {
        maxMana.RemoveModifier(manaToAdd);
    }
    public void IncreaseMaxFlask(int flaskToAdd)
    {
        if (currentMaxFlask.GetValue() == maxFlask)
            return;
        currentMaxFlask.AddModifier(flaskToAdd);
        currentFlask = currentMaxFlask.GetValue();
    }

    public void HealingAll()
    {
        currentHealth = maxHealth.GetValue();
        currentMana = maxMana.GetValue();
        currentFlask = currentMaxFlask.GetValue();
    }
    protected override void Die()
    {
        base.Die();
            
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
