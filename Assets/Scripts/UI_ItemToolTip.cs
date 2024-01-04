using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_ItemToolTip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemTypeText;
    [SerializeField] private TextMeshProUGUI itemDescription;
    [SerializeField] private int defaultFontsize=32;

    void Start()
    {

    }

    public void ShowToolTip(ItemData_Equipment item)
    {
        if (item == null) return;
        itemNameText.text = item.name;
        itemTypeText.text = item.equipmentType.ToString();
        itemDescription.text = item.GetDescription();

        if(itemNameText.text.Length> 12 )
        {
            itemNameText.fontSize = itemTypeText.fontSize * .7f;
        }
        else
        {
            itemNameText.fontSize = 32;
        }
        gameObject.SetActive(true);
    }

    public void HideToolTip()
    {
        itemNameText.fontSize = defaultFontsize;
        gameObject.SetActive(false);
    }
}
