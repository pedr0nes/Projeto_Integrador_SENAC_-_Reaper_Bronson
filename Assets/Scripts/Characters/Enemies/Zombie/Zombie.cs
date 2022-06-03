using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Zombie Class derives from the Enemy Class
public class Zombie : Enemy
{
    //Zombie State Variables
    #region State Variables 
    public ZombieStateSpawn SpawnState { get; private set; }
    public ZombieStateIdle IdleState { get; private set; }
    public ZombieStateWalk WalkState { get; private set; }
    public ZombieStateAttack AttackState { get; private set; }
    public ZombieStateDead DeadState { get; private set; }
    #endregion

    //References needed to be shown in Unity Inspector
    #region Reference Variables

    [Header("Transform References")]                               //References to specific points in space
    [SerializeField] public Transform groundDetectionPoint;
    [SerializeField] public Transform obstacleDetectionPoint;
    [SerializeField] public Transform attackDetectionPoint;

    [Header("Script References")]                                  //Scripts
    public ZombieEventManager zombieEventManager;
    public ZombieAnimationEvents zombieAnimationEvents;
    public ZombieSFX zombieSFX;

    [Header("Data References")]                                    //Data and scriptable objects
    [SerializeField] public ZombieData zombieData;

    #endregion

    //Other variables of different types
    #region Other Variables and Properties     
    
    public Vector2 CurrentVelocity { get; private set; }
    public int FacingDirection { get; private set; }

    #endregion

    //Unity Methods
    #region Unity Methods

    protected override void Awake()
    {
        base.Awake();
        //Zombie States Instantiation
        #region Zombie States Instantiation

        SpawnState = new ZombieStateSpawn(this, StateMachine, zombieData, "spawn");
        IdleState = new ZombieStateIdle(this, StateMachine, zombieData, "idle");
        WalkState = new ZombieStateWalk(this, StateMachine, zombieData, "walk");
        AttackState = new ZombieStateAttack(this, StateMachine, zombieData, "attack");
        DeadState = new ZombieStateDead(this, StateMachine, zombieData, "dead");

        #endregion
    }

    protected override void Start()
    {
        base.Start();

        //Variable attribuition
        zombieEventManager = GetComponent<ZombieEventManager>();
        zombieAnimationEvents = GetComponentInChildren<ZombieAnimationEvents>();
        zombieSFX = GetComponentInChildren<ZombieSFX>();

        //Set zombie initial number of lives
        CurrentHealth = zombieData.lives;

        //Set initial direction player is facing (1 = right and -1 = left). By default it should be the left direction  
        FacingDirection = -1;

        //Call State Machine Initialization. Initial state should be Spawn State
        StateMachine.Initialize(SpawnState);
    }

    protected override void Update()
    {
        base.Update();

        //Call State Tick Method
        StateMachine.CurrentState.Tick();
    }

    private void FixedUpdate()
    {
        //Call State method Physics Tick
        StateMachine.CurrentState.PhysicsTick();

        //Store current velocity in a variable for checks
        CurrentVelocity = Rigidbody2D.velocity;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(groundDetectionPoint.position, zombieData.groundCheckLenght);
    }

    #endregion

    //Zombie Script Methods
    #region Zombie Script Specific Methods

    public void SetHorizontalVelocity(float velocity)        //Set horizontal velocity based on Zombie Data value 
    {
        Rigidbody2D.velocity = new Vector2(velocity, Rigidbody2D.velocity.y);
    }

    public void MeleeAttack()                                //Search for nearby objects labled 'Player' and calls its 'Take Damage' method
    {
        Collider2D hitPlayer = Physics2D.OverlapCircle(attackDetectionPoint.position, zombieData.attackDetectionRadius, zombieData.whatIsPlayer);
        if(hitPlayer)
        {
            Player player = hitPlayer.GetComponent<Player>();
            player.TakeDamage(zombieData.attackDamage);
        }
    }

    public void KillZombie()                                 //Destroys zombie game object
    {
        Destroy(gameObject);
    }

    public void CheckIfShouldFlip(int velocity)              //Checks which horizontal direction the zombie is facing or moving to flip the sprite properly
    {
        if (velocity != 0 && velocity != FacingDirection)
        {
            FlipSprite();
        }
    }

    public bool CheckIfGrounded()                            //Checks if zombie is touching ground (objects with the layer ground)
    {
        return Physics2D.OverlapCircle(groundDetectionPoint.position, zombieData.groundCheckLenght, zombieData.whatIsGround);
    }

    public void FlipSprite()                                 //Flips zombie sprite if check calls it
    {
        FacingDirection *= -1;
        transform.Rotate(0f, 180f, 0f);
    }


    #endregion
}

