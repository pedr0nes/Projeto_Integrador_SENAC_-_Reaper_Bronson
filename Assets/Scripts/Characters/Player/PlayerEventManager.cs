using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Player Event Manager Class
 * Class created to manage in game events related to the Player character and its trigers, in a Observer Pattern approach
 * Animation events are managed in a different script
 * All event names are self explanatory
 */
public class PlayerEventManager : MonoBehaviour
{
    //Script References
    [SerializeField] public Player thisPlayer;
    [SerializeField] private GameManager gameManager;
    
    //Event Declaration
    public delegate void PlayerEvent();
    public PlayerEvent OnPlayerHurt;
    public PlayerEvent OnPlayerDead;
    public PlayerEvent OnPlayerWin;


    #region Unity Methods
    private void Start()
    {
        //Player reference attribution
        thisPlayer = GetComponent<Player>();
    }

    private void Update()
    {
        PlayerDeath();
        PlayerWins();
    }

    #endregion

    #region Class Specific Methods

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

    #endregion

}
