using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimation : MonoBehaviour
{
    [SerializeField] private PatrolOnGround patrolScript;
    [SerializeField] private EnemyController m_EnemyController;
    [SerializeField] Transform attackPoint;
    [SerializeField] private float playerDetectionRadius;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private ZombieSFX zombieSFXManager;
    private Animator m_animator;
    private Collider2D isPlayerNearby;
    private bool alreadySpawned = false;

    private bool dieSFXplayed = false;
   // private Rigidbody2D rigidbody;


    private void Awake()
    {
        m_animator = GetComponent<Animator>();
        
        patrolScript.enabled = false;

    }

    private void Start()
    {
        zombieSFXManager.PlayZombieSpawnSFX();
    }

    // Update is called once per frame
    void Update()
    {
        isPlayerNearby = Physics2D.OverlapCircle(attackPoint.position, playerDetectionRadius, playerLayer);
        if (alreadySpawned)
        {
            PlayAttackAnimation();
        }
      
        if (m_EnemyController.EnemyCurrentHealth <= 0)
        {
            PlayDeathAnimation();
        }

    }

    public void EnablePatrolAfterSpawn(int aux)
    {
        if (aux == 1)
        {
            
            patrolScript.enabled = true;
            m_animator.SetTrigger("Walk");
            alreadySpawned = true;
        }
    }

    public void PlayAttackAnimation()
    {
        if (isPlayerNearby)
        {
            patrolScript.enabled = false;
            m_animator.SetTrigger("Attack");

        }
        else
        {
            patrolScript.enabled = true;
            m_animator.SetTrigger("BackToWalk");
        }
    }

    public void AttackPlayer()
    {
        if(isPlayerNearby)
        {
            PlayerController player = isPlayerNearby.GetComponent<PlayerController>();
            player.TakeDamage(m_EnemyController.EnemyAttackDamage);
        }
    }

    public void PlayDeathAnimation()
    {

        patrolScript.enabled = false;
        m_animator.SetBool("IsDead", true);
    }

    public void KillEnemy()
    {
        m_EnemyController.KillEnemy();
    }


    public void CallZombieAttackSFX()
    {
        zombieSFXManager.PlayZombieAttackSFX();
    }

    public void CallZombieDieSFX()
    {
        if(!dieSFXplayed)
        {
            zombieSFXManager.PlayZombieDieSFX();
            dieSFXplayed = true;
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, playerDetectionRadius);
    }


}
