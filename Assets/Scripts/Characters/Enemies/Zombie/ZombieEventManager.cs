using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Zombie Event Manager Class
 * Class created to manage in game events related to the Zombie character and its trigers, in a Observer Pattern approach
 * Animation events are managed in a different script
 * All event names are self explanatory
 */

public class ZombieEventManager : MonoBehaviour
{
    //Script References
    [SerializeField] public Zombie thisZombie;

    //Event Declaration
    public delegate void ZombieAction();
    public ZombieAction OnObstacleFound;
    public ZombieAction OnPlayerFound;
    public ZombieAction OnPlayerGone;
    public ZombieAction OnZombieDead;

    //Variable Declaration
    private Vector2 groundCheckDirection;
    private Vector2 obstacleCheckDirection;
    private Collider2D isPlayerNearby;

    #region Unity Methods
    private void Start()
    {
        //Variable attribution
        thisZombie = GetComponent<Zombie>();
    }

    private void Update()
    {
        //Method Calls
        ObstacleCheck();
        PlayerCheck();
        ZombieDeath();
    }

    #endregion

    #region Class Specific Methods

    //Notifies subscribed scripts when this zombie character health is below 0 (zero)
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

    //Notifies subscribed scripts when this zombie finds the Player nearby or when the Player is gone from reach
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

    //Notifies subscribed scripts when this zombie finds an obstacle (either ground, walls or other enemy type characters
    private void ObstacleCheck()
    {
        obstacleCheckDirection = -transform.right;
        groundCheckDirection = -transform.right + new Vector3(0, -1f, 0f);

        RaycastHit2D groundInfo = Physics2D.Raycast(thisZombie.groundDetectionPoint.position, groundCheckDirection, thisZombie.zombieData.groundCheckLenght, thisZombie.zombieData.whatIsGround);
        RaycastHit2D wallInfo = Physics2D.Raycast(thisZombie.obstacleDetectionPoint.position, obstacleCheckDirection, thisZombie.zombieData.groundCheckLenght, thisZombie.zombieData.whatIsGround);
        RaycastHit2D otherEnemyInfo = Physics2D.Raycast(thisZombie.obstacleDetectionPoint.position, obstacleCheckDirection, thisZombie.zombieData.groundCheckLenght, thisZombie.zombieData.whatIsEnemy);

        if ((!groundInfo.collider || otherEnemyInfo.collider || wallInfo.collider) && otherEnemyInfo.collider != gameObject.GetComponent<Collider2D>())
        {
            if (OnObstacleFound != null)
            {
                OnObstacleFound();
            }
        }

        //Debugs
        Debug.DrawRay(thisZombie.groundDetectionPoint.position, groundCheckDirection);
        Debug.DrawRay(thisZombie.obstacleDetectionPoint.position, obstacleCheckDirection, Color.magenta, thisZombie.zombieData.groundCheckLenght);
    }
    #endregion
}
