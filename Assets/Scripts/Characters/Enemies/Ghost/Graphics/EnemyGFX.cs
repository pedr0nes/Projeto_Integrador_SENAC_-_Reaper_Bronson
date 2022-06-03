using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

//This is a special script that manages which horizontal direction the Ghost character's sprite is facing.
//It uses the data its AIPath desired velocity on the x axis to make that kind of decision.

public class EnemyGFX : MonoBehaviour
{
    //AIPath script reference
    [SerializeField] private AIPath aiPath;

    //Unity Update
    void Update()
    {
        if(aiPath.desiredVelocity.x >= 0.01f)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if(aiPath.desiredVelocity.x <= 0.01f)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }
}
