using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]

//Class to store Player Data values. Scriptable Object. Values can be changed via Unity interface.
public class PlayerData : CharacterData
{
    #region Health and Environmental Damage
    [Header("Health")]

    [Tooltip("Player initial number of lives [int]")]
    public int lives = 5;                           //Player initial lives [int]
    
    [Tooltip("Damage taken by players in traps (number of lives lost [int])")]
    public int damageInTraps = 10;                  //Damage taken by player in traps (number of lives lost) [int]

    #endregion

    #region Particular State Variables

    #region Walk State
    [Header("Walk State")]
    
    [Tooltip("Player movement velocity (horizontal axis) to be set in Rigidbody component [float]")]
    public float movementVelocity = 10f;            //Player movement velocity (horizontal axis) to be set in Rigidbody component [float]

    #endregion

    #region Jump State
    [Header("Jump State")]

    [Tooltip("Player jump velocity (vertical axis) to be set in Rigidbody component [float]")]
    public float jumpVelocity = 10f;                //Player jump velocity (vertical axis) to be set in Rigidbody component [float]

    [Tooltip("Player movement velocity (horizontal axis) while in air [float]")]
    public float airMovementVelocity = 5f;          //Player movement velocity (horizontal axis) while in air [float]

    [Tooltip("How much time (in seconds) the jump button can be held down to keep player jumping higher [float]")]
    public float jumpSustainTime = 0.5f;            //How much time (in seconds) the jump button can be held down to keep player jumping higher [float]
    #endregion

    #region Melee State
    [Header("Melee State")]
    
    [Tooltip("Melee attack radius [float]")]
    public float meleeAttackDetectionRadius = 1f;  //Player melee attack radius [float]

    [Tooltip("Melee attack damage [int]")]
    public int meleeAttackDamage = 1;              //Player melee attack damage [int]

    [Tooltip("Melee attack pushback [float]")]
    public float meleePushback = 20f;              //Player melee attack pushback [float]
    #endregion

    #region Gun States
    [Header("Gun States")]

    [Tooltip("Delay time in seconds after shooting for player be able to walk again carrying gun [float]")]
    public float afterShootDelay = 0.3f;  //Player delay time after shooting [float]

    [Tooltip("Applies a pushback force to player game object everytime it shoots")]
    public float gunRecoilPushback = 10f;  //Gun recoil pushback [float]

    #endregion

    #endregion

    #region Check Variables
    [Header("Check Variables")]
    
    [Tooltip("Ground check radius [float]")]
    public float groundCheckRadius = 0.3f;         //Player ground check radius [float]

    [Tooltip("Determines which objects player will identify as Ground [Layer Mask]")]
    public LayerMask whatIsGround;                 //Determines which objects player will identify as Ground [Layer Mask]

    [Tooltip("Determines which objects player will identify as Enemy [Layer Mask]")]
    public LayerMask whatIsEnemy;                  //Determines which objects player will identify as Enemy [Layer Mask]

    [Tooltip("Determines which objects player will identify as Trap [Layer Mask]")]
    public LayerMask whatIsTrap;                   //Determines which objects player will identify as Trap [Layer Mask]
    #endregion

}
