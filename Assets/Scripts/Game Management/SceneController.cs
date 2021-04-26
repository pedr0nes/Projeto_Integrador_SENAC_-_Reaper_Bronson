using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] GameObject winScreen;
    private GameManager m_GameManager;


    private void Start()
    {
        if (FindObjectOfType<GameManager>() == true)
        {
            m_GameManager = FindObjectOfType<GameManager>();
            if (m_GameManager.PlayerWon)
            {
                winScreen.SetActive(true);
            }
            else
            {
                winScreen.SetActive(false);
            }
        }
        else
        {
            return;
        }




    }


    public void LoadTryAgain()
    {
        if(m_GameManager)
        {
            Destroy(m_GameManager);
            SceneManager.LoadScene(1);
        }
        else
        {
            SceneManager.LoadScene(1);
        }

    }

    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadFinalScreen()
    {
        SceneManager.LoadScene(2);
    }



    public void LoadQuitGame()
    {
        Application.Quit();
    }


}
