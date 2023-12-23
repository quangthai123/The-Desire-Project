using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_AttackState : EnemyState
{
    private Boss enemy;
    public Boss_AttackState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string animBoolName, Boss enemy) : base(enemyBase, enemyStateMachine, animBoolName)
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
        if ((enemy.playerPos.position.x < enemy.transform.position.x && enemy.facingDirection == 1) || (enemy.playerPos.position.x > enemy.transform.position.x && enemy.facingDirection == -1))
            enemy.Flip();
        if (finishAnim)
            enemyStateMachine.ChangeState(enemy.idleState);
        if (enemy.isDead)
            enemy.stateMachine.ChangeState(enemy.deathState);
    }
}
