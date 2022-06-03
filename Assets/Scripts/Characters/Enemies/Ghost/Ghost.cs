using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Ghost Class derives from the Enemy Class
public class Ghost : Enemy
{
    //Ghost State Variables
    #region State Variables 

    public GhostStateFly FlyState { get; private set; }
    public GhostStateDead DeadState { get; private set; }

    #endregion

    //References needed to be shown in Unity Inspector
    #region Reference Variables

    [Header("Transform References")]                               //References to specific points in space
    [SerializeField] public Transform attackDetectionPoint;

    [Header("Script References")]                                  //Scripts
    [SerializeField] public GhostEventManager ghostEventManager;
    [SerializeField] public GhostAnimationEvents ghostAnimationEvents;
    [SerializeField] public GhostSFX ghostSFX;

    [Header("Data References")]                                    //Data and scriptable objects
    [SerializeField] public GhostData ghostData;

    #endregion

    //Unity MonoBehaviour Methods
    #region Unity Callback Methods

    protected override void Awake()
    {
        base.Awake();
        //Ghost States Instantiation
        #region Ghost States Instantiation

        FlyState = new GhostStateFly(this, StateMachine, ghostData, "fly");
        DeadState = new GhostStateDead(this, StateMachine, ghostData, "dead");

        #endregion
    }

    protected override void Start()
    {
        base.Start();

        //Set ghost initial number of lives
        CurrentHealth = ghostData.lives;

        //Call State Machine Initialization. Initial state should be Fly State
        StateMachine.Initialize(FlyState);

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
    }
    #endregion

    //Ghost Script Methods
    #region Methods
    public void GhostAttack()                        //Search for nearby objects labled 'Player' and calls its 'Take Damage' method
    {
        Collider2D hitPlayer = Physics2D.OverlapCircle(attackDetectionPoint.position, ghostData.attackDetectionRadius, ghostData.whatIsPlayer);
        if (hitPlayer)
        {
            ghostSFX.PlayGhostAttackSFX();
            
            Player player = hitPlayer.GetComponent<Player>();
            player.TakeDamage(ghostData.attackDamage);
        }
    }

    public void KillGhost()                         //Destroys ghost game object
    {
        Destroy(gameObject);
    }
    #endregion
}
