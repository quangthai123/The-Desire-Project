using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel_RunToPlayerState : EnemyState
{
    private Enemy_Wheel enemy;
    private float enemySpeedOriginal;
    public Wheel_RunToPlayerState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string animBoolName, Enemy_Wheel enemy) : base(enemyBase, enemyStateMachine, animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Start()
    {
        base.Start();
        enemySpeedOriginal = enemy.speed;
        enemy.speed = enemySpeedOriginal * 1.5f;
    }
    public override void Exit()
    {
        base.Exit();
        enemy.speed = enemySpeedOriginal;
        rb.velocity = Vector2.zero;
    }


    public override void Update()
    {
        base.Update();
        if(enemy.player)
        { 
            if ((enemy.player.transform.position.x < enemy.transform.position.x && enemy.facingDirection == 1) || (enemy.player.transform.position.x > enemy.transform.position.x && enemy.facingDirection == -1))
                enemy.Flip();

            Vector2 runDir = (enemy.player.transform.position - enemy.transform.position).normalized;
            rb.velocity = new Vector2(runDir.x * enemy.speed, 0f);
            if (enemy.PlayerInAttackRange())
                enemy.stateMachine.ChangeState(enemy.attackState);
        }
        else if (!enemy.player)
            enemy.stateMachine.ChangeState(enemy.idleState);
    }
}
