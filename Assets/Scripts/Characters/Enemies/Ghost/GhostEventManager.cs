using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEventManager : MonoBehaviour
{
    public Ghost thisGhost;

    public delegate void GhostAction();

    public GhostAction OnGhostDead;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GhostDeath();
    }


    private void GhostDeath()
    {
        
        if (thisGhost.CurrentHealth <= 0)
        {

            if (OnGhostDead != null)
            {
                OnGhostDead();
            }
        }
    }

}
