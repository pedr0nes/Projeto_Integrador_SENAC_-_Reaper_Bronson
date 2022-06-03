using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Bat Class derives from the Enemy Class
public class Bat : Enemy
{
    //Bat State Variables
    #region State Variables 
    public BatStateIdle IdleState { get; private set; }
    public BatStateFly FlyState { get; private set; }
    public BatStateAttack AttackState { get; private set; }
    public BatStateDead DeadState { get; private set; }
    #endregion

    //References needed to be shown in Unity Inspector
    #region Reference Variables

    [Header("Transform References")]                                //References to specific points in space
    //[SerializeField] public Transform attackDetectionPoint;
    [SerializeField] public Transform detectionAreaPoint1;
    [SerializeField] public Transform detectionAreaPoint2;
    [SerializeField] private Transform[] spawners = new Transform[4];


    [Header("Script References")]                                  //Scripts
    [SerializeField] public BatEventManager batEventManager;
    [SerializeField] public BatAnimationEvents batAnimationEvents;
    [SerializeField] public BatSFX batSFX;

    [Header("Data References")]                                    //Data and scriptable objects
    [SerializeField] public BatData batData;


    [Header("Prefab References")]                                  //Prefabs
    [SerializeField] private GameObject sonicWavePrefab;

    #endregion

    //Unity MonoBehaviour Methods
    #region Unity Callback Methods

    protected override void Awake()
    {
        base.Awake();
        //Bat States Instantiation
        #region Bat States Instantiation

        IdleState = new BatStateIdle(this, StateMachine, batData, "idle");
        FlyState = new BatStateFly(this, StateMachine, batData, "fly");
        AttackState = new BatStateAttack(this, StateMachine, batData, "attack");
        DeadState = new BatStateDead(this, StateMachine, batData, "dead");

        #endregion

    }

    protected override void Start()
    {
        base.Start();

        //Variable attribuition
        batEventManager = GetComponent<BatEventManager>();

        //Set bat initial number of lives
        CurrentHealth = batData.lives;

        //Call State Machine Initialization. Initial state should be Fly State
        StateMachine.Initialize(IdleState);

    }

    protected override void Update()
    {
        base.Update();

        //Call State Tick Method
        StateMachine.CurrentState.Tick();

        //Debugs
        Debug.DrawLine(detectionAreaPoint2.position, detectionAreaPoint1.position);
        //Debug.Log(StateMachine.CurrentState);
    }

    private void FixedUpdate()
    {
        //Call State method Physics Tick
        StateMachine.CurrentState.PhysicsTick();
    }

    #endregion

    //Bat Script Methods
    #region Methods
    public void SonicWaveSpawn()                           //Spawns its sonic wave attack in four directions
    {
        //batSFXManager.PlayBatAttackSFX();
        foreach (Transform spawner in spawners)
        {
            Instantiate(sonicWavePrefab, spawner.position, spawner.rotation);
        }
    }

    public void KillBat()                                 //Destroys bat game object
    {
        Destroy(gameObject);
    }

    #endregion
}
