using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {

    [Tooltip("the button for the player to click")] public Button startGame;


    void Start()
    {
        Button theButton = startGame.GetComponent<Button>();

        theButton.onClick.AddListener(LevelTransition);
    }

    void LevelTransition()
    {
        SceneManager.LoadScene(1);
    }
}
