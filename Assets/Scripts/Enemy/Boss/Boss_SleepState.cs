using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_SleepState : EnemyState
{
    private Boss enemy;
    public Boss_SleepState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string animBoolName, Boss enemy) : base(enemyBase, enemyStateMachine, animBoolName)
    {
        this.enemy = enemy;
    }
    public override void Start()
    {
        base.Start();
    }

    public override void Exit()
    {
        base.Exit();
        AudioManager.instance.playerSFX(24);
        finishAnim = false;
    }


    public override void Update()
    {
        base.Update();
        if (enemy.startBossCombat)
            enemy.GetComponentInChildren<Animator>().SetBool("Enter", true);
        if(finishAnim)
        {
            enemy.GetComponentInChildren<Animator>().SetBool("Enter", false);
            enemy.stateMachine.ChangeState(enemy.idleState);
        }
    }
}
