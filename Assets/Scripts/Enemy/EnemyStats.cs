using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    [SerializeField] private int pointGetByThisEnemy;
    protected override void Die()
    {
        base.Die();
        MoneyManager.instance.IncreaseExtinctPoint(pointGetByThisEnemy);
    }
}
