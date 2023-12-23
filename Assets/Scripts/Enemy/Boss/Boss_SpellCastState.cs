using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_SpellCastState : EnemyState
{
    private Boss enemy;
    public Boss_SpellCastState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string animBoolName, Boss enemy) : base(enemyBase, enemyStateMachine, animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Start()
    {
        base.Start();
        finishAnim = false;
    }
    public override void Exit()
    {
        base.Exit();
        finishAnim = true;
    }


    public override void Update()
    {
        base.Update();
        if(finishAnim)
            enemyStateMachine.ChangeState(enemy.idleState);
        if (enemy.isDead)
            enemy.stateMachine.ChangeState(enemy.deathState);
    }
}
