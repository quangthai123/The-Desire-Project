using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel_IdleState : EnemyState
{
    private Enemy_Wheel enemy;
    public Wheel_IdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Wheel _enemy) : base(_enemy, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }
    public override void Start()
    {
        base.Start();
        enemyBase.attackedForBeStunned = false;
        if (enemy.PlayerInAttackRange())
        {
            stateDurationCounter = enemy.attackCooldown;
            return;
        }
        float rd = Random.Range(enemy.idleMinTime, enemy.idleMaxTime);
        stateDurationCounter = rd;
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void Update()
    {
        base.Update();
        // attack
        if (enemy.PlayerInAttackRange() && enemy.healthCanvas.enabled)
        {
            if (enemy.playerPos.position.x < enemy.transform.position.x && enemy.facingDirection == 1)
                enemy.Flip();
            if (enemy.playerPos.position.x > enemy.transform.position.x && enemy.facingDirection == -1)
                enemy.Flip();
            if (stateDurationCounter <= 0f)
                enemyStateMachine.ChangeState(enemy.attackState);
        }
        // patroller
        else if (stateDurationCounter <= 0f && enemy.healthCanvas.enabled)
        {
            enemy.Flip();
            enemyStateMachine.ChangeState(enemy.moveState);
        }
        if (!enemy.healthCanvas.enabled)
            enemyStateMachine.ChangeState(enemy.deathState);
        if(enemy.DetectedPlayer() && !enemy.PlayerInAttackRange())
            enemyStateMachine.ChangeState(enemy.runToPlayerState);
    }
}
