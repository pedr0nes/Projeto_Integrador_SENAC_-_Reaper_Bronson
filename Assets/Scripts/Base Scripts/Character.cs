using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Character Base Class
 * The class will serve as base class to all characters subclasses
 * This class substates are:
        - Player
        - Enemy
 */
public abstract class Character : MonoBehaviour
{
    //Variables
    public StateMachine StateMachine { get; private set; }
    public Animator Animator { get; private set; }
    public Rigidbody2D Rigidbody2D { get; private set; }

    //Unity MonoBehaviour Methods
    #region Unity Callback Methods
    protected virtual void Awake()
    {
        //State Machine instantiation
        StateMachine = new StateMachine();
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
     /* Component atribuition
      * Rigidbody component should be in parent object
      * Animator component should be in a child object called Graphics
      */
        Animator = GetComponentInChildren<Animator>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    #endregion
}
