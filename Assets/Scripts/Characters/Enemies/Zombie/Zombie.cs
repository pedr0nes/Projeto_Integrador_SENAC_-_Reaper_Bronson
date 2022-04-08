using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Enemy
{

    public ZombieStateSpawn SpawnState { get; private set; }
    public ZombieStateIdle IdleState { get; private set; }
    public ZombieStateWalk WalkState { get; private set; }
    public ZombieStateAttack AttackState { get; private set; }
    public ZombieStateDead DeadState { get; private set; }



    [SerializeField] public ZombieData zombieData;
    

    [SerializeField] public Transform groundDetectionPoint;
    [SerializeField] public Transform obstacleDetectionPoint;
    [SerializeField] public Transform attackDetectionPoint;


    
    public Vector2 CurrentVelocity { get; private set; }
    public int FacingDirection { get; private set; }

    public ZombieEventManager zombieEventManager;
    public ZombieAnimationEvents zombieAnimationEvents;
    public ZombieSFX zombieSFX;


    protected override void Awake()
    {
        base.Awake();
        SpawnState = new ZombieStateSpawn(this, StateMachine, zombieData, "spawn");
        IdleState = new ZombieStateIdle(this, StateMachine, zombieData, "idle");
        WalkState = new ZombieStateWalk(this, StateMachine, zombieData, "walk");
        AttackState = new ZombieStateAttack(this, StateMachine, zombieData, "attack");
        DeadState = new ZombieStateDead(this, StateMachine, zombieData, "dead");

    }

    protected override void Start()
    {
        base.Start();
        StateMachine.Initialize(SpawnState);
        CurrentHealth = zombieData.lives;

        zombieEventManager = GetComponent<ZombieEventManager>();
        zombieAnimationEvents = GetComponentInChildren<ZombieAnimationEvents>();
        zombieSFX = GetComponentInChildren<ZombieSFX>();
    FacingDirection = -1;
    }

    protected override void Update()
    {
        base.Update();
        StateMachine.CurrentState.Tick();

        

    }



    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsTick();

        CurrentVelocity = Rigidbody2D.velocity;

    }

    public void SetHorizontalVelocity(float velocity)
    {
        Rigidbody2D.velocity = new Vector2(velocity, Rigidbody2D.velocity.y);
    }

    public void MeleeAttack()
    {
        Collider2D hitPlayer = Physics2D.OverlapCircle(attackDetectionPoint.position, zombieData.attackDetectionRadius, zombieData.whatIsPlayer);
        if(hitPlayer)
        {
            Player player = hitPlayer.GetComponent<Player>();
            player.TakeDamage(zombieData.attackDamage);
        }
    }

    public void KillZombie()
    {
        Destroy(gameObject);
    }


    public void CheckIfShouldFlip(int velocity)
    {
        if (velocity != 0 && velocity != FacingDirection)
        {
            FlipSprite();
        }
    }

    public bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(groundDetectionPoint.position, zombieData.groundCheckLenght, zombieData.whatIsGround);
    }

    public void FlipSprite()
    {
        FacingDirection *= -1;
        transform.Rotate(0f, 180f, 0f);
    }





    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(groundDetectionPoint.position, zombieData.groundCheckLenght);
    }


}

