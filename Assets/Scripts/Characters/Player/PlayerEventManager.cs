using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEventManager : MonoBehaviour
{
    [SerializeField] public Player thisPlayer;
    [SerializeField] private GameManager gameManager;
    
    public delegate void PlayerEvent();

    public PlayerEvent OnPlayerHurt;
    public PlayerEvent OnPlayerDead;
    public PlayerEvent OnPlayerWin;

    private void Start()
    {
        thisPlayer = GetComponent<Player>();
    }

    private void Update()
    {
        PlayerDeath();
        PlayerWins();
    }

    private void PlayerWins()
    {
        if(gameManager.PlayerWon)
        {
            if(OnPlayerWin != null)
            {
                OnPlayerWin();
            }
        }
    }


    private void PlayerDeath()
    {
        if (thisPlayer.CurrentHealth <= 0)
        {
            if (OnPlayerDead != null)
            {
                OnPlayerDead();
            }
        }
    }

}
