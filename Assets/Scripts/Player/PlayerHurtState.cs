using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtState : PlayerState
{
    public PlayerHurtState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.isKnocked = true;
        rb.velocity = new Vector2(player.knockbackDirection.x * -player.facingDirection, player.knockbackDirection.y);
        stateDurationCounter = player.knockbackDuration;
    }

    public override void Exit()
    {
        base.Exit();
        player.isKnocked = false;
    }

    public override void Update()
    {
        base.Update();
        if (stateDurationCounter < 0)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
