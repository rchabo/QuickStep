using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Author: Brackeys (Asbjørn Thirslund) @ https://www.youtube.com/watch?v=JivuXdrIHK0
/// Modified by: Robin Chabouk
/// </summary>
public class MyPauseMenu : MonoBehaviour {

    /// <summary>
    /// Whether the game is paused or not.
    /// </summary>
    public static bool gameIsPaused = false;

    /// <summary>
    /// The pause menu user interface.
    /// </summary>
    public GameObject pauseMenuUI;

    /// <summary>
    /// The help menu user interface.
    /// </summary>
    public GameObject helpMenuUI;

    /// <summary>
    /// The user interface displaying information about the photogrammetry
    /// log.
    /// </summary>
    public GameObject logFactUI;

    /// <summary>
    /// Update is called once per frame. If the user presses the escape key,
    /// activate the pause menu.
    /// </summary>
    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (gameIsPaused) {
                Resume();
            }
            else
            {
                Pause();
            }
        }
	}

    /// <summary>
    /// Resume the game.
    /// </summary>
    public void Resume() { 
        pauseMenuUI.SetActive(false);
        helpMenuUI.SetActive(false);
        logFactUI.SetActive(false);
        FindObjectOfType<CameraController>().rotateSpeed = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    /// <summary>
    /// Pause the game.
    /// </summary>
    public void Pause() {
        logFactUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        FindObjectOfType<CameraController>().rotateSpeed = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    /// <summary>
    /// Show the help menu.
    /// </summary>
    public void Help() {
        pauseMenuUI.SetActive(false);
        helpMenuUI.SetActive(true);
    }

    /// <summary>
    /// Load the main menu.
    /// </summary>
    public void LoadMenu() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Quit the game.
    /// </summary>
    public void QuitGame() {
        Application.Quit();
    }
}
