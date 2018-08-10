using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UnlockablesMenuOpener : MonoBehaviour {

    [Tooltip("the button for the player to click")] public Button unlockablesButton;


    void Start()
    {
        Button theButton = unlockablesButton.GetComponent<Button>();

        theButton.onClick.AddListener(LevelTransition);
    }

    void LevelTransition()
    {
        SceneManager.LoadScene(3);
    }
}
