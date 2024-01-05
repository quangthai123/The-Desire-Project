using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flask : MonoBehaviour
{
    [SerializeField] private int flaskAddNum;
    void Start()
    {
        if (PlayerPrefs.GetInt("flaskModifiers") >= flaskAddNum)
        {
            Destroy(gameObject);
        }
    }
}
