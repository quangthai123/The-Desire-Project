using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngaredPilgrimDeathState : EnemyState
{
    private Enemy_EngaredPilgrim enemy;
    public EngaredPilgrimDeathState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string animBoolName, Enemy_EngaredPilgrim enemy) : base(enemyBase, enemyStateMachine, animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Start()
    {
        base.Start();
        enemy.colTrigger.enabled = false;
    }
    public override void Exit()
    {
        base.Exit();
        Debug.Log("Exit dead state!!!");
    }


    public override void Update()
    {
        base.Update();
        if(finishAnim)
            enemy.canDestroy = true;
    }
}
