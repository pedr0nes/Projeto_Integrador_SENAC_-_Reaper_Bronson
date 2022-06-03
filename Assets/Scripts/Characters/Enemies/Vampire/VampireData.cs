using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newVampireData", menuName = "Data/Vampire Data/Base Data")]

//Class to store Vampire Enemy Data values. Scriptable Object. Values can be changed via Unity interface.
public class VampireData : CharacterData
{
    #region Health and Environmental Damage
    [Header("Health")]
    [Tooltip("Vampire initial number of lives [int]")]
    public int lives = 5;
    #endregion

    #region Particular State Variables

    #region Idle State
    [Header("Idle State")]

    [Tooltip("Time in seconds that the vampire will stay idle [float]")]
    public float idleTime = 10f;
    #endregion

    #region Walk State
    [Header("Walk State")]

    [Tooltip("Vampire movement velocity [float]")]
    public float movementVelocity = 10f;

    [Tooltip("Minimum time in seconds that the vampire will be walking [float]")]
    public float minWalkTime = 2f;

    [Tooltip("Maximum time in seconds that the vampire will be walkinge [float]")]
    public float maxWalkTime = 5f;
    #endregion

    #region Attack State
    [Header("Attack State")]

    [Tooltip("Vampíre attack damage (lives taken per attack) [int]")]
    public int attackDamage = 1;
    #endregion

    #region Invisible State
    [Header("Invisible State")]

    [Tooltip("Time in seconds that the vampire will stay invisible [float]")]
    public float invisibleTime = 5f;
    #endregion

    #endregion

    #region Check Variables
    [Header("Check Variables")]

    [Tooltip("Attack detection check radius [float]")]
    public float attackDetectionRadius = 1f;

    [Tooltip("Attack damage radius [float]")]
    public float batAttackRadius = 1f;

    [Tooltip("Determines which objects the vampire will identify as Player [Layer Mask]")]
    public LayerMask whatIsPlayer;




    //public LayerMask whatIsGround;
    //public LayerMask whatIsEnemy;
    //public float groundCheckLenght = 0.3f;
    #endregion

}
