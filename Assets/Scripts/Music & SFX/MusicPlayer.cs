using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Music Player is a Unity MonoBehaviour derived class
// Its purpose is to set the Music Player Object with a Singleton Pattern

public class MusicPlayer : MonoBehaviour
{
    private void Awake()
    {
        SetUpSingleton();
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

}
