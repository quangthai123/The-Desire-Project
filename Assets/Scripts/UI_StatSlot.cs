using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_StatSlot : MonoBehaviour
{
    [SerializeField] private string statName;
    [SerializeField] private StatType statType;
    [SerializeField] private TextMeshProUGUI statValueText;
    [SerializeField] private TextMeshProUGUI statNameText;

    private void OnValidate()
    {
        gameObject.name = "Stat - " + statName;
        if(statNameText!=null)
        {
            statNameText.text=statName;
        }
    }

    void Start()
    {
        UpdateStatValueUI();
    }

   public void UpdateStatValueUI()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        PlayerStats playerStats = players[0].GetComponent<PlayerStats>();
        if(playerStats!=null)
        {
            statValueText.text=playerStats.GetStat(statType).GetValue().ToString();
        }

    }
}
