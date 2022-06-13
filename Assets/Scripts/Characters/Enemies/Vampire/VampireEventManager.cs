using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Vampire Event Manager Class
 * Class created to manage in-game events related to the Vampire character in a Observer Pattern approach
 * Animation events are managed in a different script
 * All event names are self explanatory
 */

public class VampireEventManager : MonoBehaviour
{
    //Script References
    [SerializeField] public Vampire thisVampire;

    //Event Declaration
    public delegate void VampireAction();
    public VampireAction OnPlayerFound;
    public VampireAction OnPlayerGone;
    public VampireAction OnVampireDead;
    public VampireAction OnChasingEnded;
    public VampireAction OnIdleEnded;
    public VampireAction OnInvisibleEnded;

    //Variable Declaration
    private Collider2D isPlayerNearby;

    #region Unity Methods
    private void Start()
    {
        //Variable Attribution
        thisVampire = GetComponent<Vampire>();
    }

    private void Update()
    {
        //Method Calls
        PlayerCheck();
        VampireDeath();
    }

    #endregion

    #region Class Specific Methods

    //Notifies subscribed scripts when this vampire finds the Player nearby or when the Player is gone from reach
    private void PlayerCheck()
    {
        isPlayerNearby = Physics2D.OverlapCircle(thisVampire.attackDetectionPoint.position, thisVampire.vampireData.attackDetectionRadius, thisVampire.vampireData.whatIsPlayer);
        if (isPlayerNearby)
        {
            if (OnPlayerFound != null)
            {
                OnPlayerFound();
            }
        }
        if (!isPlayerNearby)
        {
            if (OnPlayerGone != null)
            {
                OnPlayerGone();
            }
        }
    }

    //Notifies subscribed scripts when this zombie character health is below 0 (zero)
    private void VampireDeath()
    {
        if (thisVampire.CurrentHealth <= 0)
        {
            if (OnVampireDead != null)
            {
                OnVampireDead();
            }
        }
    }

    #endregion
}
