using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFx : MonoBehaviour
{
    private SpriteRenderer sr;
    private Material originalMat;
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalMat = sr.material;
    }
    public void RedColorBlinkFx() 
    { 
        sr.color = Color.red;
        Invoke("ToggleToNormalColor", .2f);
    }
    private void ToggleToNormalColor() 
    {
        sr.color = Color.white;
    }
}
