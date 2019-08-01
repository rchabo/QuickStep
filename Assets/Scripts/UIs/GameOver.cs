using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Author: Robin Chabouk
/// This script is responsible for making the game over UI pop up when
/// the player dies, and displaying what their options are.
/// </summary>
public class GameOver : MonoBehaviour {

    /// <summary>
    /// The starting position of the player for the current level.
    /// </summary>
    public Transform startPlayerPos;

    /// <summary>
    /// The player.
    /// </summary>
    public GameObject thePlayer;

    /// <summary>
    /// The game over user interface.
    /// </summary>
    public GameObject gameOverUI;

    /// <summary>
    /// This is responsible for restarting the level.
    /// </summary>
    public void Restart() {
        FindObjectOfType<CameraController>().rotateSpeed = 1;
        FindObjectOfType<HealthManager>().currentHealth = FindObjectOfType<HealthManager>().maxHealth;
        gameOverUI.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// This is responsible for returning the player to the position they were in at the beginning
    /// of the first level.
    /// </summary>
    public void PlayAgain() {
        FindObjectOfType<CameraController>().rotateSpeed = 1;
        FindObjectOfType<HealthManager>().currentHealth = FindObjectOfType<HealthManager>().maxHealth;
        gameOverUI.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        thePlayer.transform.position = startPlayerPos.transform.position;
    }

    /// <summary>
    /// This is responsible for returning the player to the position they were in at the beginning
    /// of the second level.
    /// </summary>
    public void PlayAgain2()
    {
        SceneManager.LoadScene(2);
        FindObjectOfType<CameraController>().rotateSpeed = 1;
        FindObjectOfType<HealthManager>().currentHealth = FindObjectOfType<HealthManager>().maxHealth;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
    }

    /// <summary>
    /// Responsible for loading the main menu.
    /// </summary>
    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    /// <summary>
    /// Quits the game.
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}
