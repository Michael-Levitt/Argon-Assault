using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    void Awake()
    {
        // Singleton pattern
        // Only allows first music player created to remain
        int numMusicPlayers = FindObjectsOfType<MusicPlayer>().Length;
        if(numMusicPlayers > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject); // Stops music from restarting each respawn
        }
    }
}
