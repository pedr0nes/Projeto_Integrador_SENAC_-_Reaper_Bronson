using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyGFX : MonoBehaviour
{
    [SerializeField] private AIPath aiPath;

    // Update is called once per frame
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
