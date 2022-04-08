using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatEventManager : MonoBehaviour
{
    public Bat thisBat;

    public delegate void BatAction();
    public BatAction OnPlayerFound;
    public BatAction OnPlayerGone;

    public BatAction OnBatDead;

    private Collider2D isPlayerNearby;

    private void Awake()
    {
        thisBat = GetComponent<Bat>();
    }

    private void Update()
    {
        PlayerCheck();
        BatDeath();
    }

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


}
