using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilHand : MonoBehaviour
{
    private Transform boxCenterPos;
    [SerializeField] private Vector2 boxSize;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float attackBoxDownDistance;
    [SerializeField] private int damage;
    private bool finish = false;
    private Animator anim;

    void Start()
    {
        boxCenterPos = transform;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (finish)
            Destroy(gameObject);
    }
    private void AttackTrigger()
    {
        RaycastHit2D player = Physics2D.BoxCast(boxCenterPos.position, boxSize, 0f, Vector2.down, attackBoxDownDistance, playerLayer);
        if (player && !player.collider.GetComponent<Player>().isDead)
        {
            Player playerRef = player.collider.GetComponent<Player>();
            if (playerRef.stateMachine.currentState != playerRef.dashState && playerRef.stateMachine.currentState != playerRef.airDashState && !playerRef.isKnocked)
            {
                playerRef.BeDamaged(damage, transform.position);
                AudioManager.instance.playerSFX(16);
            }
        }
    }
    private void FinishAnim()
    {
        finish = true;
        anim.speed = 1;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(new Vector2(boxCenterPos.position.x, boxCenterPos.position.y - attackBoxDownDistance), boxSize);
    }


}
