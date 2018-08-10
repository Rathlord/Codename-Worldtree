using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitNo : MonoBehaviour {

    [Tooltip("the button for the player to click to close the exit menu")] public Button noExit;
    [Tooltip("the object to be deactivated on click")] [SerializeField] GameObject exitNo;

    void Start()
    {
        Button theButton = noExit.GetComponent<Button>();

        theButton.onClick.AddListener(CloseExitMenu);
    }

    private void CloseExitMenu()
    {
        exitNo.SetActive(false);
    }
}
