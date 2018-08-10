using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionsMenuOpener : MonoBehaviour {

    [Tooltip("the button for the player to click")] public Button optionsButton;


    void Start()
    {
        Button theButton = optionsButton.GetComponent<Button>();

        theButton.onClick.AddListener(LevelTransition);
    }

    void LevelTransition()
    {
        SceneManager.LoadScene(2);
    }
}
