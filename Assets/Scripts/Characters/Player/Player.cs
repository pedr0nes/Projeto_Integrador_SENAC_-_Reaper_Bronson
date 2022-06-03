using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Player Class derives from the Character Class
public class Player : Character 
{
    //Player State variables
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

    //References needed to be shown in Unity Inspector
    #region Reference Variables

    [Header("Transform References")]                                //References to specific points in space
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform gunMuzzle;
    [SerializeField] public Transform meleeAttackPoint;
    [SerializeField] private Transform[] gunShootingPoints = new Transform[3];

    [Header("Script References")]                                  //Scripts
    [SerializeField] public PlayerAnimationEvents playerAnimationEvents;
    [SerializeField] public PlayerEventManager playerEventManager;
    [SerializeField] public InputHandler playerInputHandler;
    [SerializeField] public PlayerSFX playerSFX;

    [Header("Data References")]                                    //Data and scriptable objects
    [SerializeField] private CharacterData characterData;
    [SerializeField] private PlayerData playerData;

    [Header("Prefab References")]                                  //Prefabs
    [SerializeField] private GameObject gunBulletPrefab;
                                                                   



    #endregion

    //Other variables of different types
    #region Other Variables and Properties                       


    public InputHandler InputHandler { get; private set; }
    public Vector2 CurrentVelocity { get; private set; }
    public int FacingDirection { get; private set; }
    public int CurrentHealth { get; private set; }
    public bool IsImmune { get; set; }

    #endregion

    //Unity Methods
    #region Unity Methods

    protected override void Awake()
    {
        base.Awake();

        //Player States Instantiation
        #region Player States Instantiation
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
        #endregion
    }

    protected override void Start()
    {
        base.Start();

        //Variable attribuition
        InputHandler = GetComponent<InputHandler>();
        playerAnimationEvents = GetComponentInChildren<PlayerAnimationEvents>();

        //Set player initial number of lives
        CurrentHealth = playerData.lives;

        //Set initial direction player is facing (1 = right and -1 = left). By default it should be the right direction        
        FacingDirection = 1;

        //Set condition that makes player immune to damage. By default it should be false.
        IsImmune = false;

        //Call State Machine Initialization. Initial state should be Idle State
        StateMachine.Initialize(IdleState);
    }

    protected override void Update()
    {
        //Call State Tick Method
        StateMachine.CurrentState.Tick();

        //Call Method that makes player susceptible to trap damage
        TrapDamage();

        Debug.Log(StateMachine.CurrentState);
    }

    private void FixedUpdate()
    {

        //Call State method Physics Tick
        StateMachine.CurrentState.PhysicsTick();

        //Store current velocity in a variable for checks
        CurrentVelocity = Rigidbody2D.velocity;
    }

    #endregion

    //Player Script Methods
    #region Methods

    #region Set Methods

    public void SetHorizontalVelocity(float velocity)           //Set horizontal velocity based on input and Player Data value                              
    {
        Rigidbody2D.velocity = new Vector2(velocity, Rigidbody2D.velocity.y);
        //newVelocity.Set(velocity, CurrentVelocity.y);
        //Rigidbody2D.velocity = newVelocity;
        //CurrentVelocity = newVelocity;
    }

    public void SetVerticalVelocity(float velocity)             //Set vertical velocity based on input and Player Data value    
    {
        Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, velocity);
        //newVelocity.Set(CurrentVelocity.x, velocity);
        //Rigidbody2D.velocity = newVelocity;
        //CurrentVelocity = newVelocity;
    }

    public void ShootGun(int aux)                               //Makes player shoot gun and instantiates bullet prefab in the direction it is aiming. An animation event will set the aux variable.
    {
        Instantiate(gunBulletPrefab, gunShootingPoints[aux].position, gunShootingPoints[aux].rotation);
        
        playerSFX.PlayGunShotSFX();
    }

    public void MeleeAttack()                                   //Makes player call the melee attack and set damage to enemies that were hit                     
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(meleeAttackPoint.position, playerData.meleeAttackDetectionRadius, playerData.whatIsEnemy);

        foreach (Collider2D enemyHit in hitEnemies)
        {
            Enemy enemy = enemyHit.GetComponent<Enemy>();
            playerSFX.PlayWeaponHitSFX();
            enemy.TakeDamage(playerData.meleeAttackDamage);
        }

    }

    public void MeleeDownPushback(float force)                  //Makes player suffer a movement pushback when it hits an enemy
    {
        //this method is a test for now. It is not being called
        
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(meleeAttackPoint.position, playerData.meleeAttackDetectionRadius, playerData.whatIsEnemy);
        if(hitEnemies.Length > 0)
        {
            Rigidbody2D.AddForce(new Vector2(0f, 1f) * force, ForceMode2D.Impulse);
        }

        
    }

    public void TakeDamage(int damage)                          //Method that calls an event that makes player loose lives when hit if it is not immune
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

    private void TrapDamage()                                   //Method that calls trap damage when player hits objects with the layer Trap
    {
        if (GetComponent<Collider2D>().IsTouchingLayers(playerData.whatIsTrap))
        {
            TakeDamage(playerData.damageInTraps);
        }
    }

    public void KillPlayer()                                    //Method that destroys player game object
    {
        Destroy(gameObject);
    }

    public void SetRecoilImpulse(float force)                   //Makes player suffer a movement pushback when it shoots the gun.       NOTE: Can be improved
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

    public void CheckIfShouldFlip(int velocity)                  //Checks which horizontal direction player is facing or moving to flip the sprite properly
    {
        if (velocity != 0 && velocity != FacingDirection)
        {
            FlipSprite();
        }
    }

    public bool CheckIfGrounded()                                //Checks if player is touching ground (objects with the layer ground)
    {
        return Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius, playerData.whatIsGround);
    }

    #endregion

    #region Other Methods
    private void FlipSprite()                                    //Flips player sprite if check calls it
    {
        FacingDirection *= -1;
        transform.Rotate(0f, 180f, 0f);
    }


    #endregion

    #endregion
}
