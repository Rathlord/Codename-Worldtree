using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour {

    AudioSource currentMusic;

    [SerializeField] AudioClip mainMenuMusic;

    [SerializeField] AudioClip otherMusic;

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

    private void Start()
    {
        currentMusic = GetComponent<AudioSource>();
        currentMusic.clip = mainMenuMusic;
        currentMusic.Play();
    }

    void Update()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Create a temporary reference to the current scene.
        Scene currentScene = SceneManager.GetActiveScene();

        // Retrieve the name of this scene.
        string sceneName = currentScene.name;

        if (sceneName == "MainMenu")
        {
            currentMusic.clip = mainMenuMusic;
            currentMusic.Play();
        }
        else
        {
            currentMusic.clip = otherMusic;
            currentMusic.Play();
        }
    }

}
