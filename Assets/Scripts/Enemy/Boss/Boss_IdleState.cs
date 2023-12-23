using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_IdleState : EnemyState
{
    private Boss enemy;
    private int rdAttackType;
    public Boss_IdleState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string animBoolName, Boss enemy) : base(enemyBase, enemyStateMachine, animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Start()
    {
        base.Start();
        enemyBase.attackedForBeStunned = false;
        float rd = Random.Range(enemy.idleMinTime, enemy.idleMaxTime);
        stateDurationCounter = rd;
        rdAttackType = Random.Range(0, 3);
    }
    public override void Exit()
    {
        base.Exit();
    }


    public override void Update()
    {
        base.Update();
        if(stateDurationCounter < 0)
        {
            if (rdAttackType == 0)
                enemy.stateMachine.ChangeState(enemy.moveState);
            else if (rdAttackType == 1)
            {
                enemy.stateMachine.ChangeState(enemy.spellCastState);
                enemy.spellCastType = 1;
            }
            else if (rdAttackType == 2)
            {
                enemy.stateMachine.ChangeState(enemy.spellCastState);
                enemy.spellCastType = 2;
            }
        }
        if (enemy.isDead)
            enemyStateMachine.ChangeState(enemy.deathState);
    }
}
