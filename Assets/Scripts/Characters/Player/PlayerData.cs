using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : CharacterData
{
    [Header("Health")]
    public int lives = 5;
    public int damageInTraps = 10;


    [Header("Walk State")]
    public float movementVelocity = 10f;

    [Header("Jump State")]
    public float jumpVelocity = 10f;
    public float airMovementVelocity = 5f;
    public float jumpSustainTime = 0.5f;

    [Header("Melee State")]
    public float meleeAttackDetectionRadius = 1f;
    public int meleeAttackDamage = 1;


    [Header("Check Variables")]
    public float groundCheckRadius = 0.3f;
    public LayerMask whatIsGround;
    public LayerMask whatIsEnemy;
    public LayerMask whatIsTrap;

}
