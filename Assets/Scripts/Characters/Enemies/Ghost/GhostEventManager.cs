using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Ghost Event Manager Class
 * Class created to manage in game events related to the Ghost character in a Observer Pattern approach
 * Animation events are managed in a different script
 * All event names are self explanatory
 */

public class GhostEventManager : MonoBehaviour
{
    //Script References
    [SerializeField] public Ghost thisGhost;

    //Event Declaration
    public delegate void GhostAction();
    public GhostAction OnGhostDead;

    #region Unity Methods
    // Start is called before the first frame update
    void Start()
    {
        //Variable attribution
        thisGhost = GetComponent<Ghost>();
    }

    // Update is called once per frame
    void Update()
    {
        //Method Calls
        GhostDeath();
    }

    #endregion

    #region Class Specific Methods

    //Notifies subscribed scripts when this Ghost character health is below 0 (zero)
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
    #endregion
}
