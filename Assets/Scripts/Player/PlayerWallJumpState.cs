using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerState
{
    public PlayerWallJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        rb.velocity = new Vector2(5f * -player.facingDirection, 15f);
    }

    public override void Exit()
    {
        base.Exit();
        rb.velocity = Vector2.zero;
    }

    public override void Update()
    {
        base.Update();
        if (rb.velocity.y < -.1f)
        {
            if ((Input.GetKey(KeyCode.A) && player.facingDirection == 1) || (Input.GetKey(KeyCode.D) && player.facingDirection == -1))
            {
                player.Flip();
                rb.velocity = new Vector2(5f * player.facingDirection, 15f);
                if (player.WallDetected())
                    stateMachine.ChangeState(player.wallSlideState);
            }
            else stateMachine.ChangeState(player.airState);
        }
    }
}
