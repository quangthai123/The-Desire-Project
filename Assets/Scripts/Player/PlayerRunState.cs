using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerGroundedState
{
    public PlayerRunState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        AudioManager.instance.playerSFX(3);
    }

    public override void Exit()
    {
        base.Exit();
        rb.velocity = Vector3.zero;
        AudioManager.instance.StopSFX(3);
    }

    public override void Update()
    {
        base.Update();
        if (xInput == 0)
            stateMachine.ChangeState(player.idleState);
        if (!player.isKnocked)
            rb.velocity = new Vector2(xInput * player.speed, rb.velocity.y);
    }
}
