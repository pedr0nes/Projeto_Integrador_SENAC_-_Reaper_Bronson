using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScytheAnimation : MonoBehaviour
{
    [SerializeField] private Animator scytheAnimator;
    [SerializeField] private Animator scytheArmAnimator;
    private PlayerController m_PlayerController;


    // Start is called before the first frame update
    void Start()
    {
        m_PlayerController = GetComponentInParent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(m_PlayerController.AttackButtonDown)
        {
            scytheAnimator.SetTrigger("Attack");
            scytheArmAnimator.SetTrigger("Attack");
        }
    }





}
