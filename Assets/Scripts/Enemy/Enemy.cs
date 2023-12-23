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
    [HideInInspector] public Transform playerPos;
    [SerializeField] protected Transform playerCheckPos;
    public Canvas healthCanvas;
    [Header("Attack Infor")]
    public float attackCooldown;

    [Header("Hurt Infor")]
    public bool beDamaged = false;
    [Header("Stunned Infor")]
    protected bool canBeStunned;
    public bool attackedForBeStunned = false;
    [HideInInspector]public EnemyStats enemyStats;
    
    public EnemyStateMachine stateMachine { get; private set; }

    protected virtual void Awake()
    {
        stateMachine = new EnemyStateMachine();
        if (playerCheckPos == null)
            playerCheckPos = transform;
    }
    protected override void Start()
    {
        base.Start();
        enemyStats = GetComponent<EnemyStats>();
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }
    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
    }
    public bool PlayerInAttackRange() => Physics2D.OverlapCircle(playerCheckPos.position, detectPlayerRadius, playerLayer);
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
         Gizmos.DrawWireSphere(playerCheckPos.position, detectPlayerRadius);
    }
    public void SetEventOnFinishAnimation() => stateMachine.currentState.SetFinishAnimationEvent();
    public void AttackTrigger()
    {
        if(this.gameObject.tag == "Boss")
        { 
            AudioManager.instance.playerSFX(23);
        }
        Collider2D player = Physics2D.OverlapCircle(playerCheckPos.position, detectPlayerRadius, playerLayer);
        if (player && !player.GetComponent<Player>().isDead)
        {
            Player playerRef = player.GetComponent<Player>();
            if (playerRef.stateMachine.currentState != playerRef.blockState && playerRef.stateMachine.currentState != playerRef.airDashState && playerRef.stateMachine.currentState != playerRef.dashState && playerRef.stateMachine.currentState != playerRef.hurtState)
            {
               
                playerRef.BeDamaged(enemyStats.damage.GetValue(), transform.position);
                AudioManager.instance.playerSFX(16);
            }
            else if (playerRef.stateMachine.currentState == playerRef.blockState && ((player.transform.position.x < transform.position.x && playerRef.facingDirection == -1) || (player.transform.position.x > transform.position.x && playerRef.facingDirection == 1)))
            {
                playerRef.BeDamaged(enemyStats.damage.GetValue(), transform.position);
                AudioManager.instance.playerSFX(16);
            }
            else if (playerRef.stateMachine.currentState == playerRef.blockState && !playerRef.blockState.isCountering)
            {
                playerRef.isKnocked = true;
                playerRef.LightlyPushingPlayer(transform.position);
                AudioManager.instance.playerSFX(10);
            }
        }
    }
    public void BeDamaged()
    {
        beDamaged = true;
    }
    public virtual void OpenCounterAttackWindow()
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
    public void AttackForBeStunned() 
    {
        attackedForBeStunned = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10 && !isDead)
        {
            Destroy(collision.gameObject);
            BeDamaged();
            enemyStats.TakeDamage(playerPos.gameObject.GetComponent<Player>().playerStats.damage.GetValue()*3);
            AudioManager.instance.playerSFX(20);
        }
    }
}
