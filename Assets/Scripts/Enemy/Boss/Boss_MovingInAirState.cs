using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_MovingInAirState : EnemyState
{
    private Boss enemy;
    private Vector2 rdAttackPos;
    private int attackNum = 1;
    private int attackedCount = 0;
    public Boss_MovingInAirState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string animBoolName, Boss enemy) : base(enemyBase, enemyStateMachine, animBoolName)
    {
        this.enemy = enemy;
    }
    public override void Start()
    {
        base.Start();
        // x -44 -20
        // y 20 24
        rdAttackPos = new Vector2(Random.Range(-44f, -20f), Random.Range(20f, 24f));
        attackNum = Random.Range(1, 5);
        attackedCount = 0;
        rb.gravityScale = 0f;
        enemy.GetComponentInChildren<Animator>().SetBool("attackFromAirState", false);
        enemy.colTrigger.enabled = false;
    }

    public override void Exit()
    {
        base.Exit();
        rb.gravityScale = 1f;
        enemy.colTrigger.enabled = true;
        finishAnim = false;
        enemy.GetComponentInChildren<Animator>().SetBool("attackFromAirState", true);
    }


    public override void Update()
    {
        base.Update();
        if (attackedCount >= attackNum)
        {
            Debug.Log("Stop!!");
            enemy.stateMachine.ChangeState(enemy.idleState);
        }
        if(Vector2.Distance(enemy.transform.position, rdAttackPos) > .1f)
        {
            enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, rdAttackPos, Time.deltaTime*enemy.movingInAirSpeed);
        } else
        {
            enemy.SpawnAxeSkill();
            rdAttackPos = new Vector2(Random.Range(-44f, -20f), Random.Range(20f, 24f));
            attackedCount++;
        }
    }
}
