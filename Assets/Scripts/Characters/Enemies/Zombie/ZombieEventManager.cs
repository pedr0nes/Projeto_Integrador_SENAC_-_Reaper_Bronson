using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieEventManager : MonoBehaviour
{
    public Zombie thisZombie;

    public delegate void ZombieAction();
    public ZombieAction OnObstacleFound;
    public ZombieAction OnPlayerFound;
    public ZombieAction OnPlayerGone;
    public ZombieAction OnZombieDead;

    private Vector2 groundCheckDirection;
    private Vector2 obstacleCheckDirection;
    private Collider2D isPlayerNearby;

    private void Start()
    {
        thisZombie = this.GetComponent<Zombie>();
        
        //obstacleCheckDirection = transform.right * thisZombie.FacingDirection;
    }


    private void Update()
    {
        ObstacleCheck();

        PlayerCheck();

        ZombieDeath();

    }

    private void ZombieDeath()
    {
        
        if (thisZombie.CurrentHealth <= 0)
        {
            if (OnZombieDead != null)
            {
                OnZombieDead();
            }
        }
    }

    private void PlayerCheck()
    {
        isPlayerNearby = Physics2D.OverlapCircle(thisZombie.attackDetectionPoint.position, thisZombie.zombieData.attackDetectionRadius, thisZombie.zombieData.whatIsPlayer);
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

    private void ObstacleCheck()
    {
        obstacleCheckDirection = -transform.right;
        groundCheckDirection = -transform.right + new Vector3(0, -1f, 0f);

        RaycastHit2D groundInfo = Physics2D.Raycast(thisZombie.groundDetectionPoint.position, groundCheckDirection, thisZombie.zombieData.groundCheckLenght, thisZombie.zombieData.whatIsGround);
        RaycastHit2D wallInfo = Physics2D.Raycast(thisZombie.obstacleDetectionPoint.position, obstacleCheckDirection, thisZombie.zombieData.groundCheckLenght, thisZombie.zombieData.whatIsGround);
        RaycastHit2D otherEnemyInfo = Physics2D.Raycast(thisZombie.obstacleDetectionPoint.position, obstacleCheckDirection, thisZombie.zombieData.groundCheckLenght, thisZombie.zombieData.whatIsEnemy);

        if ((!groundInfo.collider || otherEnemyInfo.collider || wallInfo.collider) && otherEnemyInfo.collider != this.gameObject.GetComponent<Collider2D>())
        {
            if (OnObstacleFound != null)
            {
                OnObstacleFound();
            }
        }

        Debug.DrawRay(thisZombie.groundDetectionPoint.position, groundCheckDirection);
        Debug.DrawRay(thisZombie.obstacleDetectionPoint.position, obstacleCheckDirection, Color.magenta, thisZombie.zombieData.groundCheckLenght);
    }

}
