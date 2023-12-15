using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Wheel : Enemy
{
    public Wheel_IdleState idleState { get; private set; }
    public Wheel_MoveState moveState { get; private set; }
    public Wheel_AttackState attackState { get; private set; }
    public Wheel_HurtState hurtState { get; private set; }
    public Wheel_StunnedState stunnedState { get; private set; }
    public Wheel_DeathState deathState { get; private set; }
    public Wheel_RunToPlayerState runToPlayerState { get; private set; }
    public EntityFx effect { get; private set; }
    public bool canDestroy = false;
    public BoxCollider2D colTrigger;
    [HideInInspector] public RaycastHit2D player;
    [SerializeField] private float playerCheckDistance;

    protected override void Awake()
    {
        base.Awake();
        idleState = new Wheel_IdleState(this, stateMachine, "Idle", this);
        moveState = new Wheel_MoveState(this, stateMachine, "Move", this);
        attackState = new Wheel_AttackState(this, stateMachine, "Attack", this);
        hurtState = new Wheel_HurtState(this, stateMachine, "Hurt", this);
        stunnedState = new Wheel_StunnedState(this, stateMachine, "Stunned", this);
        deathState = new Wheel_DeathState(this, stateMachine, "Dead", this);
        runToPlayerState = new Wheel_RunToPlayerState(this, stateMachine, "DetectedPlayer", this);
    }
    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
        effect = GetComponent<EntityFx>();
    }

    protected override void Update()
    {
        base.Update();
        if (canDestroy)
            Destroy(gameObject);
        if (isDead)
        {
            healthCanvas.enabled = false;
        }
        if (beDamaged)
        {
            if (stateMachine.currentState != stunnedState)
                stateMachine.ChangeState(hurtState);
        }
        player = Physics2D.Raycast(transform.position, Vector2.right * facingDirection, playerCheckDistance, playerLayer);
    }
    public bool DetectedPlayer() => Physics2D.Raycast(transform.position, Vector2.right * facingDirection, playerCheckDistance, playerLayer);
    public override bool CanBeStunned()
    {
        if (base.CanBeStunned())
        {
            if (attackedForBeStunned)
                stateMachine.ChangeState(stunnedState);
            return true;
        }
        return false;
    }
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x + playerCheckDistance*facingDirection, transform.position.y));
    }
}
