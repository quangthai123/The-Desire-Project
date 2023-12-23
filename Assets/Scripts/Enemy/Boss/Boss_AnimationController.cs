using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_AnimationController : MonoBehaviour
{
    [SerializeField] private Animator anim;
    private Boss parent;
    private void Start()
    {
        anim = GetComponent<Animator>();
        parent = GetComponentInParent<Boss>();
    }
    private void AttackTrigger()
    {
        parent.SpawnEvilHand();
    }
    private void CuttingAttackTrigger()
    {
        parent.AttackTrigger();
    }
    private void FinishAnimation()
    {
        parent.SetEventOnFinishAnimation();
    }
    private void AttackForBeStunned()
    {
        parent.AttackForBeStunned();
    }
    private void CanBeStunned()
    {
        parent.OpenCounterAttackWindow();
    }
}
