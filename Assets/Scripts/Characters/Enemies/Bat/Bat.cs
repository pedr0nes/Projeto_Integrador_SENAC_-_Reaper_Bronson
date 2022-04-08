using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Enemy
{
    public BatStateIdle IdleState { get; private set; }
    public BatStateFly FlyState { get; private set; }
    public BatStateAttack AttackState { get; private set; }
    public BatStateDead DeadState { get; private set; }

    [SerializeField] public BatData batData;
    [SerializeField] public BatEventManager batEventManager;
    [SerializeField] public BatAnimationEvents batAnimationEvents;
    [SerializeField] public BatSFX batSFX;


    //[SerializeField] public Transform attackDetectionPoint;
    [SerializeField] public Transform detectionAreaPoint1;
    [SerializeField] public Transform detectionAreaPoint2;

    [SerializeField] private Transform[] spawners = new Transform[4];

    [SerializeField] private GameObject sonicWavePrefab;

    protected override void Awake()
    {
        base.Awake();
        IdleState = new BatStateIdle(this, StateMachine, batData, "idle");
        FlyState = new BatStateFly(this, StateMachine, batData, "fly");
        AttackState = new BatStateAttack(this, StateMachine, batData, "attack");
        DeadState = new BatStateDead(this, StateMachine, batData, "dead");


    }

    protected override void Start()
    {
        base.Start();
        batEventManager = GetComponent<BatEventManager>();

        StateMachine.Initialize(FlyState);

        CurrentHealth = batData.lives;

    }

    protected override void Update()
    {
        base.Update();
        StateMachine.CurrentState.Tick();

        Debug.DrawLine(detectionAreaPoint2.position, detectionAreaPoint1.position);

        Debug.Log(StateMachine.CurrentState);
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsTick();
    }

    public void SonicWaveSpawn()
    {

        //batSFXManager.PlayBatAttackSFX();
        foreach (Transform spawner in spawners)
        {
            Instantiate(sonicWavePrefab, spawner.position, spawner.rotation);
        }
    }

    public void KillBat()
    {
        Destroy(gameObject);
    }

}
