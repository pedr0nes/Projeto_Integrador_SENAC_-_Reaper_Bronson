using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] GameObject heartsDisplay;

    [SerializeField] GameObject winCanvas;
    [SerializeField] GameObject gameOverCanvas;

    private int currentNumberOfHearts = 5;
    private int currentNumberOfLives = 3;
    private bool playerWon = false;

    private void Awake()
    {
        //SetUpSingleton();
        currentNumberOfHearts = player.CurrentHealth;
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
        currentNumberOfHearts = player.CurrentHealth;

        if(playerWon)
        {
            winCanvas.SetActive(true);
        }

        if(currentNumberOfHearts <=0)
        {
            gameOverCanvas.SetActive(true);
        }


    }

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

    //public void RestartScene()
    //{
    //    SceneManager.LoadScene(1);
    //}

    //public void LoadFinalScene()
    //{
    //    SceneManager.LoadScene(2);
    //}


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


}
