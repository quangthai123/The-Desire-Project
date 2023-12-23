using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_StunnedState : EnemyState
{
    private Boss enemy;
    public Boss_StunnedState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string animBoolName, Boss enemy) : base(enemyBase, enemyStateMachine, animBoolName)
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
