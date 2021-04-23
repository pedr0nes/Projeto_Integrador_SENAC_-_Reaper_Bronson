using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private PlayerController m_PlayerController;
    private Animator m_Animator;

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
        m_Animator.SetFloat("Speed", Mathf.Abs(m_PlayerController.MoveInput));
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



    public void OnLanding()
    {
        m_Animator.SetBool("IsJumping", false);
    }

    private IEnumerator AttackDeltaTime(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        //scytheAttack.gameObject.SetActive(false);
        m_Animator.SetTrigger("AttackFinished");
    }

}
