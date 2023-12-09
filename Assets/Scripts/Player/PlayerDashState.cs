using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateDurationCounter = player.dashDuration;
        player.mainBox.isTrigger = true;
        player.lieDownBox.enabled = true;
        AudioManager.instance.playerSFX(2);
    }

    public override void Exit()
    {
        base.Exit();
        rb.velocity = Vector2.zero;
        player.dashCooldownCounter = player.dashCooldown;
        player.mainBox.isTrigger = false;
        player.lieDownBox.enabled = false;
    }

    public override void Update()
    {
        base.Update();
        if (stateDurationCounter > 0f && player.GroundDetected() && !player.isKnocked)
        {
            rb.velocity = new Vector2(player.dashSpeed * player.facingDirection, 0f);
        }
        else stateMachine.ChangeState(player.idleState);
        if(Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(player.jumpState);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            stateMachine.ChangeState(player.blockState);
        }
    }
}
