using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirDashState : PlayerState
{
    public PlayerAirDashState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateDurationCounter = player.dashDuration-.125f;
        AudioManager.instance.playerSFX(0);
    }

    public override void Exit()
    {
        base.Exit();
        rb.velocity = Vector2.zero;
        player.airDashed = true;
    }

    public override void Update()
    {
        base.Update();
        if (stateDurationCounter > 0 && !player.isKnocked)
        {
            rb.velocity = new Vector2(player.dashSpeed * 1.2f * player.facingDirection, 0f);
        }
        else stateMachine.ChangeState(player.airState);
        if (player.WallDetected())
            stateMachine.ChangeState(player.wallSlideState);
    }
}
