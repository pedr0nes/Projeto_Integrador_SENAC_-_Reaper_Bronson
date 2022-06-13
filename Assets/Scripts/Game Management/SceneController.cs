using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* Scene Controller is a Unity MonoBehaviour derived class
 * It is responsible for managing scene transitions and loadings
 * It is not optimized yet. Probably its structure will be altered in future uptades. It is functional though.
 */

public class SceneController : MonoBehaviour
{

    #region Scene Controller Methods

    //Reloads Current Scene
    public void ReloadScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex);
    }

    //Loads Main Menu Scene after destroying the in-game music player, if there is one
    public void LoadMainMenu()
    {
        MusicPlayer musicPlayer = FindObjectOfType<MusicPlayer>();

        if (musicPlayer != null)
        {
            Destroy(musicPlayer.gameObject);
        }
        SceneManager.LoadScene(0);
    }

    //Loads the Stage Selection Menu
    public void LoadMoviesMenu()
    {
        SceneManager.LoadScene(1);
    }

    //Loads Stage Acts (Game Scenes) Menu
    public void LoadActsMenu()
    {
        SceneManager.LoadScene(4);
    }

    //Loads the Help/Tutorial Menu
    public void LoadHelpMenu()
    {
        SceneManager.LoadScene(2);
    }

    //Loads the Credits
    public void LoadCreditsMenu()
    {
        SceneManager.LoadScene(3);
    }

    //Loads the Game Scene after destroying the menu music player, if there is one
    public void LoadGame(int scene)
    {
        MenuMusicController menuMusic = FindObjectOfType<MenuMusicController>();

        if(menuMusic != null)
        {
            Destroy(menuMusic.gameObject);
        }
        
        SceneManager.LoadScene(scene);
    }

    //Quits the game
    public void LoadQuitGame()
    {
        Application.Quit();
    }


    #endregion

}
