using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Boss_AttackState : EnemyState
{
    private Boss enemy;
    private int rdAttackNum;
    private int attackedCount = 1;
    private bool rotatedInXAxisWhenAirAttack = false;
    public Boss_AttackState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string animBoolName, Boss enemy) : base(enemyBase, enemyStateMachine, animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Start()
    {
        base.Start();
        rdAttackNum = Random.Range(1, 5);
        
    }
    public override void Exit()
    {
        base.Exit();
        finishAnim = false;
    }


    public override void Update()
    {
        base.Update();
        if ((enemy.playerPos.position.x < enemy.transform.position.x && enemy.facingDirection == 1) || (enemy.playerPos.position.x > enemy.transform.position.x && enemy.facingDirection == -1))
        {
            enemy.Flip();
            //if(enemy.attackFromAirState && !rotatedInXAxisWhenAirAttack)
            //{
            //    Debug.Log("sdasddasdsad");
            //    enemy.transform.rotation = Quaternion.Euler(enemy.transform.eulerAngles.x + 180f,
            //        enemy.transform.rotation.y, enemy.transform.rotation.z);
            //    rotatedInXAxisWhenAirAttack = true;
            //}else if(enemy.attackFromAirState &&  rotatedInXAxisWhenAirAttack)
            //    rotatedInXAxisWhenAirAttack = false;
        }
        //if(finishAnim && enemy.attackFromAirState)
        //{
        //    if(attackedCount > rdAttackNum)
        //    {
        //        attackedCount = 1;
        //        enemy.Change
        //    }
        //}
        if (finishAnim)
        {
            if (attackedCount > rdAttackNum)
            {
                attackedCount = 1;
                enemy.stateMachine.ChangeState(enemy.idleState);
            }
            else
            {
                if (!enemy.PlayerInAttackRange())
                {
                    AudioManager.instance.playerSFX(0);
                    float xPos = Random.Range(-9f, 9f);
                    // -46 -18 max boss spawn pos
                    // -44 -20 max player spawn pos
                    if (enemy.playerPos.position.x < -44f)
                    {
                        xPos = Random.Range(2f, 9f);
                    } else if(enemy.playerPos.position.x > -20f)
                    {
                        xPos = Random.Range(-9f, -2f);                       
                    } else if(enemy.playerPos.position.x > -44f && enemy.playerPos.position.x < -37f)
                    {
                        xPos = Random.Range((-46f - enemy.playerPos.position.x), 9f);
                    } else if(enemy.playerPos.position.x < -20f && enemy.playerPos.position.x > -27f)
                        xPos = Random.Range(-9f, (-20f - enemy.playerPos.position.x));
                    enemy.transform.position = new Vector2(enemy.playerPos.position.x + xPos, enemy.transform.position.y);
                }   
                finishAnim = false;
                attackedCount++;
            }

        }
        //if(enemy.attackFromAirState)
        //{
        //    Vector3 directionToPlayer = enemy.playerPos.position - enemy.transform.position;
        //    directionToPlayer.Normalize();
            
        //    float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;

            
        //    enemy.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        //}
        if (enemy.isDead)
            enemy.stateMachine.ChangeState(enemy.deathState);
    }
}
