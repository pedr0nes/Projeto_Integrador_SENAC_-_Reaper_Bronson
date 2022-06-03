using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newGhostData", menuName = "Data/Ghost Data/Base Data")]

//Class to store Ghost Enemy Data values. Scriptable Object. Values can be changed via Unity interface.
public class GhostData : CharacterData
{
    #region Health and Environmental Damage
    [Header("Health")]

    [Tooltip("Ghost initial number of lives [int]")]
    public int lives = 1;

    #endregion

    #region Particular State Variables
    [Header("Fly State")]

    [Tooltip("Ghost attack damage (lives taken per attack) [int]")]
    public int attackDamage = 1;

    #endregion

    #region Check Variables
    [Header("Check Variables")]

    [Tooltip("Attack detection check radius [float]")]
    public float attackDetectionRadius = 1f;

    [Tooltip("Determines which objects the ghost will identify as Player [Layer Mask]")]
    public LayerMask whatIsPlayer;




    //public LayerMask whatIsGround;
    //public LayerMask whatIsEnemy;
    #endregion
}
