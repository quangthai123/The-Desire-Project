using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_EngaredPilgrim : Enemy
{
    public EngaredPilgrimIdleState idleState { get; private set; }
    public EngaredPilgrimMoveState moveState { get; private set; }
    public EngaredPilgrimAttackState attackState { get; private set; }
    public EngaredPilgrimHurtState hurtState { get; private set; }
    public EngaredPilgrimStunnedState stunnedState { get; private set; }
    public EntityFx effect { get; private set; }

    
    protected override void Awake()
    {
        base.Awake();
        idleState = new EngaredPilgrimIdleState(this, stateMachine, "Idle", this);
        moveState = new EngaredPilgrimMoveState(this, stateMachine, "Move", this);
        attackState = new EngaredPilgrimAttackState(this, stateMachine, "Attack", this);
        hurtState = new EngaredPilgrimHurtState(this, stateMachine, "Hurt", this);
        stunnedState = new EngaredPilgrimStunnedState(this, stateMachine, "Stunned", this);
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
        if(beDamaged)
        {
            if (stateMachine.currentState != attackState && stateMachine.currentState != stunnedState)
                stateMachine.ChangeState(hurtState);
        }
    }
    public override bool CanBeStunned()
    {
        if (base.CanBeStunned())
        {
            if(attackedForBeStunned)
                stateMachine.ChangeState(stunnedState);
            return true;
        }
        return false;
    }
}
