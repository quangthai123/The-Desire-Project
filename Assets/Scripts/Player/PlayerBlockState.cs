using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlockState : PlayerState
{
    public bool isCountering = false;

    public PlayerBlockState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        stateDurationCounter = player.blockDuration;
        AudioManager.instance.playerSFX(6);
        player.anim.SetBool("SuccessfulCounter", false);
        rb.velocity = Vector2.zero;
        isCountering = false;
    }

    public override void Exit()
    {
        base.Exit();
        player.isKnocked = false;
        rb.velocity = Vector2.zero;
        isCountering = false;
    }

    public override void Update()
    {
        base.Update();
        
        Collider2D[] Enemies = Physics2D.OverlapCircleAll(player.enemyCheck.position, player.radiusEnemyDetected);
        foreach (var enemy in Enemies)
        {
            if (enemy.GetComponentInParent<Enemy>() != null)
            {
                if (enemy.GetComponentInParent<Enemy>().CanBeStunned())
                {
                    player.isKnocked = true;
                    if(enemy.GetComponentInParent<Enemy>().attackedForBeStunned)
                    {
                        isCountering = true;
                        rb.velocity = Vector2.zero;
                        stateDurationCounter = 3f;
                        player.anim.SetBool("SuccessfulCounter", isCountering);
                        AudioManager.instance.playerSFX(11);
                
                    } 
                 }
            } else if(enemy.GetComponent<Arrow>() != null)
            {   
                if(enemy.gameObject.transform.position.y > player.enemyCheck.position.y)
                    player.isKnocked = true;
            }
        }
        if (stateDurationCounter < 0 || finishAnim)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
