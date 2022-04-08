using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vampire : Enemy
{

    public VampireStateAppear AppearState { get; private set; }
    public VampireStateDisappear DisappearState { get; private set; }
    public VampireStateIdle IdleState { get; private set; }
    public VampireStateWalk WalkState { get; private set; }
    public VampireStateInvisible InvisibleState { get; private set; }
    public VampireStateAttack AttackState { get; private set; }
    public VampireStateDead DeadState { get; private set; }



    [SerializeField] private Transform player;
    [SerializeField] private HealthBar healthBar;

    [SerializeField] public VampireData vampireData;

    [SerializeField] public Transform attackDetectionPoint;
    [SerializeField] public Transform batAttackPoint;

    [SerializeField] public VampireAnimationEvents vampireAnimationEvents;
    [SerializeField] public VampireEventManager vampireEventManager;
    [SerializeField] public VampireSFX vampireSFX;

    public bool isFlipped = false;

    protected override void Awake()
    {
        base.Awake();
        AppearState = new VampireStateAppear(this, StateMachine, vampireData, "appear");
        DisappearState = new VampireStateDisappear(this, StateMachine, vampireData, "disappear");
        IdleState = new VampireStateIdle(this, StateMachine, vampireData, "idle");
        WalkState = new VampireStateWalk(this, StateMachine, vampireData, "walk");
        InvisibleState = new VampireStateInvisible(this, StateMachine, vampireData, "invisible");
        AttackState = new VampireStateAttack(this, StateMachine, vampireData, "attack");
        DeadState = new VampireStateDead(this, StateMachine, vampireData, "dead");


    }

    protected override void Start()
    {
        base.Start();
        StateMachine.Initialize(AppearState);
        CurrentHealth = vampireData.lives;
        healthBar.SetMaxHealth(vampireData.lives);


        vampireAnimationEvents = GetComponentInChildren<VampireAnimationEvents>();
    }

    protected override void Update()
    {
        base.Update();
        StateMachine.CurrentState.Tick();
        healthBar.SetHealth(CurrentHealth);

        Debug.Log(StateMachine.CurrentState);
        //transform.LookAt(target.position);
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsTick();
    }

    public void KillVampire()
    {
        Destroy(gameObject);
    }


    public void BatAttack()
    {
        Collider2D hitPlayer = Physics2D.OverlapCircle(batAttackPoint.position, vampireData.batAttackRadius, vampireData.whatIsPlayer);
        if (hitPlayer)
        {
            Debug.Log("CACETADA");
            Player player = hitPlayer.GetComponent<Player>();
            player.TakeDamage(vampireData.attackDamage);
        }
    }

    public void FollowPlayer()
    {
        if(player != null)
        {
            Vector2 target = new Vector2(player.position.x, Rigidbody2D.position.y);
            Vector2 newPosition = Vector2.MoveTowards(Rigidbody2D.position, target, vampireData.movementVelocity * Time.fixedDeltaTime);
            Rigidbody2D.MovePosition(newPosition);
        }

        
    }


    public void LookAtPlayer()
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

    public void CallFlahsDeathEffect()
    {
        StartCoroutine(FlashDeathEffect());
    }



    private IEnumerator FlashDeathEffect()
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


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(batAttackPoint.position, vampireData.batAttackRadius);
    }
}
