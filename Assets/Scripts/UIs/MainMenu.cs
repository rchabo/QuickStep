using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Author: Brackeys (Asbjørn Thirslund) @ https://www.youtube.com/watch?v=zc8ac_qUXQY&t=287s
/// Modified by: Robin Chabouk.
/// This script is responsible for the the main menu.
/// </summary>
public class MainMenu : MonoBehaviour {

    /// <summary>
    /// Start the game.
    /// </summary>
    public void PlayGame() {
        SceneManager.LoadScene(1);
        Cursor.visible = false;
        Time.timeScale = 1f;
    }

    /// <summary>
    /// Quit the game.
    /// </summary>
    public void QuitGame() {
        Debug.Log("Quit");
        Application.Quit();
    }
}
