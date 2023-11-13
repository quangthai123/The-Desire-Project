using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{
    private int comboCounter = 0;
    private float allowComboTime = 0.2f;
    private float lastAttackedTime;
    public PlayerPrimaryAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if(comboCounter > 3 || !player.enemyDectected || Time.time - lastAttackedTime > allowComboTime)
        {
            comboCounter = 0;
        }
        if (comboCounter == 0)
            AudioManager.instance.playerSFX(1);
    }

    public override void Exit()
    {
        base.Exit();
        if (player.enemyDectected) { 
            comboCounter++;
            lastAttackedTime = Time.time;       
        }
    }

    public override void Update()
    {
        base.Update();
        player.anim.SetInteger("ComboCounter", comboCounter);
        rb.velocity = Vector3.zero;
        if (finishAnim)
            stateMachine.ChangeState(player.idleState);
        if (xInput > 0 && player.facingDirection < 0)
            player.Flip();
        else if (xInput < 0 && player.facingDirection > 0)
            player.Flip();
    }

    public void DamageEnemy()
    {
        if (comboCounter==0)
        {
            Debug.Log("Damage on Enemy: 20");
        } else if(comboCounter==1)
        {
            Debug.Log("Damage on Enemy: 35");
        } else if(comboCounter==2)
        {
            Debug.Log("Damage on Enemy: 60");
        } else if(comboCounter==3)
        {
            Debug.Log("Damage on Enemy: 100");
        }
    }
}
