using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newBatData", menuName = "Data/Bat Data/Base Data")]

//Class to store Bat Enemy Data values. Scriptable Object. Values can be changed via Unity interface.
public class BatData : CharacterData
{
    #region Health and Environmental Damage
    [Header("Health")]

    [Tooltip("Bat initial number of lives [int]")]
    public int lives = 5;
    #endregion

    #region Particular State Variables

    #region Idle State
    //[Header("Idle State")]

    //public float minIdleTime = 1f;
    //public float maxIdleTime = 10f;
    #endregion

    #region Fly State
    [Header("Fly State")]

    [Tooltip("Time in seconds between bat attacks [float]")]
    public float timeBetweenAttacks = 2f;



    //public float movementVelocity = 10f;
    //public float minWalkTime = 5f;
    //public float maxWalkTime = 10f;
    #endregion

    #region Attack State
    [Header("Attack State")]

    [Tooltip("Bat attack damage (lives taken per attack) [int]")]
    public int attackDamage = 1;
    #endregion

    #endregion

    #region Check Variables
    [Header("Check Variables")]

    [Tooltip("Determines which objects the bat will identify as Player [Layer Mask]")]
    public LayerMask whatIsPlayer;


    //public float groundCheckLenght = 0.3f;
    //public float attackDetectionRadius = 1f;
    //public LayerMask whatIsGround;
    //public LayerMask whatIsEnemy;
    #endregion
}
