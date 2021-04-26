using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    
    private PlayerController m_PlayerController;
   
    private Animator m_Animator;

    private GameManager m_GameManager;

    private bool isPlayerDead;

    //private Transform scytheAttack;

    // Start is called before the first frame update
    void Start()
    {
        m_PlayerController = GetComponent<PlayerController>();
        m_Animator = GetComponent<Animator>();
        //scytheAttack = transform.Find("Scythe Attack");

    }

    // Update is called once per frame
    void Update()
    {

        m_GameManager = FindObjectOfType<GameManager>();
        m_Animator.SetFloat("Speed", Mathf.Abs(m_PlayerController.MoveInput));
        isPlayerDead = m_PlayerController.LostAllLives;
        if (m_PlayerController.IsJumping)
        {
            m_Animator.SetBool("IsJumping", true);
        }
        if (m_PlayerController.AttackButtonDown)
        {
            //scytheAttack.gameObject.SetActive(true);
            m_Animator.SetTrigger("Attack");
            //StartCoroutine(AttackDeltaTime(0.65f));
            
        }

    }

    public IEnumerator KillPlayer()
    {
        
        yield return new WaitForSeconds(1.5f);
        m_GameManager.CurrentNumberOfLifes--;


        yield return new WaitForSeconds(0.45f);
        if (isPlayerDead)
        {

            Destroy(gameObject);
            m_GameManager.LoadFinalScene();
        }
        else
        {
            Destroy(gameObject);
            m_GameManager.RestartScene();
        }

    }

    /*public void StopPlayerMovement()
    {
        GetComponent<PlayerController>().enabled = false;
    }*/

    public void OnLanding()
    {
        m_Animator.SetBool("IsJumping", false);
    }

    /*private IEnumerator AttackDeltaTime(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        //scytheAttack.gameObject.SetActive(false);
        m_Animator.SetTrigger("AttackFinished");
    }*/

}
