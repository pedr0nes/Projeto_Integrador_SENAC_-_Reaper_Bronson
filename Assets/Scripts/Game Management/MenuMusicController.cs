using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Menu Music Controller is a Unity MonoBehaviour derived class
// Its purpose is to set the Menu Music Controller Object with a Singleton Pattern

public class MenuMusicController : MonoBehaviour
{
    void Start()
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
