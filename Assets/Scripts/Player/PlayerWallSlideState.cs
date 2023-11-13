using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.doubleJumped = false;
        player.airDashed = false;
        AudioManager.instance.playerSFX(7);
    }

    public override void Exit()
    {
        base.Exit();
        AudioManager.instance.StopSFX(7);
    }

    public override void Update()
    {
        base.Update();
        if(Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(player.wallJumpState);
            return;
        }
        rb.velocity = new Vector2(0f, rb.velocity.y * 0.9f);
        if (player.GroundDetected())
            stateMachine.ChangeState(player.idleState);
        if (!player.WallDetected())
            stateMachine.ChangeState(player.airState);
    }
}
