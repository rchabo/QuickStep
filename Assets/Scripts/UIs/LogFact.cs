using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Author: Robin Chabouk
/// The UI that is shown when they player accesses the area with the
/// log created using photogrammetry.
/// </summary>
public class LogFact : MonoBehaviour {

    /// <summary>
    /// The UI that is displayed when the player goes near the log.
    /// </summary>
    public GameObject logFactUI;

    /// <summary>
    /// When the player enters the log's hitbox, show the UI with information
    /// about the log.
    /// </summary>
    /// <param name="other">The collider that has entered the trigger.</param>
    public void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            logFactUI.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    /// <summary>
    /// Responsible for setting the logfact UI to inactive.
    /// </summary>
    public void closeLogFactUI() {
        logFactUI.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
