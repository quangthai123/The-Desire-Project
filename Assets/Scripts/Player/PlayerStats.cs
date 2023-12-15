using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum StatType
{
    hp,
    mana,
    damage,
    healthInFlask
}

public class PlayerStats : CharacterStats
{
    public Stat maxMana;
    public Stat currentMaxFlask;
    public Stat healthInFlask;
    public int currentMana;
    public int maxFlask;
    public int currentFlask;
    private Player playerStates;

    public List<int> skillsChecker;
    public bool gotDamageData;


    protected override void Start()
    {
        base.Start();
        playerStates = GetComponent<Player>();
        currentMana = 0;
        currentFlask = currentMaxFlask.GetValue();
    }
    private void Update()
    {
        // lay du lieu skills tu playerprefs
        for (int i = 0; i < skillsChecker.Count; i++)
        {
            skillsChecker[i] = PlayerPrefs.GetInt("Skill" + i.ToString());
        }
        // lay du lieu damage tu skill checker
        if (!gotDamageData)
        {
            for (int i = 0; i < skillsChecker.Count; i++)
            {
                if (skillsChecker[i] == 1)
                    damage.modifiers[i] = i + 1;
            }
            gotDamageData = true;
        }
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
    public void DecreaseHealthInFlask(int heal)
    {
        healthInFlask.RemoveModifier(heal);
    }
    public void IncreaseDamage(int damage1)
    {
        damage.AddModifier(damage1);
    }
    public void DecreaseDamage(int damage1)
    {
        damage.RemoveModifier(damage1);
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
        playerStates.stateMachine.ChangeState(playerStates.deathState);
    }
    public void ResetGameAfterDied()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public Stat GetStat(StatType _statType) {
        if (_statType == StatType.hp) return maxHealth;
        if (_statType == StatType.mana) return maxHealth;
        if (_statType == StatType.damage) return damage;
        if (_statType == StatType.healthInFlask) return healthInFlask;
        return null;
    }
}
