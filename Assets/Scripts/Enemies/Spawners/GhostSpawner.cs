using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class GhostSpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float playerDetectionRadius;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float secondsToNewSpawn = 2f;


    [SerializeField] private GameObject ghostPrefab;
    [SerializeField] private AIDestinationSetter aiDestinationSetter;
    private Collider2D[] enemiesAround;
    private bool isPlayerNearby;
    [SerializeField] private int maxNumberOfSpawns = 1;



    private void Awake()
    {
        aiDestinationSetter.target = FindObjectOfType<PlayerController>().transform;
    }

    IEnumerator Start()
    {
        while (true)
        {
            if (isPlayerNearby && enemiesAround.Length < maxNumberOfSpawns)
            {
  
                SpawnGhost();
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

    


    private void SpawnGhost()
    {
        if(isPlayerNearby)
        {
            Debug.Log("new ghost in town");
            Instantiate(ghostPrefab, transform.position, transform.rotation);
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(spawnPoint.position, playerDetectionRadius);
    }


}
