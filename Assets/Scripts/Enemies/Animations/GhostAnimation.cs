using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAnimation : MonoBehaviour
{
    [SerializeField] EnemyController m_EnemyController;
    [SerializeField] Transform attackPoint;
    [SerializeField] private float playerDetectionRadius;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private GhostSFX ghostSFXManager;
    private Collider2D isPlayerNearby;
    private Animator m_Animator;


    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }

    private void Start()
    {
        ghostSFXManager.PlayGhostAttackSFX();
    }


    private void Update()
    {
        isPlayerNearby = Physics2D.OverlapCircle(attackPoint.position, playerDetectionRadius, playerLayer);
        PlayAttackAnimation();

        if (m_EnemyController.EnemyCurrentHealth <=0)
        {
            PlayDeathAnimation();
        }
    }


    public void PlayAttackAnimation()
    {
        if (isPlayerNearby)
        {

            m_Animator.SetTrigger("Attack");
        }
        else
        {

            m_Animator.SetTrigger("BackToFly");
        }
    }

    public void AttackPlayer()
    {
        if (isPlayerNearby)
        {
            PlayerController player = isPlayerNearby.GetComponent<PlayerController>();
            ghostSFXManager.PlayGhostAttackSFX();
            player.TakeDamage(m_EnemyController.EnemyAttackDamage);
        }
    }




    public void PlayDeathAnimation()
    {
        m_Animator.SetBool("IsDying", true);
    }

    public void KillEnemy()
    {
        ghostSFXManager.PlayGhostDieSFX();
        m_EnemyController.KillEnemy();
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, playerDetectionRadius);
    }

}
