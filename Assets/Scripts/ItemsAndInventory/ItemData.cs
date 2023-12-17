using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
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
    public string itemId;

    protected StringBuilder sb=new StringBuilder();

    private void OnValidate()
    {
        string path=AssetDatabase.GetAssetPath(this);
        itemId = AssetDatabase.AssetPathToGUID(path);
    }

    public virtual string GetDescription()
    {
        return "";
    }
}
