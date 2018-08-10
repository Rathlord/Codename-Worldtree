using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitGameConfirmation : MonoBehaviour {

    [Tooltip("the button for the player to click to bring up the exit menu")] public Button exitGame;
    [Tooltip("the object to be activated on click")][SerializeField] GameObject exitMenu;



    void Start () {
        Button theButton = exitGame.GetComponent<Button>();

        theButton.onClick.AddListener(ExitPopup);
	}
	
    void ExitPopup()
    {
        print("I should open the exit popup here");
        exitMenu.SetActive(true);
    }
}
