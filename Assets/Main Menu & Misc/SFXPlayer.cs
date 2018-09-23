using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SFXPlayer : MonoBehaviour {

    //I'm not an sfx player yet, I'm just a copy of the music player. Whoops.

    public static SFXPlayer instance;

    AudioSource audioSource;

    [SerializeField] AudioClip jump;

    [SerializeField] AudioClip potato;

    private void Awake()
    {
        int numberOfSFXPlayer = FindObjectsOfType<MusicPlayer>().Length;
        print("Number of music players = " + numberOfSFXPlayer);

        if (numberOfSFXPlayer > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

        instance = this;
    }

    public void PlayJump() // I'm almost certain this will work
    {
        audioSource.PlayOneShot(jump);
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }



}


