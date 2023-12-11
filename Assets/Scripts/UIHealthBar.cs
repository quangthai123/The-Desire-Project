using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    private Entity entity;
    private RectTransform rectTransform;
    private Slider slider;
    private EnemyStats enemyStats;
    private void Start()
    {
        entity = GetComponentInParent<Entity>();
        rectTransform = GetComponent<RectTransform>();
        slider = GetComponentInChildren<Slider>();
        enemyStats = GetComponentInParent<EnemyStats>();
        entity.onFlipped += FlipUI;
    }
    private void Update()
    {
        UpdateHealthUI();
        
    }
    private void FlipUI()
    {
        rectTransform.Rotate(0, 180f, 0);
    }
    private void OnDisable()
    {
        entity.onFlipped -= FlipUI;
    }
    private void UpdateHealthUI()
    {
        slider.maxValue = enemyStats.maxHealth.GetBaseValue();
        slider.value = enemyStats.currentHealth;
    }
}
