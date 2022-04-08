using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampireEventManager : MonoBehaviour
{
    public Vampire thisVampire;

    public delegate void VampireAction();

    public VampireAction OnPlayerFound;
    public VampireAction OnPlayerGone;
    public VampireAction OnVampireDead;

    public VampireAction OnChasingEnded;
    public VampireAction OnIdleEnded;
    public VampireAction OnInvisibleEnded;

    private Collider2D isPlayerNearby;

    private void Start()
    {
        thisVampire = GetComponent<Vampire>();
    }

    private void Update()
    {
        PlayerCheck();

        VampireDeath();
    }

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

    private void OnEnable()
    {
        VampireStateWalk.OnWalkStarted += CallChasingPlayer;
        VampireStateIdle.OnIdleStarted += CallIdlePeriod;
        VampireStateInvisible.OnInvisibleStarted += CallInvisiblePeriod;
    }

    private void OnDisable()
    {
        VampireStateWalk.OnWalkStarted -= CallChasingPlayer;
        VampireStateIdle.OnIdleStarted -= CallIdlePeriod;
        VampireStateInvisible.OnInvisibleStarted -= CallInvisiblePeriod;
    }



    private void CallChasingPlayer()
    {
        Debug.Log("Chamou Chasing Player");
        StartCoroutine(ChasingPlayerPeriod());
    }

    private void CallIdlePeriod()
    {
        StartCoroutine(IdlePeriod());
    }

    private void CallInvisiblePeriod()
    {
        StartCoroutine(InvisiblePeriod());
    }



    private IEnumerator ChasingPlayerPeriod()
    {
        
        yield return new WaitForSeconds(Random.Range(thisVampire.vampireData.minWalkTime, thisVampire.vampireData.maxWalkTime));
        if(OnChasingEnded != null)
        {
            OnChasingEnded();
        }
    }




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


}
