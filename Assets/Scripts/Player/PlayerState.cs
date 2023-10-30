using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected float xInput;
    private string animBoolName;
    protected Rigidbody2D rb;
    protected bool finishAnim;
    protected float stateDurationCounter;
    public PlayerState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
    {
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }
    public virtual void Enter()
    {
        player.anim.SetBool(animBoolName, true);
        rb = player.rb;
        finishAnim = false;
    }
    public virtual void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        player.anim.SetFloat("yVelocity", rb.velocity.y);
        stateDurationCounter -= Time.deltaTime;
        if (player.GroundDetected())
        {
            player.airDashed = false;
            player.doubleJumped = false;
        }
    }
    public virtual void Exit()
    {
        player.anim.SetBool(animBoolName, false);
    }
    public void SetFinishAnimationEvent()
    {
        finishAnim = true;
    }
}
