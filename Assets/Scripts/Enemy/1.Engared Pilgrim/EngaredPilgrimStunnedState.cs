using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngaredPilgrimStunnedState : EnemyState
{
    private Enemy_EngaredPilgrim enemy;
    public EngaredPilgrimStunnedState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string animBoolName, Enemy_EngaredPilgrim enemy) : base(enemyBase, enemyStateMachine, animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Start()
    {
        base.Start();
        stateDurationCounter = 1f;
        enemy.effect.RedColorBlinkFx();
    }
    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (stateDurationCounter < 0f)
            enemyStateMachine.ChangeState(enemy.idleState);
    }
}
