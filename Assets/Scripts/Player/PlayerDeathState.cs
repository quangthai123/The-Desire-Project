using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathState : PlayerState
{
    public PlayerDeathState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        AudioManager.instance.playBgm = false;
        AudioManager.instance.playerSFX(14);
        player.isKnocked = true;
    }

    public override void Exit()
    {
        
    }

    public override void Update()
    {
        base.Update();
        rb.velocity = new Vector2(0f, rb.velocity.y);
        if(finishAnim)
            player.playerStats.ResetGameAfterDied();
    }
}
