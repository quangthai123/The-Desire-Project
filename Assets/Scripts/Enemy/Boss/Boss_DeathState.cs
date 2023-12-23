using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_DeathState : EnemyState
{
    private Boss enemy;
    public Boss_DeathState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string animBoolName, Boss enemy) : base(enemyBase, enemyStateMachine, animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Start()
    {
        base.Start();
        enemy.colTrigger.enabled = false;
        AudioManager.instance.playBgm = false;
        AudioManager.instance.playerSFX(24);
    }
    public override void Exit()
    {
        base.Exit();
       
    }


    public override void Update()
    {
        base.Update();
        if (finishAnim)
        {
            AudioManager.instance.playerSFX(25);           
            enemy.canDestroy = true;
        }
    }
}
