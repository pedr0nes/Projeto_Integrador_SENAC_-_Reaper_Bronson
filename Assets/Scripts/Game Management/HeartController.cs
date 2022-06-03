using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Heart Controller is a Unity MonoBehaviour derived class
 * It is responsible for managing the number of hearts (player lives) displayed on canvas
 * It is attached to the Canvas Game Object called 'Hearts Remaining'
 * It is not the most efficient way to do it and it will be optimized in the future, but it works.
 */

public class HeartController : MonoBehaviour
{
    #region Variables
    [Header("Script References")]
    [Tooltip("Game Manager script attached to the Game Manager object")]
    [SerializeField] public GameManager m_GameManager;

    [Header("Canvas Hearts References")]
    [Tooltip("References to each one of the canvas hearts. They are the child objects to the 'Hearts Remaining' game object located on canvas")]
    [SerializeField] private GameObject heart0;
    [SerializeField] private GameObject heart1;
    [SerializeField] private GameObject heart2;
    [SerializeField] private GameObject heart3;
    [SerializeField] private GameObject heart4;

    #endregion

    //Unity MonoBehaviour Methods
    #region Unity Callback Methods
    private void Awake()
    {
        //Hearts atribution
        heart0 = gameObject.transform.GetChild(0).gameObject;
        heart1 = gameObject.transform.GetChild(1).gameObject;
        heart2 = gameObject.transform.GetChild(2).gameObject;
        heart3 = gameObject.transform.GetChild(3).gameObject;
        heart4 = gameObject.transform.GetChild(4).gameObject;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Keeps track of current number of hearts value and updates how many should be shown in-game
        if (m_GameManager.CurrentNumberOfHearts == 5)
        {
            heart0.SetActive(true);
            heart1.SetActive(true);
            heart2.SetActive(true);
            heart3.SetActive(true);
            heart4.SetActive(true);
        }
        else if (m_GameManager.CurrentNumberOfHearts == 4)
        {
            heart0.SetActive(true);
            heart1.SetActive(true);
            heart2.SetActive(true);
            heart3.SetActive(true);
            heart4.SetActive(false);
        }
        else if (m_GameManager.CurrentNumberOfHearts == 3)
        {
            heart0.SetActive(true);
            heart1.SetActive(true);
            heart2.SetActive(true);
            heart3.SetActive(false);
            heart4.SetActive(false);
        }
        else if(m_GameManager.CurrentNumberOfHearts == 2)
        {
            heart0.SetActive(true);
            heart1.SetActive(true);
            heart2.SetActive(false);
            heart3.SetActive(false);
            heart4.SetActive(false);
        }
        else if (m_GameManager.CurrentNumberOfHearts == 1)
        {
            heart0.SetActive(true);
            heart1.SetActive(false);
            heart2.SetActive(false);
            heart3.SetActive(false);
            heart4.SetActive(false);
        }
        else if (m_GameManager.CurrentNumberOfHearts <= 0)
        {
            heart0.SetActive(false);
            heart1.SetActive(false);
            heart2.SetActive(false);
            heart3.SetActive(false);
            heart4.SetActive(false);
        }
    }

    #endregion
}
