using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Author: Robin Chabouk
/// This script is responsbile for the UI that is shown when the player
/// completes the first level.
/// </summary>
public class Level1Complete : MonoBehaviour {

    /// <summary>
    /// The user interface that shows when the first level is completed.
    /// </summary>
    public GameObject Level1CompleteUI;
	
    /// <summary>
    /// If the player enteres the region of the trigger, the next
    /// level is loaded.
    /// </summary>
    /// <param name="other">The collider that has entered the trigger region</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Level1CompleteUI.SetActive(true);
            Time.timeScale = 0f;
            Cursor.visible = true;
            FindObjectOfType<CameraController>().rotateSpeed = 0f;
            Cursor.lockState = CursorLockMode.None;

        }
    }

    /// <summary>
    /// Responsible for loading the second level of the game.
    /// </summary>
    public void LoadLevel2() {
        SceneManager.LoadScene(2);
        Time.timeScale = 1f;
        FindObjectOfType<CameraController>().rotateSpeed = 1f;
        FindObjectOfType<HealthManager>().currentHealth = FindObjectOfType<HealthManager>().maxHealth;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
