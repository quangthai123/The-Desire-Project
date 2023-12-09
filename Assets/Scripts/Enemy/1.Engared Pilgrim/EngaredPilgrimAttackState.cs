using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngaredPilgrimAttackState : EnemyState
{
    private Enemy_EngaredPilgrim enemy;
    public EngaredPilgrimAttackState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string animBoolName, Enemy_EngaredPilgrim enemy) : base(enemyBase, enemyStateMachine, animBoolName)
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
    }

    public override void Update()
    {
        base.Update();
        if (finishAnim)
            enemyStateMachine.ChangeState(enemy.idleState);
    }
}
