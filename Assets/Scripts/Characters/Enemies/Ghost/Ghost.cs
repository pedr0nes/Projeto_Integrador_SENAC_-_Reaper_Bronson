using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Enemy
{
    public GhostStateFly FlyState { get; private set; }
    public GhostStateDead DeadState { get; private set; }

    [SerializeField] public GhostData ghostData;
    [SerializeField] public Transform attackDetectionPoint;

    public GhostEventManager ghostEventManager;
    public GhostAnimationEvents ghostAnimationEvents;
    public GhostSFX ghostSFX;


    protected override void Awake()
    {
        base.Awake();
        FlyState = new GhostStateFly(this, StateMachine, ghostData, "fly");
        DeadState = new GhostStateDead(this, StateMachine, ghostData, "dead");
    }

    protected override void Start()
    {
        base.Start();
        StateMachine.Initialize(FlyState);
        CurrentHealth = ghostData.lives;
    }

    protected override void Update()
    {
        base.Update();
        StateMachine.CurrentState.Tick();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsTick();
    }

    public void GhostAttack()
    {
        Collider2D hitPlayer = Physics2D.OverlapCircle(attackDetectionPoint.position, ghostData.attackDetectionRadius, ghostData.whatIsPlayer);
        if (hitPlayer)
        {
            ghostSFX.PlayGhostAttackSFX();
            
            Player player = hitPlayer.GetComponent<Player>();
            player.TakeDamage(ghostData.attackDamage);
        }
    }

    public void KillGhost()
    {
        Destroy(gameObject);
    }

}
