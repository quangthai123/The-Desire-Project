using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngaredPilgrimMoveState : EnemyState
{
    private Enemy_EngaredPilgrim enemy;
    public EngaredPilgrimMoveState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_EngaredPilgrim _enemy) : base(_enemy, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }
    public override void Start()
    {
        base.Start();
    }
    public override void Exit()
    {
        base.Exit();
        rb.velocity = Vector2.zero;
    }
    public override void Update()
    {
        base.Update();
        rb.velocity = new Vector2(enemy.speed * enemy.facingDirection, rb.velocity.y);
        if (enemy.WallDetected() || !enemy.CheckGroundAction() || enemy.PlayerInAttackRange())
            enemyStateMachine.ChangeState(enemy.idleState);
    }
}
