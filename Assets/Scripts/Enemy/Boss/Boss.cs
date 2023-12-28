using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss : Enemy
{
    public Boss_IdleState idleState { get; private set; }
    public Boss_MoveState moveState { get; private set; }
    public Boss_AttackState attackState { get; private set; }
    public Boss_HurtState hurtState { get; private set; }
    public Boss_DeathState deathState { get; private set; }
    public Boss_StunnedState stunnedState { get; private set; }
    public Boss_SpellCastState spellCastState { get; private set; }
    public Boss_SleepState sleepState { get; private set; }

    public EntityFx effect;
    public bool canDestroy = false;
    public BoxCollider2D colTrigger;
    [HideInInspector] public int spellCastType;
    [SerializeField] private Animator animInChild;
    [SerializeField] private Transform evilHandPrefab;
    public bool startBossCombat = false;

    protected override void Awake()
    {
        base.Awake();
        idleState = new Boss_IdleState(this, stateMachine, "Idle", this);
        moveState = new Boss_MoveState(this, stateMachine, "Move", this);
        attackState = new Boss_AttackState(this, stateMachine, "Attack", this);
        hurtState = new Boss_HurtState(this, stateMachine, "Hurt", this);
        stunnedState = new Boss_StunnedState(this, stateMachine, "Stunned", this);
        deathState = new Boss_DeathState(this, stateMachine, "Dead", this);
        spellCastState = new Boss_SpellCastState(this, stateMachine, "SpellCast", this);
        sleepState = new Boss_SleepState(this, stateMachine, "Sleep", this);
    }
    protected override void Start()
    {
        base.Start();
        anim = animInChild;
        stateMachine.Initialize(sleepState);
        facingDirection = 1;
        rb.gravityScale = 0f;
    }

    protected override void Update()
    {
        base.Update();
        if (canDestroy)
            Destroy(gameObject);
        if(startBossCombat && !isDead && !playerPos.GetComponent<Entity>().isDead)
        {
            
            rb.gravityScale = 1f;
        }
    }

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
    public void SpawnEvilHand()
    {
        if (spellCastType == 1)
        {
            Instantiate(evilHandPrefab, new Vector2(playerPos.position.x, playerPos.position.y + 2.3f), Quaternion.identity);
            AudioManager.instance.playerSFX(21);
        }
        else
        {
            AudioManager.instance.playerSFX(22);
            float xPos = 6f;
            for (int i = 0; i < 5; i++)
            {
                Instantiate(evilHandPrefab, new Vector2(-49.5f + xPos, 18f), Quaternion.identity);
                xPos += 6;
            }
        }
    }
}
