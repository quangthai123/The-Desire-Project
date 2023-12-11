using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Player : Entity
{
    #region
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
    public PlayerHealingState healingState { get; private set; }
    public PlayerHurtState hurtState { get; private set; }
    #endregion
    public BoxCollider2D mainBox;
    public BoxCollider2D lieDownBox;
    private Vector2 boxSize;
    [Header("Dash Infor")]
    public float dashDuration;
    public float dashCooldown;
    [HideInInspector] public float dashCooldownCounter;
    public float dashSpeed;
    [SerializeField] private float groundCheckDistance;

    [Header("Block Infor")]
    public float blockDuration;
    [Header("Enemy Detected Infor")]
    public Transform enemyCheck;
    public float radiusEnemyDetected;
    [SerializeField] private LayerMask whatIsEnemy;
    public bool enemyDetected { get; private set; } = false;
    public float speed;
    public float jumpForce;
    [HideInInspector] public bool airDashed = false;
    [HideInInspector] public bool doubleJumped = false;
    [Header("Knockback Infor")]
    public Vector2 knockbackDirection;
    public float knockbackDuration;
    public bool isKnocked = false;
    public PlayerStats playerStats;
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
        healingState = new PlayerHealingState(this, stateMachine, "Healing");
        hurtState = new PlayerHurtState(this, stateMachine, "Hurt");
    }
    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
        lieDownBox.enabled = false;
        playerStats = GetComponent<PlayerStats>();
        boxSize = new Vector2(mainBox.bounds.size.x / 1.25f, mainBox.bounds.size.y / 2.5f);
    }
    protected override void Update()
    {
        if(!isKnocked)
            base.Update();
        stateMachine.currentState.Update();
        dashCooldownCounter -= Time.deltaTime;
        DetectEnemy();
        if (CheckGroundAction() && rb.velocity.y < -1f)
            AudioManager.instance.playerSFX(4);

        // Dua binh rong va tien cho npc thi goi ham nay de tang so binh mau toi da, dong thoi hoi lai
        // toan bo binh mau da mat
        if (Input.GetKeyDown(KeyCode.E))
            playerStats.IncreaseMaxFlask(1);


        // Save game de hoi toan bo hp, mana va binh mau
        if (Input.GetKeyDown(KeyCode.R))
            playerStats.HealingAll();

    }

    private void DetectEnemy()
    {
        Collider2D EnemyDetected = Physics2D.OverlapCircle(enemyCheck.position, radiusEnemyDetected, whatIsEnemy);
        if (EnemyDetected)
        {
            enemyDetected = true;
        }
        else enemyDetected = false;
    }
    private void AttackTrigger() 
    {
        if(enemyDetected)
            AudioManager.instance.playerSFX(9);
        Collider2D[] Enemies = Physics2D.OverlapCircleAll(enemyCheck.position, radiusEnemyDetected, whatIsEnemy);
        foreach (var enemy in Enemies)
        {
            if (enemy.GetComponentInParent<Enemy>() != null)
            {
                enemy.GetComponentInParent<Enemy>().BeDamaged();
                playerStats.IncreaseManaFromAttack(1);
                if (stateMachine.currentState != blockState)
                {
                    if (primaryAttackState.comboCounter == 0)
                    {
                        enemy.GetComponentInParent<EnemyStats>().TakeDamage(playerStats.damage.GetValue());
                        Debug.Log(playerStats.damage.GetValue());
                    }
                    else if (primaryAttackState.comboCounter == 1)
                    {
                        enemy.GetComponentInParent<EnemyStats>().TakeDamage(playerStats.damage.GetValue() * 2);
                        Debug.Log(playerStats.damage.GetValue() * 2);
                    }
                    else if (primaryAttackState.comboCounter == 2)
                    {
                        enemy.GetComponentInParent<EnemyStats>().TakeDamage(playerStats.damage.GetValue() * 3);
                        Debug.Log(playerStats.damage.GetValue() * 3);
                    }
                    else if (primaryAttackState.comboCounter == 3)
                    {
                        enemy.GetComponentInParent<EnemyStats>().TakeDamage(playerStats.damage.GetValue() * 4);
                        Debug.Log(playerStats.damage.GetValue() * 4);
                    }
                }
                else
                {
                    enemy.GetComponentInParent<EnemyStats>().TakeDamage(playerStats.damage.GetValue() * 5);
                    Debug.Log(playerStats.damage.GetValue() * 5);
                }
            }
        }
    }
    public void BeDamaged(int _damage)
    {
        if(!isKnocked)
        {
            stateMachine.ChangeState(hurtState);
            playerStats.TakeDamage(_damage);
        }
    }
    private void FinishAnimation() => stateMachine.currentState.SetFinishAnimationEvent();
    public bool GroundDetected() => Physics2D.BoxCast(mainBox.bounds.center, boxSize, 0f, Vector2.down, groundCheckDistance, whatIsGround);
    
    protected override void OnDrawGizmos()
    {
        Gizmos.DrawCube(mainBox.bounds.center - new Vector3(0f, groundCheckDistance, 0f), boxSize);
        Gizmos.DrawWireSphere(enemyCheck.position, radiusEnemyDetected);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && stateMachine.currentState != dashState && stateMachine.currentState != airDashState)
        {
            AudioManager.instance.playerSFX(8);
            BeDamaged(5);
        }
    }
}
