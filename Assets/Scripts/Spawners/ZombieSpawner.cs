using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float playerDetectionRadius;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask enemyLayer;

    [SerializeField] private GameObject zombiePrefab;
    [SerializeField] private float secondsToNewSpawn = 2f;
    [SerializeField] private int maxNumberOfSpawns = 1;
    
    private bool isPlayerNearby;
    private Collider2D[] enemiesAround;


    IEnumerator Start()
    {
        while (true)
        {
            if (isPlayerNearby && enemiesAround.Length < maxNumberOfSpawns)
            {
                Debug.Log("chegou");
                SpawnZombie();
                yield return new WaitForSeconds(secondsToNewSpawn);
            }
            else
            {
                yield return new WaitForSeconds(1f);
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        isPlayerNearby = Physics2D.OverlapCircle(spawnPoint.position, playerDetectionRadius, playerLayer);
        enemiesAround = Physics2D.OverlapCircleAll(spawnPoint.position, playerDetectionRadius, enemyLayer);
    }




    private void SpawnZombie()
    {
        if (isPlayerNearby)
        {
            
            Instantiate(zombiePrefab, transform.position, transform.rotation);

        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(spawnPoint.position, playerDetectionRadius);
    }


}
