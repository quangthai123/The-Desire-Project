using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{
    public int comboCounter = 0;
    private float allowComboTime = 0.2f;
    private float lastAttackedTime;
    public PlayerPrimaryAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        AudioManager.instance.playerSFX(1);
        if (comboCounter > 3 || (!player.enemyDetected && player.GroundDetected()) || Time.time - lastAttackedTime > allowComboTime)
        {
            comboCounter = 0;
        }
        if ((comboCounter == 0 && !player.enemyDetected) || (comboCounter==1 && !player.enemyDetected) || (player.enemyDetected && !player.GroundDetected()))
        {
            player.anim.speed = 2f;
        }
    }

    public override void Exit()
    {
        base.Exit();
        if (player.enemyDetected && player.GroundDetected()) {
            comboCounter++;
            lastAttackedTime = Time.time;
        }
        if (!player.GroundDetected() && comboCounter==0) 
        {
            comboCounter++;
            lastAttackedTime = Time.time;
        }
    }

    public override void Update()
    {
        base.Update();
        player.anim.SetInteger("ComboCounter", comboCounter);
        if(player.GroundDetected())
            rb.velocity = Vector3.zero;
        if (finishAnim)
            stateMachine.ChangeState(player.idleState);
        if (xInput > 0 && player.facingDirection < 0 && comboCounter == 0)
            player.Flip();
        else if (xInput < 0 && player.facingDirection > 0 && comboCounter == 0)
            player.Flip();
        if(Input.GetKeyDown(KeyCode.J))
        {
            stateMachine.ChangeState(player.blockState);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if ((xInput < 0 && player.facingDirection == 1) || (xInput > 0 && player.facingDirection == -1))
                player.Flip();
            stateMachine.ChangeState(player.dashState);
        }
    }

    public void DamageEnemy()
    {
        if (comboCounter==0)
        {
            Debug.Log("Damage on Enemy: 20");
        } else if(comboCounter==1)
        {
            Debug.Log("Damage on Enemy: 25");
        } else if(comboCounter==2)
        {
            Debug.Log("Damage on Enemy: 35");
        } else if(comboCounter==3)
        {
            Debug.Log("Damage on Enemy: 55");
        }
    }
}
