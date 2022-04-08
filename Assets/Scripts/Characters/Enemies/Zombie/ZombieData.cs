using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "newZombieData", menuName = "Data/Zombie Data/Base Data")]
public class ZombieData : CharacterData
{
    [Header("Health")]
    public int lives = 5;


    [Header("Idle State")]
    public float minIdleTime = 1f;
    public float maxIdleTime = 10f;


    [Header("Walk State")]
    public float movementVelocity = 10f;
    public float minWalkTime = 5f;
    public float maxWalkTime = 10f;

    [Header("Attack State")]
    public int attackDamage = 1;

    [Header("Check Variables")]
    public float groundCheckLenght = 0.3f;
    public float attackDetectionRadius = 1f;
    public LayerMask whatIsGround;
    public LayerMask whatIsEnemy;
    public LayerMask whatIsPlayer;
}
