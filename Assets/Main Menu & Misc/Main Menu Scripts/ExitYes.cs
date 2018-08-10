using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitYes : MonoBehaviour {

    [Tooltip("the button for the player to click to exit the game")] public Button exitGame;

    // Use this for initialization
    void Start () {
        Button theButton = exitGame.GetComponent<Button>();

        theButton.onClick.AddListener(ExitGame);
    }
	
    void ExitGame()
    {
        Application.Quit();
        print("Quitting the game now.");
    }
}
