using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScytheDamageController : MonoBehaviour
{
    [SerializeField] private PlayerController m_PlayerController;
    [SerializeField] private PlayerSFX playerSFXManager;
    // Start is called before the first frame update


    public void CallAttack()
    {
        m_PlayerController.Attack();
    }

    public void CallScytheSFX()
    {
        playerSFXManager.PlayScytheAttackSFX();
    }


}
