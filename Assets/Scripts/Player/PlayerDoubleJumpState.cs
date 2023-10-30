using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDoubleJumpState : PlayerState
{
    public PlayerDoubleJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        rb.velocity = new Vector2(rb.velocity.x, player.jumpForce);
    }

    public override void Exit()
    {
        base.Exit();
        player.doubleJumped = true;
    }

    public override void Update()
    {
        base.Update();
        rb.velocity = new Vector2(xInput * player.speed * 0.8f, rb.velocity.y);
        if (rb.velocity.y < -.1f)
            stateMachine.ChangeState(player.airState);
        if (Input.GetKeyDown(KeyCode.LeftShift) && !player.airDashed)
        {
            stateMachine.ChangeState(player.airDashState);
        }
    }
}
