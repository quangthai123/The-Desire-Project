using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackGroundedState : PlayerState
{
    public PlayerAttackGroundedState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.isKnocked = true;
        Collider2D[] Enemies = Physics2D.OverlapCircleAll(player.transform.position, 5, 7);
            foreach (var enemy in Enemies)
            {
                if (enemy.GetComponentInParent<Enemy>() != null)
                {
                    AudioManager.instance.playerSFX(19);
                    enemy.GetComponentInParent<Enemy>().BeDamaged();
                    enemy.GetComponentInParent<EnemyStats>().TakeDamage(player.playerStats.damage.GetValue() * 6);
                }
                else AudioManager.instance.playerSFX(18);
            }        
    }

    public override void Exit()
    {
        base.Exit();
        player.isKnocked = false;
    }

    public override void Update()
    {
        base.Update();
        if (finishAnim)
            stateMachine.ChangeState(player.idleState);
    }
}
