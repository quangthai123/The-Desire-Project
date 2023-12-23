using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_MoveState : EnemyState
{
    private Boss enemy;
    public Boss_MoveState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string animBoolName, Boss enemy) : base(enemyBase, enemyStateMachine, animBoolName)
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
        rb.velocity = Vector3.zero;
    }


    public override void Update()
    {
        base.Update();
        Vector2 moveDir = (enemy.playerPos.position - enemy.transform.position).normalized;
        if ((enemy.playerPos.position.x < enemy.transform.position.x && enemy.facingDirection == 1) || (enemy.playerPos.position.x > enemy.transform.position.x && enemy.facingDirection == -1))
            enemy.Flip();
        if (!enemy.PlayerInAttackRange())
        {
            rb.velocity = new Vector2(moveDir.x * enemy.speed, 0f);
        }
        else
        {
            enemy.stateMachine.ChangeState(enemy.attackState);
        }
        if (enemy.isDead)
            enemy.stateMachine.ChangeState(enemy.deathState);
    }
}
