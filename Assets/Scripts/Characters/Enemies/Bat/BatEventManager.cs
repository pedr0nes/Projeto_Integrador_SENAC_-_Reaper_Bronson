using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Bat Event Manager Class
 * Class created to manage in game events related to the Bat character and its trigers, in a Observer Pattern approach
 * Animation events are managed in a different script
 * All event names are self explanatory
 */

public class BatEventManager : MonoBehaviour
{
    //Script References
    [SerializeField] public Bat thisBat;

    //Event Declaration
    public delegate void BatAction();
    public BatAction OnPlayerFound;
    public BatAction OnPlayerGone;
    public BatAction OnBatDead;

    //Variable Declaration
    private Collider2D isPlayerNearby;

    #region Unity Methods
    void Start()
    {
        //Variable attribution
        thisBat = GetComponent<Bat>();
    }

    void Update()
    {
        //Method Calls
        PlayerCheck();
        BatDeath();
    }

    #endregion

    #region Class Specific Methods

    //Notifies subscribed scripts when this Bat finds the Player nearby or when the Player is gone from reach
    private void PlayerCheck()
    {
        isPlayerNearby = Physics2D.OverlapArea(thisBat.detectionAreaPoint1.position, thisBat.detectionAreaPoint2.position, thisBat.batData.whatIsPlayer);
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

    //Notifies subscribed scripts when this Bat character health is below 0 (zero)
    private void BatDeath()
    {
        if (thisBat.CurrentHealth <= 0)
        {
            if (OnBatDead != null)
            {
                OnBatDead();
            }
        }
    }
    #endregion

}
