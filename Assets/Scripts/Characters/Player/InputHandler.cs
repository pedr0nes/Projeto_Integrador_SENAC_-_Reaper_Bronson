using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] Player player;
    
    public float HorizontalInput { get; private set; }
    public float VerticalInput { get; private set; }
    //public bool IsJumping { get; private set; }

    public delegate void InputAction();
    
    public event InputAction OnJumpAction;
    public event InputAction OnJumpSustainAction;



    public event InputAction OnFireAction;

    public event InputAction OnWalkAction;
    public event InputAction OnStopAction;

    public event InputAction OnGunOn;
    public event InputAction OnGunOff;




    // Update is called once per frame
    void Update()
    {
        HorizontalInput = Input.GetAxisRaw("Horizontal");
        VerticalInput = Input.GetAxisRaw("Vertical");




        if(HorizontalInput != 0)
        {
            if(OnWalkAction != null)
            {
                OnWalkAction();
            }
        }

        if (HorizontalInput == 0)
        {
            if (OnStopAction != null)
            {
                OnStopAction();
            }
        }


        if (Input.GetButtonDown("Jump"))
        {
            if(OnJumpAction != null)
            {
                OnJumpAction();
            }
        }

        if(Input.GetButton("Jump"))
        {
            if(OnJumpSustainAction != null)
            {
                OnJumpSustainAction();
            }
        }



        if(Input.GetButtonDown("Fire1"))
        {
            if(OnFireAction != null)
            {
                OnFireAction();
            }
        }

        if (Input.GetButtonDown("Fire2"))
        {
            if (OnGunOn != null)
            {
                OnGunOn();
                player.playerSFX.PlayDrawGunSFX();
            }
        }

        if (Input.GetButtonUp("Fire2"))
        {
            if (OnGunOff != null)
            {
                OnGunOff();
            }
        }




        //Debug.Log(IsJumping);


    }

    //public void ResetJumpCondition() => IsJumping = false;



}
