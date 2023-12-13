using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Quest,
    Equipment
}

[CreateAssetMenu(fileName ="New item Data",menuName ="Data/Item")]
public class ItemData : ScriptableObject
{
    public ItemType itemType;
    public string name;
    public Sprite icon;
}
