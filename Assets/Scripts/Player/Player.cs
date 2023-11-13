using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region
    public Animator anim { get; private set; }
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerRunState runState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerDoubleJumpState doubleJumpState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerAirDashState airDashState { get; private set; }
    public PlayerBlockState blockState { get; private set; }
    public PlayerWallSlideState wallSlideState { get; private set; }
    public PlayerWallJumpState wallJumpState { get; private set; }
    public PlayerPrimaryAttackState primaryAttackState { get; private set; }
    #endregion
    public bool enemyDectected { get; private set; }
    public BoxCollider2D mainBox;
    public BoxCollider2D lieDownBox;
    private Vector2 boxSize;
    [Header("Collision Infor")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask whatIsWall;
    [SerializeField] private float groundCheckDisRay;


    [Header("Dash Infor")]
    public float dashDuration;
    public float dashCooldown;
    [HideInInspector] public float dashCooldownCounter;
    public float dashSpeed;

    [Header("Block Infor")]
    public float blockDuration;
    [Header("Enemy Detected Infor")]
    [SerializeField] private Transform enemyCheck;
    [SerializeField] private float radiusEnemyDetected;
    [SerializeField] private LayerMask whatIsEnemy;
    public Rigidbody2D rb { get; private set; }
    public float speed;
    public float jumpForce;
    public int facingDirection { get; private set; }
    [HideInInspector] public bool airDashed = false;
    [HideInInspector] public bool doubleJumped = false;

    private void Awake()
    {       
        stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        runState = new PlayerRunState(this, stateMachine, "Run");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        doubleJumpState = new PlayerDoubleJumpState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        airDashState = new PlayerAirDashState(this, stateMachine, "AirDash");
        blockState = new PlayerBlockState(this, stateMachine, "Block");
        wallSlideState = new PlayerWallSlideState(this, stateMachine, "Hanging");
        wallJumpState = new PlayerWallJumpState(this, stateMachine, "Jump");
        primaryAttackState = new PlayerPrimaryAttackState(this, stateMachine, "IsAttacking");
    }
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        stateMachine.Initialize(idleState);
        facingDirection = 1;
        lieDownBox.enabled = false;
        boxSize = new Vector2(mainBox.bounds.size.x / 1.25f, mainBox.bounds.size.y / 2.5f);
    }
    void Update()
    {
        stateMachine.currentState.Update();
        FlipController();
        dashCooldownCounter -= Time.deltaTime;
        DetectEnemy();
        if(CheckGroundAction() && rb.velocity.y < -1f)
            AudioManager.instance.playerSFX(4);
    }

    private void DetectEnemy()
    {
        Collider2D[] Enemies = Physics2D.OverlapCircleAll(enemyCheck.position, radiusEnemyDetected, whatIsEnemy);
        if (Enemies.Length > 0)
        {
            enemyDectected = true;
        }
        else enemyDectected = false;
    }

    private void FlipController()
    {
        if (rb.velocity.x < -.1f && facingDirection == 1)
            Flip();
        else if(rb.velocity.x > .1f && facingDirection == -1)
            Flip();
    }
    public void Flip()
    {
        facingDirection *= -1;
        transform.Rotate(0f, 180f, 0f);
    }
    private void FinishAnimation() => stateMachine.currentState.SetFinishAnimationEvent();
    public bool WallDetected() => Physics2D.Raycast(transform.position, Vector2.right*facingDirection, wallCheckDistance, whatIsWall);
    public bool CheckGroundAction() => Physics2D.Raycast(transform.position, Vector2.down, groundCheckDisRay, whatIsGround);
    public bool GroundDetected() => Physics2D.BoxCast(mainBox.bounds.center, boxSize, 0f, Vector2.down, groundCheckDistance, whatIsGround);
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(mainBox.bounds.center - new Vector3(0f, groundCheckDistance, 0f), boxSize);
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x + facingDirection*wallCheckDistance, transform.position.y));
        Gizmos.DrawWireSphere(enemyCheck.position, radiusEnemyDetected);
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - groundCheckDisRay));
    }
}
