using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "newVampireData", menuName = "Data/Vampire Data/Base Data")]
public class VampireData : CharacterData
{
    [Header("Health")]
    public int lives = 5;


    [Header("Idle State")]
    public float idleTime = 10f;


    [Header("Walk State")]
    public float movementVelocity = 10f;

    public float minWalkTime = 2f;
    public float maxWalkTime = 5f;

    [Header("Attack State")]
    public int attackDamage = 1;

    [Header("Invisible State")]
    public float invisibleTime = 5f;

    [Header("Check Variables")]
    public float groundCheckLenght = 0.3f;
    public float attackDetectionRadius = 1f;
    public float batAttackRadius = 1f;

    public LayerMask whatIsGround;
    public LayerMask whatIsEnemy;
    public LayerMask whatIsPlayer;


}
