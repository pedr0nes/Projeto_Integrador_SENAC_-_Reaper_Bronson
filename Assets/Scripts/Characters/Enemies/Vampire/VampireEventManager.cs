using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Vampire Event Manager Class
 * Class created to manage in game events related to the Vampire character in a Observer Pattern approach
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
        thisVampire = GetComponent<Vampire>();
    }

    private void Update()
    {
        PlayerCheck();

        VampireDeath();
    }

    private void OnEnable()
    {
        //VampireStateWalk.OnWalkStarted += CallChasingPlayer;
        VampireStateIdle.OnIdleStarted += CallIdlePeriod;
        VampireStateInvisible.OnInvisibleStarted += CallInvisiblePeriod;
    }

    private void OnDisable()
    {
        //VampireStateWalk.OnWalkStarted -= CallChasingPlayer;
        VampireStateIdle.OnIdleStarted -= CallIdlePeriod;
        VampireStateInvisible.OnInvisibleStarted -= CallInvisiblePeriod;
    }



    #endregion

    #region Class Specific Methods

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





    /*private void CallChasingPlayer()
    {
        Debug.Log("Chamou Chasing Player");
        StartCoroutine(ChasingPlayerPeriod());
    }*/

    private void CallIdlePeriod()
    {
        StartCoroutine(IdlePeriod());
    }

    private void CallInvisiblePeriod()
    {
        StartCoroutine(InvisiblePeriod());
    }



    /*private IEnumerator ChasingPlayerPeriod()
    {
        
        yield return new WaitForSeconds(Random.Range(thisVampire.vampireData.minWalkTime, thisVampire.vampireData.maxWalkTime));
        if(OnChasingEnded != null)
        {
            OnChasingEnded();
        }
    }
    */



    private IEnumerator IdlePeriod()
    {
        yield return new WaitForSeconds(thisVampire.vampireData.idleTime);
        if(OnIdleEnded != null)
        {
            OnIdleEnded();
        }
    }

    private IEnumerator InvisiblePeriod()
    {
        yield return new WaitForSeconds(thisVampire.vampireData.invisibleTime);
        if (OnInvisibleEnded != null)
        {
            OnInvisibleEnded();
        }
    }


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
