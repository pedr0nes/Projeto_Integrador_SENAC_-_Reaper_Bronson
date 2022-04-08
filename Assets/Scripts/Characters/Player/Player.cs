using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    #region State Variables

    public PlayerStateInAir InAirState { get; private set; }
    public PlayerStateGround GroundState { get; private set; }
    public PlayerStateAbility AbilityState { get; private set; }
    public PlayerStateDead DeadState { get; private set; }
    public PlayerStateWin WinState { get; private set; }

    public PlayerStateIdle IdleState { get; private set; }
    public PlayerStateWalk WalkState { get; private set; }
    public PlayerStateLand LandState { get; private set; }
    public PlayerStateJump JumpState { get; private set; }
    public PlayerStateMelee MeleeAttackState { get; private set; }
    public PlayerStateGunIdle GunIdleState { get; private set; }
    public PlayerStateGunShoot GunShootState { get; private set; }
    public PlayerStateGunWalk GunWalkState { get; private set; }

    public PlayerStateHurt HurtState { get; private set; }


    #endregion

    #region Transform Variables
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform gunMuzzle;
    [SerializeField] public Transform meleeAttackPoint;

    [SerializeField] private Transform[] gunShootingPoints = new Transform[3];

    #endregion

    #region Other Variables
    [SerializeField] private CharacterData characterData;
    [SerializeField] private PlayerData playerData;

    public InputHandler InputHandler { get; private set; }

    public Vector2 CurrentVelocity { get; private set; }
    
    public int FacingDirection { get; private set; }
    
    public int CurrentHealth { get; private set; }

    public bool IsImmune { get; set; }

    #endregion

    #region Component References

    [SerializeField] public PlayerAnimationEvents playerAnimationEvents;
    [SerializeField] public PlayerEventManager playerEventManager;
    [SerializeField] public InputHandler playerInputHandler;
    [SerializeField] private GameObject gunBulletPrefab;
    [SerializeField] public PlayerSFX playerSFX;


    #endregion


    #region Unity Callback Methods
    protected override void Awake()
    {
        base.Awake();

        //Player States Instantiation

        //Super States
        GroundState = new PlayerStateGround(this, StateMachine, playerData, "ground");
        InAirState = new PlayerStateInAir(this, StateMachine, playerData, "inAir");
        AbilityState = new PlayerStateAbility(this, StateMachine, playerData, "ability");
        DeadState = new PlayerStateDead(this, StateMachine, playerData, "dead");
        WinState = new PlayerStateWin(this, StateMachine, playerData, "win");

        //Ground Sub States
        IdleState = new PlayerStateIdle(this, StateMachine, playerData, "idle");
        WalkState = new PlayerStateWalk(this, StateMachine, playerData, "walk");
        LandState = new PlayerStateLand(this, StateMachine, playerData, "land");

        //Ability Sub States
        JumpState = new PlayerStateJump(this, StateMachine, playerData, "jump");
        MeleeAttackState = new PlayerStateMelee(this, StateMachine, playerData, "melee");
        HurtState = new PlayerStateHurt(this, StateMachine, playerData, "hurt");
        GunIdleState = new PlayerStateGunIdle(this, StateMachine, playerData, "gunIdle");
        GunShootState = new PlayerStateGunShoot(this, StateMachine, playerData, "gunShoot");
        GunWalkState = new PlayerStateGunWalk(this, StateMachine, playerData, "gunWalk");
        
    }


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        InputHandler = GetComponent<InputHandler>();
        playerAnimationEvents = GetComponentInChildren<PlayerAnimationEvents>();

        CurrentHealth = playerData.lives;

        FacingDirection = 1;
        IsImmune = false;

        
        StateMachine.Initialize(IdleState);
    }

    // Update is called once per frame
    protected override void Update()
    {
        StateMachine.CurrentState.Tick();
        TrapDamage();


        
        Vector2 teste;
        teste = gunMuzzle.localPosition;
        teste.Normalize();
        //Debug.Log("Current Livers: " + CurrentHealth);

        //Debug.Log(teste);
        //Debug.Log(StateMachine.CurrentState);
        //Debug.Log("is in the air =" + PlayerStateInAir.isInTheAir);
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsTick();

        CurrentVelocity = Rigidbody2D.velocity;
    }
    #endregion

    #region Set Methods
    public void SetHorizontalVelocity(float velocity)
    {
        Rigidbody2D.velocity = new Vector2(velocity, Rigidbody2D.velocity.y);
        //newVelocity.Set(velocity, CurrentVelocity.y);
        //Rigidbody2D.velocity = newVelocity;
        //CurrentVelocity = newVelocity;
    }

    public void SetVerticalVelocity(float velocity)
    {
        Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, velocity);
        //newVelocity.Set(CurrentVelocity.x, velocity);
        //Rigidbody2D.velocity = newVelocity;
        //CurrentVelocity = newVelocity;
    }

    public void ShootGun(int aux)
    {
        Instantiate(gunBulletPrefab, gunShootingPoints[aux].position, gunShootingPoints[aux].rotation);
        
        playerSFX.PlayGunShotSFX();
    }

    public void MeleeAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(meleeAttackPoint.position, playerData.meleeAttackDetectionRadius, playerData.whatIsEnemy);

        foreach (Collider2D enemyHit in hitEnemies)
        {
            Enemy enemy = enemyHit.GetComponent<Enemy>();
            playerSFX.PlayWeaponHitSFX();
            enemy.TakeDamage(playerData.meleeAttackDamage);
            
        }

    }

    public void MeleeDownPushback(float force)
    {
        //Esse método é um teste
        
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(meleeAttackPoint.position, playerData.meleeAttackDetectionRadius, playerData.whatIsEnemy);
        if(hitEnemies.Length > 0)
        {
            Rigidbody2D.AddForce(new Vector2(0f, 1f) * force, ForceMode2D.Impulse);
        }

        
    }


    public void TakeDamage(int damage)
    {
        if(!IsImmune)
        {
            CurrentHealth -= damage;
            if (playerEventManager.OnPlayerHurt != null)
            {
                playerEventManager.OnPlayerHurt();
            }
        }

    }

    private void TrapDamage()
    {
        if (GetComponent<Collider2D>().IsTouchingLayers(playerData.whatIsTrap))
        {
            TakeDamage(playerData.damageInTraps);
        }
    }


    public void KillPlayer()
    {
        Destroy(gameObject);
    }



    public void SetRecoilImpulse(float force)
    {
        if(InputHandler.VerticalInput > 0)
        {
            Rigidbody2D.AddForce(new Vector2(0f, -1f) * force/2f, ForceMode2D.Impulse);
        }
        else if(InputHandler.VerticalInput < 0)
        {
            Rigidbody2D.AddForce(new Vector2(0f, 1f) * force, ForceMode2D.Impulse);
        }
        else
        {
            Vector2 normalizedRecoil = gunMuzzle.position - this.transform.position;
            normalizedRecoil.Normalize();
            Rigidbody2D.AddForce(new Vector2(-normalizedRecoil.x, normalizedRecoil.y) * force, ForceMode2D.Impulse);
        }

        //Vector2 normalizedRecoil = gunMuzzle.position - this.transform.position;
        //normalizedRecoil.Normalize();
        //Rigidbody2D.AddForce(new Vector2(-normalizedRecoil.x, normalizedRecoil.y) * force, ForceMode2D.Impulse);
    }


    #endregion

    #region Check Methods

    public void CheckIfShouldFlip(int velocity)
    {
        if (velocity != 0 && velocity != FacingDirection)
        {
            FlipSprite();
        }
    }

    public bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius, playerData.whatIsGround);
    }

    #endregion

    #region Other Methods
    private void FlipSprite()
    {
        FacingDirection *= -1;
        transform.Rotate(0f, 180f, 0f);
    }


    #endregion





}
