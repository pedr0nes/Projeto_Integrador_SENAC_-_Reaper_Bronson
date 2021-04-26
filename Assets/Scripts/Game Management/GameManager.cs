using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] GameObject heartsDisplay;

    private int currentNumberOfHearts = 5;
    private int currentNumberOfLifes = 3;
    private bool playerWon = false;

    private void Awake()
    {
        SetUpSingleton();
        currentNumberOfLifes = player.MaxPlayerLifes;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        player = FindObjectOfType<PlayerController>();
        Debug.Log("vidas do gam3e manager: " + currentNumberOfLifes);
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

    public void RestartScene()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadFinalScene()
    {
        SceneManager.LoadScene(2);
    }


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
            return currentNumberOfLifes;
        }
        set
        {
            currentNumberOfLifes = value;
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
