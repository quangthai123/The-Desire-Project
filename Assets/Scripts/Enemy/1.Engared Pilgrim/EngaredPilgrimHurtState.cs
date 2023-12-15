using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngaredPilgrimHurtState : EnemyState
{
    Enemy_EngaredPilgrim enemy;
    public EngaredPilgrimHurtState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string animBoolName, Enemy_EngaredPilgrim enemy) : base(enemyBase, enemyStateMachine, animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Start()
    {
        base.Start();
    }
    public override void Exit()
    {
        base.Exit();
        finishAnim = false;
        enemy.beDamaged = false;
    }


    public override void Update()
    {
        base.Update();
        if (finishAnim)
        {
            enemyStateMachine.ChangeState(enemy.idleState);
        }
        if (!enemy.healthCanvas.enabled)
            enemyStateMachine.ChangeState(enemy.deathState);
    }
}
