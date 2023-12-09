using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState 
{
    private string animBoolName;
    protected Enemy enemyBase;
    protected EnemyStateMachine enemyStateMachine;
    protected bool finishAnim = false;
    protected float stateDurationCounter;
    protected Rigidbody2D rb;
    public EnemyState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string animBoolName)
    {
        this.enemyBase = enemyBase;
        this.enemyStateMachine = enemyStateMachine;
        this.animBoolName = animBoolName;
    }
    public virtual void Start()
    {
        enemyBase.anim.SetBool(animBoolName, true);
        rb = enemyBase.rb;
    }
    public virtual void Update()
    {
        stateDurationCounter -= Time.deltaTime;
    }
    public virtual void Exit()
    {
        enemyBase.anim.SetBool(animBoolName, false);
    }
    public void SetFinishAnimationEvent()
    {
        finishAnim = true;
    }
}
