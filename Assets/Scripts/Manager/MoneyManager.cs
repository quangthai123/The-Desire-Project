using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{   
    public static MoneyManager instance;
    private int extinctPoint;
    private void Start()
    {
        extinctPoint = PlayerPrefs.GetInt("extinct");
    }
    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }
    public void IncreaseExtinctPoint(int point)
    {
        extinctPoint += point;
        PlayerPrefs.SetInt("extinct", extinctPoint);
    } 
    public void DecreaseExtinctPoint(int point)
    {
        extinctPoint -= point;
        PlayerPrefs.SetInt("extinct", extinctPoint);
    }
}
