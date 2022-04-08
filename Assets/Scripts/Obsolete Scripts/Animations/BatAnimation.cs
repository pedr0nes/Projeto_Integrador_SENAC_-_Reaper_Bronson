using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatAnimation : MonoBehaviour
{
    [SerializeField] private Transform areaPoint1;
    [SerializeField] private Transform areaPoint2;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private GameObject sonicWavePrefab;
    [SerializeField] private Transform[] spawners = new Transform[4];
    [SerializeField] private EnemyController m_EnemyController;
    [SerializeField] private Rigidbody2D m_Rigidbody2D;
    [SerializeField] private BatSFX batSFXManager;


    private Animator m_Animator;
    private Collider2D isPlayerNearby;
    private bool shouldSpawn = true;


    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    IEnumerator Start()
    {
        while (shouldSpawn)
        {
            if(isPlayerNearby)
            {
                Attack();
                yield return new WaitForSeconds(2f);
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
        isPlayerNearby = Physics2D.OverlapArea(areaPoint1.position, areaPoint2.position, playerLayer);
        Debug.DrawLine(areaPoint2.position, areaPoint1.position);
        //Debug.Log(isPlayerNearby);

        if (m_EnemyController.EnemyCurrentHealth <= 0)
        {
            PlayDeathAnimation();
        }
    }

    private void Attack()
    {
        m_Animator.SetTrigger("Attack");

    }

    public void InstantiateBatAttack()
    {
        batSFXManager.PlayBatAttackSFX();
        foreach (Transform spawner in spawners)
        {
            Instantiate(sonicWavePrefab, spawner.position, spawner.rotation);
        }

    }

    public void ReturnToFlying()
    {
        m_Animator.SetTrigger("BackToFly");
    }

    public void PlayDeathAnimation()
    {
        m_Rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        m_Animator.SetBool("IsDying", true);
    }

    public IEnumerator KillEnemy()
    {
        batSFXManager.PlayBatDieSFX();
        m_Animator.SetBool("IsAlreadyDead", true);
        yield return new WaitForSeconds(1f);
        m_EnemyController.KillEnemy();
    }

}
