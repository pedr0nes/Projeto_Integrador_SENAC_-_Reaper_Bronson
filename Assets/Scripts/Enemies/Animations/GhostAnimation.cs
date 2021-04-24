using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAnimation : MonoBehaviour
{
    [SerializeField] EnemyController m_EnemyController;
    private Animator m_Animator;


    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(m_EnemyController.EnemyCurrentHealth <=0)
        {
            PlayDeathAnimation();
        }
    }

    public void PlayDeathAnimation()
    {
        m_Animator.SetBool("IsDying", true);
    }

    public void KillEnemy()
    {
        m_EnemyController.KillEnemy();
    }


}
