using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New item Data",menuName ="Data/Item")]
public class ItemData : ScriptableObject
{
    public string name;
    public Sprite icon;
}
