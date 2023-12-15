using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackFromAirState : PlayerState
{
    public PlayerAttackFromAirState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        AudioManager.instance.playerSFX(17);
        player.isKnocked = true;
        rb.velocity = new Vector2(0f, -20f);
        player.playerStats.DecreaseManaFromSkills(40);
    }

    public override void Exit()
    {
        base.Exit();
        player.isKnocked = false;
    }

    public override void Update()
    {
        base.Update();
        if (player.GroundDetected())
            stateMachine.ChangeState(player.attackGroundedState);
    }
}
