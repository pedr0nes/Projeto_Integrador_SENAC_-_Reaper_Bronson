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
        //if (FindObjectOfType<GameManager>() == true)
        //{
        //    m_GameManager = FindObjectOfType<GameManager>();
        //    if (m_GameManager.PlayerWon)
        //    {
        //        winScreen.SetActive(true);
        //    }
        //    else
        //    {
        //        winScreen.SetActive(false);
        //    }
        //}
        //else
        //{
        //    return;
        //}
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

    public void ReloadScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex);
    }
    public void LoadMainMenu()
    {
        MusicPlayer musicPlayer = FindObjectOfType<MusicPlayer>();

        if (musicPlayer != null)
        {
            Destroy(musicPlayer.gameObject);
        }
        SceneManager.LoadScene(0);
    }

    public void LoadMoviesMenu()
    {
        SceneManager.LoadScene(1);
    }



    public void LoadHelpMenu()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadCreditsMenu()
    {
        SceneManager.LoadScene(3);
    }

    public void LoadActsMenu()
    {
        SceneManager.LoadScene(4);
    }

    public void LoadGame(int scene)
    {
        MenuMusicController menuMusic = FindObjectOfType<MenuMusicController>();

        if(menuMusic != null)
        {
            Destroy(menuMusic.gameObject);
        }
        
        SceneManager.LoadScene(scene);
    }

    public void LoadFinalScreen()
    {
        SceneManager.LoadScene(8);
    }



    public void LoadQuitGame()
    {
        Application.Quit();
    }


}
