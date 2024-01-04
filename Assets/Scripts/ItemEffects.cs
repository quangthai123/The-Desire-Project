using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New item data", menuName = "Data/Item Effect")]
public class ItemEffects : ScriptableObject
{
    public virtual void ExecuteEffect()
    {
        Debug.Log("Sos");
    }
}
