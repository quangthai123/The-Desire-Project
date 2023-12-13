using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType
{
    Item1,
    Item2,
    Item3,
    Item4
}


[CreateAssetMenu(fileName = "New item Data", menuName = "Data/Equipment")]
public class ItemData_Equipment : ItemData
{
    public EquipmentType equipmentType;

    [Header("Major stats")]
    public int damage;
    public int mana;
    public int speed;
    public int flask;

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
            playerStats.DecreaseHealthInFlask(flask);
        }
        else
        {
            Debug.LogError("No GameObject with the 'Player' tag found.");
        }
    }

}
