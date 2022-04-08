using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newGhostData", menuName = "Data/Ghost Data/Base Data")]
public class GhostData : CharacterData
{
    [Header("Health")]
    public int lives = 1;


    [Header("Fly State")]
    public int attackDamage = 1;

    [Header("Check Variables")]

    public float attackDetectionRadius = 1f;
    public LayerMask whatIsGround;
    public LayerMask whatIsEnemy;
    public LayerMask whatIsPlayer;

}
