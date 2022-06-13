using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Game Manager is a Unity MonoBehaviour derived class
 * It is responsible for managing in-game checks, canvases and victory/defeat conditions
 * It does NOT manage scene changes. Scene Controller is the class responsible for it
 * It is not optimized yet. Probably its structure will be altered
 */

public class GameManager : MonoBehaviour
{
    #region Variables
    //Reference Variables shown in Unity Inspector
    [Header("Script References")]
    [Tooltip("Player script is in the Player Game Object")]
    [SerializeField] Player player;

    [Header("Canvas Game Objects References")]
    [SerializeField] GameObject heartsDisplay;
    [SerializeField] GameObject winCanvas;
    [SerializeField] GameObject gameOverCanvas;

    //Other Variables
    private int currentNumberOfHearts = 5;
    private int currentNumberOfLives = 3;
    private bool playerWon = false;

    #endregion

    //Unity MonoBehaviour Methods
    #region Unity Methods
    private void Awake()
    {
        //Sets this game object to be a singleton
        //SetUpSingleton();

        //At the start of the game, stores player's initial number of hearts in a variable
        currentNumberOfHearts = player.CurrentHealth;

        //Canvas objects of game victory/defeat are set to NOT ACTIVE
        winCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Updates the current number of hearts based on player's health value
        currentNumberOfHearts = player.CurrentHealth;

        //Victory menu is set to active if win condition is met
        if(playerWon)
        {
            winCanvas.SetActive(true);
        }

        //Game Over menu is set to active if defeat condition is met
        if (currentNumberOfHearts <=0)
        {
            gameOverCanvas.SetActive(true);
        }
    }

    #endregion

    #region Game Manager Methods

    //Set the game object on which this script is a component to be a singleton. It is not being used yet though.
    private void SetUpSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    #endregion

    #region Properties
    public int CurrentNumberOfHearts
    {
        get
        {
            return currentNumberOfHearts;
        }
        set
        {
            currentNumberOfHearts = value;
        }
    }

    public int CurrentNumberOfLifes
    {
        get
        {
            return currentNumberOfLives;
        }
        set
        {
            currentNumberOfLives = value;
        }
    }

    public bool PlayerWon
    {
        get
        {
            return playerWon;
        }
        set
        {
            playerWon = value;
        }
    }

    #endregion

}
