using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Input Handler Class is derived from Unity MonoBehaviour
 * Class created to manage all player gameplay inputs
 * The script is attached to the 'Player' main game object
 * Inputs are handled via an Observer Pattern approach
 * All event names are self explanatory
 */


public class InputHandler : MonoBehaviour
{
    #region Variable Declaration

    //Player script reference
    [SerializeField] Player player;
    
    //Float variable to store horizontal input
    public float HorizontalInput { get; private set; }

    //Float variable to store vertical input
    public float VerticalInput { get; private set; }

    #endregion

    #region Event Declaration

    //Input delegate
    public delegate void InputAction();

    //Input events
    #region Input events
    public event InputAction OnJumpAction;
    public event InputAction OnJumpSustainAction;

    public event InputAction OnFireAction;

    public event InputAction OnWalkAction;
    public event InputAction OnStopAction;

    public event InputAction OnGunOn;
    public event InputAction OnGunOff;

    #endregion

    #endregion

    //Unity MonoBehaviour Methods
    #region Unity Callback Methods

    // Update is called once per frame
    void Update()
    {
        //Keeps track of Horizontal and Vertical input
        HorizontalInput = Input.GetAxisRaw("Horizontal");
        VerticalInput = Input.GetAxisRaw("Vertical");

        #region Input Event Calls

        //Player is walking
        if (HorizontalInput != 0)
        {
            if(OnWalkAction != null)
            {
                OnWalkAction();
            }
        }

        //Player stoped walking
        if (HorizontalInput == 0)
        {
            if (OnStopAction != null)
            {
                OnStopAction();
            }
        }

        //Player jumped
        if (Input.GetButtonDown("Jump"))
        {
            if(OnJumpAction != null)
            {
                OnJumpAction();
            }
        }

        //Player is jumping higher
        if (Input.GetButton("Jump"))
        {
            if(OnJumpSustainAction != null)
            {
                OnJumpSustainAction();
            }
        }

        //Player is attacking (melee/shoot)
        if(Input.GetButtonDown("Fire1"))
        {
            if(OnFireAction != null)
            {
                OnFireAction();
            }
        }

        //Player gun active
        if (Input.GetButtonDown("Fire2"))
        {
            if (OnGunOn != null)
            {
                OnGunOn();
                player.playerSFX.PlayDrawGunSFX();
            }
        }

        //Player gun inactive
        if (Input.GetButtonUp("Fire2"))
        {
            if (OnGunOff != null)
            {
                OnGunOff();
            }
        }

        #endregion

    }

    #endregion

}
