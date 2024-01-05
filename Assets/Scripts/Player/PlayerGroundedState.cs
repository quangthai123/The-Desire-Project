using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if(Input.GetKeyDown(KeyCode.Space) && player.GroundDetected())
        {
            stateMachine.ChangeState(player.jumpState);
        }
        if(Input.GetKeyDown(KeyCode.LeftShift) && player.dashCooldownCounter < 0f)
        {
            if(!player.WallGroundLayerDetected())
                stateMachine.ChangeState(player.dashState);
            else
            {
                player.transform.position = new Vector2(player.transform.position.x - player.facingDirection * 1.4f, player.transform.position.y);
                stateMachine.ChangeState(player.dashState);
            }
        }
        if(Input.GetKeyDown(KeyCode.J))
        {
            stateMachine.ChangeState(player.blockState);
        }
        if(Input.GetKeyDown(KeyCode.K))
        {
            stateMachine.ChangeState(player.primaryAttackState);
        }
        if(rb.velocity.y < -.1f)
        {
            stateMachine.ChangeState(player.airState);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (player.playerStats.currentFlask > 0 && player.playerStats.currentHealth < player.playerStats.maxHealth.GetValue())
                stateMachine.ChangeState(player.healingState);
            else
                AudioManager.instance.playerSFX(13);
        }
    }
}
