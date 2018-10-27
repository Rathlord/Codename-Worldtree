using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SFXPlayer : MonoBehaviour {

    public static SFXPlayer instance;

    AudioSource audioSource;

    [SerializeField] AudioClip empty; // Use this as template
    [SerializeField] AudioClip jump;
    [SerializeField] AudioClip wrathFireball;
    [SerializeField] AudioClip chestOpen;
    [SerializeField] AudioClip jumpPad;
    [SerializeField] AudioClip tyrfing;
    [SerializeField] AudioClip irisTakerAttack;
    [SerializeField] AudioClip itemPickup;
    [SerializeField] AudioClip monsterDeath;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Awake()
    {
        int numberOfSFXPlayer = FindObjectsOfType<SFXPlayer>().Length;
        print("Number of SFX players = " + numberOfSFXPlayer);

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

    /// Below are functions for sounds to be called from other classes ///



    public void PlaySound() // Template- rename and replace clip
    {
        audioSource.PlayOneShot(empty);
    }

    public void PlayMonsterDeath()
    {
        audioSource.PlayOneShot(monsterDeath);
    }

    public void PlayItempickup() 
    {
        audioSource.PlayOneShot(itemPickup);
    }

    public void PlayIrisTakerAttack()
    {
        audioSource.PlayOneShot(irisTakerAttack);
    }

    public void PlayTyrfing()
    {
        audioSource.PlayOneShot(tyrfing);
    }

    public void PlayJumpPad()
    {
        audioSource.PlayOneShot(jumpPad);
    }

    public void PlayChestOpen()
    {
        audioSource.PlayOneShot(chestOpen);
    }

    public void PlayWrathFireball()
    {
        audioSource.PlayOneShot(wrathFireball);
    }

    public void PlayJump()
    {
        audioSource.PlayOneShot(jump);
    }




}


