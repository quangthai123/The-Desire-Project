using System.Collections;
using System.Collections.Generic;
using System.Text;
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

    protected StringBuilder sb=new StringBuilder();

    public virtual string GetDescription()
    {
        return "";
    }
}
