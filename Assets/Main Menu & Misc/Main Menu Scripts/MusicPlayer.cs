using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {

    private void Awake()
    {
        int numberOfMusicPlayers = FindObjectsOfType<MusicPlayer>().Length;
        print("Number of music players = " + numberOfMusicPlayers);

        if (numberOfMusicPlayers > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

}
