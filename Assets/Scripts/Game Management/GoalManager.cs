using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Goal Manager is a Unity MonoBehaviour derived class
// It is responsible for checking if victory conditions are met by the player

public class GoalManager : MonoBehaviour
{
    #region Variable Declaration

    //References shown in Unity Inspector 
    [SerializeField] private Transform areaPoint1;
    [SerializeField] private Transform areaPoint2;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] public GameManager m_GameManager;

    //Other Variables
    private Collider2D isPlayerNearby;
    private Vampire vampire;

    #endregion

    void Update()
    {
        //Constantly assigning to the 'vampire' variable the Vampire script found in scene
        vampire = FindObjectOfType<Vampire>();
        //Constantly assigning to the 'm_GameManager' variable the GameManager script found in scene. This is due to a bug still not solved and will be changed later.
        m_GameManager = FindObjectOfType<GameManager>();

        //Checks if objects labeled 'Player' are within the victory area
        isPlayerNearby = Physics2D.OverlapArea(areaPoint1.position, areaPoint2.position, playerLayer);
        Debug.DrawLine(areaPoint2.position, areaPoint1.position);

        //If a player is found and there are no Vampires in scene, it changes the value of the Game Manager property 'PlayerWon' to true
        if(isPlayerNearby && vampire == null)
        {
            m_GameManager.PlayerWon = true;
        }
    }
}
