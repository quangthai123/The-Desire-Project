using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_HurtState : EnemyState
{
    private Boss enemy;
    public Boss_HurtState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string animBoolName, Boss enemy) : base(enemyBase, enemyStateMachine, animBoolName)
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
        {
            enemyStateMachine.ChangeState(enemy.idleState);
        }
        if (enemy.isDead)
            enemyStateMachine.ChangeState(enemy.deathState);
    }
}
