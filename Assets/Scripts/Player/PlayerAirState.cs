using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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
        if(!player.isKnocked)
            rb.velocity = new Vector2(xInput * player.speed * 0.75f, rb.velocity.y);
        if (player.GroundDetected())
            stateMachine.ChangeState(player.idleState);
        if (Input.GetKeyDown(KeyCode.Space) && !player.doubleJumped && player.playerStats.skillsChecker[0]==1)
        {
            stateMachine.ChangeState(player.doubleJumpState);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && !player.airDashed && player.playerStats.skillsChecker[2] == 1)
        {
            stateMachine.ChangeState(player.airDashState);
        }
        if (player.WallDetected() && player.playerStats.skillsChecker[4]==1)
            stateMachine.ChangeState(player.wallSlideState);
        if (Input.GetKeyDown(KeyCode.K))
            stateMachine.ChangeState(player.primaryAttackState);
    }
}
