using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : Entity
{
    [Header("Move Infor")]
    public float speed;
    public float idleMinTime;
    public float idleMaxTime;
    [Header("Detect player")]
    [SerializeField] protected float detectPlayerRadius;
    [SerializeField] protected LayerMask playerLayer;
    [SerializeField] public Transform playerPos;
    [Header("Attack Infor")]
    public float attackCooldown;

    [Header("Hurt Infor")]
    public bool beDamaged = false;
    [Header("Stunned Infor")]
    protected bool canBeStunned;
    public EnemyStateMachine stateMachine { get; private set; }
    protected virtual void Awake()
    {
        stateMachine = new EnemyStateMachine();
    }
    
    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
    }
    public bool PlayerInAttackRange() => Physics2D.OverlapCircle(transform.position, detectPlayerRadius, playerLayer);
    protected override void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, detectPlayerRadius);
    }
    public void SetEventOnFinishAnimation() => stateMachine.currentState.SetFinishAnimationEvent();
    private void AttackTrigger()
    {
        Collider2D player = Physics2D.OverlapCircle(transform.position, detectPlayerRadius, playerLayer);
        if (player)
        {
            player.GetComponent<Player>().BeDamaged();
            AudioManager.instance.playerSFX(8);
        }
    }
    public void BeDamaged()
    {
        beDamaged = true;
    }
    protected virtual void OpenCounterAttackWindow()
    {
        canBeStunned = true;
    }
    protected virtual void CloseCounterAttackWindow()
    {
        canBeStunned = false;
    }
    public virtual bool CanBeStunned()
    {
        if(canBeStunned)
        {
            CloseCounterAttackWindow();
            return true;
        } 
        return false;
    }
}
