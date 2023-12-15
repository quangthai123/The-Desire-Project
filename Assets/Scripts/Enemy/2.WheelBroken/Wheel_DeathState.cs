using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel_DeathState : EnemyState
{
    private Enemy_Wheel enemy;
    public Wheel_DeathState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string animBoolName, Enemy_Wheel enemy) : base(enemyBase, enemyStateMachine, animBoolName)
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
        if (finishAnim)
            enemy.canDestroy = true;
    }
}
