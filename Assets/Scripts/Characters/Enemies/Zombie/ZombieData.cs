using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newZombieData", menuName = "Data/Zombie Data/Base Data")]

//Class to store Zombie Enemy Data values. Scriptable Object. Values can be changed via Unity interface.
public class ZombieData : CharacterData
{
    #region Health and Environmental Damage
    [Header("Health")]

    [Tooltip("Zombie initial number of lives [int]")]
    public int lives = 5;
    #endregion

    #region Particular State Variables

    #region Idle State
    [Header("Idle State")]

    [Tooltip("Minimum time in seconds that zombie will stay idle [float]")]
    public float minIdleTime = 1f;

    [Tooltip("Maximum time in seconds that zombie will stay idle [float]")]
    public float maxIdleTime = 10f;
    #endregion

    #region Walk State
    [Header("Walk State")]

    [Tooltip("Zombie movement velocity [float]")]
    public float movementVelocity = 10f;

    [Tooltip("Minimum time in seconds that zombie will be walking [float]")]
    public float minWalkTime = 5f;

    [Tooltip("Maximum time in seconds that zombie will be walkinge [float]")]
    public float maxWalkTime = 10f;
    #endregion

    #region Attack State
    [Header("Attack State")]

    [Tooltip("Zombie attack damage (lives taken per attack) [int]")]
    public int attackDamage = 1;
    #endregion

    #endregion

    #region Check Variables
    [Header("Check Variables")]

    [Tooltip("Ground check radius [float]")]
    public float groundCheckLenght = 0.3f;

    [Tooltip("Attack detection check radius [float]")]
    public float attackDetectionRadius = 1f;

    [Tooltip("Determines which objects the zombie will identify as Ground [Layer Mask]")]
    public LayerMask whatIsGround;

    [Tooltip("Determines which objects the zombie will identify as other enemies like itself [Layer Mask]")]
    public LayerMask whatIsEnemy;

    [Tooltip("Determines which objects the zombie will identify as Player [Layer Mask]")]
    public LayerMask whatIsPlayer;

    #endregion
}
