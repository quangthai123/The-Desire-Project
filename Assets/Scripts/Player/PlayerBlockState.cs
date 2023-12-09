using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlockState : PlayerState
{
    public PlayerBlockState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        stateDurationCounter = player.blockDuration;
        AudioManager.instance.playerSFX(6);
        player.anim.SetBool("SuccessfulCounter", false);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        rb.velocity = Vector2.zero;
        Collider2D[] Enemies = Physics2D.OverlapCircleAll(player.enemyCheck.position, player.radiusEnemyDetected);
        foreach (var enemy in Enemies)
        {
            if (enemy.GetComponentInParent<Enemy>() != null)
            {
                if (enemy.GetComponentInParent<Enemy>().CanBeStunned())
                {
                    stateDurationCounter = 1.5f;
                    player.anim.SetBool("SuccessfulCounter", true);
                }
            }
        }
        if (stateDurationCounter < 0 || finishAnim)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
