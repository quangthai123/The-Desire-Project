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
    public bool attackedForBeStunned = false;
    public EnemyStats enemyStats;
    public EnemyStateMachine stateMachine { get; private set; }

    protected virtual void Awake()
    {
        stateMachine = new EnemyStateMachine();
    }
    protected override void Start()
    {
        base.Start();
        enemyStats = GetComponent<EnemyStats>();
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
            if (player.GetComponent<Player>().stateMachine.currentState != player.GetComponent<Player>().blockState && player.GetComponent<Player>().stateMachine.currentState != player.GetComponent<Player>().airDashState && player.GetComponent<Player>().stateMachine.currentState != player.GetComponent<Player>().dashState)
            {
                if (player.GetComponent<Player>().stateMachine.currentState != player.GetComponent<Player>().hurtState)
                {
                    player.GetComponent<Player>().BeDamaged(enemyStats.damage.GetValue());
                    AudioManager.instance.playerSFX(8);
                }
            }
            else if (player.GetComponent<Player>().stateMachine.currentState == player.GetComponent<Player>().blockState && !player.GetComponent<Player>().blockState.isCountering)
            {
                player.GetComponent<Player>().rb.velocity = new Vector2(9f * -player.GetComponent<Player>().facingDirection, rb.velocity.y);
                AudioManager.instance.playerSFX(10);
            }
            else if (player.GetComponent<Player>().stateMachine.currentState == player.GetComponent<Player>().blockState && player.GetComponent<Player>().blockState.isCountering)
                return;
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
    protected void AttackForBeStunned() 
    {
        attackedForBeStunned = true;
    }
}
