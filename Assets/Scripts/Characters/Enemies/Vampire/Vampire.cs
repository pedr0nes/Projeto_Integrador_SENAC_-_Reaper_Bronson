using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Vampire Class derives from the Enemy Class
public class Vampire : Enemy
{
    //Vampire State Variables
    #region State Variables 
    public VampireStateAppear AppearState { get; private set; }
    public VampireStateDisappear DisappearState { get; private set; }
    public VampireStateIdle IdleState { get; private set; }
    public VampireStateWalk WalkState { get; private set; }
    public VampireStateInvisible InvisibleState { get; private set; }
    public VampireStateAttack AttackState { get; private set; }
    public VampireStateDead DeadState { get; private set; }
    #endregion

    //References needed to be shown in Unity Inspector
    #region Reference Variables

    [Header("Transform References")]                               //References to specific points in space
    [SerializeField] private Transform player;
    [SerializeField] public Transform attackDetectionPoint;
    [SerializeField] public Transform batAttackPoint;

    [Header("Script References")]                                  //Scripts
    [SerializeField] public VampireAnimationEvents vampireAnimationEvents;
    [SerializeField] public VampireEventManager vampireEventManager;
    [SerializeField] public VampireSFX vampireSFX;

    [Header("Data References")]                                    //Data and scriptable objects

    [Header("Prefab References")]                                  //Prefabs
    [SerializeField] private HealthBar healthBar;
    [SerializeField] public VampireData vampireData;

    #endregion

    //Other variables of different types
    #region Other Variables and Properties      

    public bool isFlipped = false;

    #endregion

    //Unity MonoBehaviour Methods
    #region Unity Methods

    protected override void Awake()
    {
        base.Awake();
        //Vampire States Instantiation
        #region Vampire States Instantiation

        AppearState = new VampireStateAppear(this, StateMachine, vampireData, "appear");
        DisappearState = new VampireStateDisappear(this, StateMachine, vampireData, "disappear");
        IdleState = new VampireStateIdle(this, StateMachine, vampireData, "idle");
        WalkState = new VampireStateWalk(this, StateMachine, vampireData, "walk");
        InvisibleState = new VampireStateInvisible(this, StateMachine, vampireData, "invisible");
        AttackState = new VampireStateAttack(this, StateMachine, vampireData, "attack");
        DeadState = new VampireStateDead(this, StateMachine, vampireData, "dead");

        #endregion

    }

    protected override void Start()
    {
        base.Start();

        //Variable attribuition
        vampireAnimationEvents = GetComponentInChildren<VampireAnimationEvents>();

        //Set vampire initial number of lives
        CurrentHealth = vampireData.lives;

        //Set health bar to the maximum value
        healthBar.SetMaxHealth(vampireData.lives);

        //Call State Machine Initialization. Initial state should be Appear State
        StateMachine.Initialize(AppearState);

    }

    protected override void Update()
    {
        base.Update();

        //Call State Tick Method
        StateMachine.CurrentState.Tick();

        //Sets the health bar value to current
        healthBar.SetHealth(CurrentHealth);

        //Debugs
        Debug.Log(StateMachine.CurrentState);
        //transform.LookAt(target.position);
    }

    private void FixedUpdate()
    {
        //Call State method Physics Tick
        StateMachine.CurrentState.PhysicsTick();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(batAttackPoint.position, vampireData.batAttackRadius);
    }

    #endregion

    //Vampire Script Methods
    #region Vampire Script Methods

    public void KillVampire()                                         //Destroys vampire game object
    {
        Destroy(gameObject);
    }

    public void BatAttack()                                           //Search for nearby objects labled 'Player' and calls its 'Take Damage' method
    {
        Collider2D hitPlayer = Physics2D.OverlapCircle(batAttackPoint.position, vampireData.batAttackRadius, vampireData.whatIsPlayer);
        if (hitPlayer)
        {
            Player player = hitPlayer.GetComponent<Player>();
            player.TakeDamage(vampireData.attackDamage);
        }
    }

    public void FollowPlayer()                                        //Makes the Vampire object walk towards the Player object in the Horizontal Axis
    {
        if(player != null)
        {
            Vector2 target = new Vector2(player.position.x, Rigidbody2D.position.y);
            Vector2 newPosition = Vector2.MoveTowards(Rigidbody2D.position, target, vampireData.movementVelocity * Time.fixedDeltaTime);
            Rigidbody2D.MovePosition(newPosition);
        }
    }

    public void LookAtPlayer()                                        //Flips Vampire object's sprite to face the Player object
    {
        if (player != null)
        {
            Vector3 flipped = transform.localScale;
            flipped.z *= -1f;

            if (transform.position.x > player.position.x && isFlipped)
            {
                transform.localScale = flipped;
                transform.Rotate(0f, 180f, 0f);
                isFlipped = false;
            }
            else if (transform.position.x < player.position.x && !isFlipped)
            {
                transform.localScale = flipped;
                transform.Rotate(0f, 180f, 0f);
                isFlipped = true;
            }
        }

    }

    public void CallFlahsDeathEffectCoroutine()                       //Calls the coroutine that flashes the sprite
    {
        StartCoroutine(FlashDeathEffect());
    }

    public void CallChasingPlayerCoroutine()                          //Calls the coroutine that sets and tracks the duration of the period that the Vampire will be following the player object
    {
        StartCoroutine(ChasingPlayerPeriod());
    }

    public void CallIdlePeriodCoroutine()                             //Calls the coroutine that sets and tracks the duration of the period that the Vampire will be idle and visible
    {
        StartCoroutine(IdlePeriod());
    }

    public void CallInvisiblePeriodCoroutine()                        //Calls the coroutine that sets and tracks the duration of the period that the Vampire will be invisible
    {
        StartCoroutine(InvisiblePeriod());
    }

    private IEnumerator FlashDeathEffect()                            //Coroutine that applies a flash effect to the object's sprite for some time
    {
        float flashAmount = 0;
        
        while (flashAmount <= 1)
        {
            spriteRenderer.material.SetFloat("_FlashAmount", flashAmount);
            flashAmount += 0.01f;
            yield return new WaitForSeconds(0.03f);
        }
        yield return null;
    }

    private IEnumerator ChasingPlayerPeriod()                         //Coroutine that sets and keeps track of the period that the Vampire will be following the player object. It also raises a event when it should stop.     
    {
        yield return new WaitForSeconds(Random.Range(vampireData.minWalkTime, vampireData.maxWalkTime));
        if (vampireEventManager.OnChasingEnded != null)
        {
            vampireEventManager.OnChasingEnded();
        }
    }                                           

    private IEnumerator IdlePeriod()                                  //Coroutine that sets and keeps track of the period that the Vampire will be idle. It also raises a event when it should stop.
    {
        yield return new WaitForSeconds(vampireData.idleTime);
        if (vampireEventManager.OnIdleEnded != null)
        {
            vampireEventManager.OnIdleEnded();
        }
    }

    private IEnumerator InvisiblePeriod()                             //Coroutine that sets and keeps track of the period that the Vampire will be invisible. It also raises a event when it should stop.
    {
        yield return new WaitForSeconds(vampireData.invisibleTime);
        if (vampireEventManager.OnInvisibleEnded != null)
        {
            vampireEventManager.OnInvisibleEnded();
        }
    }



    #endregion
}
