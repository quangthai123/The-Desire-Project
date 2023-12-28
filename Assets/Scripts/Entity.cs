using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public Rigidbody2D rb { get; private set; }
    public Animator anim;
    public EntityFx fx { get; private set; }
    public int facingDirection;
    public System.Action onFlipped;
    public bool isDead = false;
    [Header("Collision Infor")]
    [SerializeField] private Transform groundCheckPos;
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask whatIsWall;
    [SerializeField] protected float groundCheckDisRay;
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        fx = GetComponent<EntityFx>();
        facingDirection = 1;
        if(groundCheckPos == null)
            groundCheckPos = transform;
    }
    protected virtual void Update()
    {
        FlipController();
        
    }
    protected void FlipController()
    {
        if (rb.velocity.x < -.1f && facingDirection == 1)
            Flip();
        else if (rb.velocity.x > .1f && facingDirection == -1)
            Flip();
    }
    public void Flip()
    {
        facingDirection *= -1;
        transform.Rotate(0f, 180f, 0f);
        if(onFlipped != null)
            onFlipped();
    }
    public virtual bool WallDetected() => Physics2D.Raycast(transform.position, Vector2.right * facingDirection, wallCheckDistance, whatIsWall);
    public virtual bool WallGroundLayerDetected() => Physics2D.Raycast(transform.position, Vector2.right * facingDirection, wallCheckDistance, whatIsGround);
    public virtual bool CheckGroundMain() => Physics2D.Raycast(transform.position, Vector2.down, groundCheckDisRay, whatIsGround);
    public virtual bool CheckGroundAction() => Physics2D.Raycast(groundCheckPos.position, Vector2.down, groundCheckDisRay, whatIsGround);
    protected virtual void OnDrawGizmos()
    {       
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x + facingDirection * wallCheckDistance, transform.position.y));
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - groundCheckDisRay));
        if(gameObject.layer == 7)
            Gizmos.DrawLine(groundCheckPos.position, new Vector2(groundCheckPos.position.x, groundCheckPos.position.y - groundCheckDisRay));
    }
}
