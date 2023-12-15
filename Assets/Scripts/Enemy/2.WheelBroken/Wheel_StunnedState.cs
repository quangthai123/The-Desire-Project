using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel_StunnedState : EnemyState
{
    private Enemy_Wheel enemy;
    public Wheel_StunnedState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string animBoolName, Enemy_Wheel enemy) : base(enemyBase, enemyStateMachine, animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Start()
    {
        base.Start();
        stateDurationCounter = .75f;
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
        {
            enemy.effect.RedColorBlinkFx();
            enemyStateMachine.ChangeState(enemy.hurtState);
        }

    }
}
