using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType
{
    Item1,
    Item2,
    Item3,
    Item4,
    Item5
}


[CreateAssetMenu(fileName = "New item Data", menuName = "Data/Equipment")]
public class ItemData_Equipment : ItemData
{
    public EquipmentType equipmentType;
    public ItemEffects[] ItemEffects;

    [Header("Major stats")]
    public int damage;
    public int mana;
    public int speed;
    public int flask;
    public int hp;

    private int minDescriptionLength;

    public void AddModifiers()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        if (players != null && players.Length > 0)
        {
            PlayerStats playerStats = players[0].GetComponent<PlayerStats>();
            Debug.Log(playerStats.damage);
            Player player = players[0].GetComponent<Player>();
            playerStats.IncreaseMaxMana(mana);
            
            playerStats.IncreaseDamage(damage);
            player.speed += speed;
            player.speed = Mathf.Max(0, player.speed);
            playerStats.IncreaseMaxHealth(hp);
            playerStats.IncreaseHealthInFlask(flask);
            
        }
        else
        {
            Debug.LogError("No GameObject with the 'Player' tag found.");
        }
    }


    public void RemoveModifiers()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        if (players != null && players.Length > 0)
        {
            PlayerStats playerStats = players[0].GetComponent<PlayerStats>();

            Player player = players[0].GetComponent<Player>();
            playerStats.DecreaseMaxMana(mana);
            playerStats.DecreaseDamage(damage);
            player.speed -= speed;
            player.speed = Mathf.Max(0, player.speed);
            playerStats.DecreaseMaxHealth(hp);

            playerStats.DecreaseHealthInFlask(flask);
        }
        else
        {
            Debug.LogError("No GameObject with the 'Player' tag found.");
        }
    }

    public void ItemEffect()
    {
        foreach(var item in ItemEffects)
        {
            item.ExecuteEffect();
        }
    }

    public override string GetDescription()
    {
        sb.Length = 0;
        minDescriptionLength = 0;
        AddItemDescription(damage, "Damage");
        AddItemDescription(mana, "Mana");
        AddItemDescription(speed, "Speed");
        AddItemDescription(flask, "Increase HP healing effect");
        if (minDescriptionLength < 5)
        {
            for(int i = 0; i < 5 - minDescriptionLength; i++)
            {
                sb.AppendLine();
                sb.Append("");
            }
        }
        return sb.ToString();
    }

    private void AddItemDescription(int _value, string _name)
    {
        if (_value != 0)
        {
            if (sb.Length > 0)
            {
                sb.AppendLine();
            }
            if (_value > 0)
            {
                sb.Append(_name + ":" + _value);
            }
            minDescriptionLength++;
        }
    }
}
