using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngaredPilgrimIdleState : EnemyState
{
    private Enemy_EngaredPilgrim enemy;
    public EngaredPilgrimIdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_EngaredPilgrim _enemy) : base(_enemy, _stateMachine, _animBoolName)
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
        if (enemy.PlayerInAttackRange())
        {
            if (enemy.playerPos.position.x < enemy.transform.position.x && enemy.facingDirection == 1)
                enemy.Flip();
            if(enemy.playerPos.position.x > enemy.transform.position.x && enemy.facingDirection == -1)
                enemy.Flip();
            if (stateDurationCounter <= 0f && !enemy.beDamaged)
                enemyStateMachine.ChangeState(enemy.attackState);
        }
        // patroller
        else if (stateDurationCounter <= 0f)
        {
            enemy.Flip();
            enemyStateMachine.ChangeState(enemy.moveState);
        }
    }
}
