using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Robin Chabouk.
/// This script is responsible for displaying the help menu.
/// </summary>
public class HelpMenu : MonoBehaviour {

    /// <summary>
    /// The pause menu User Interface.
    /// </summary>
    public GameObject pauseMenuUI;

    /// <summary>
    /// The help menu User Interface.
    /// </summary>
    public GameObject helpMenuUI;

    /// <summary>
    /// This method takes the player back to the pause menu from the
    /// help menu.
    /// </summary>
    public void Back()
    {
        pauseMenuUI.SetActive(true);
        helpMenuUI.SetActive(false);
    }
}
