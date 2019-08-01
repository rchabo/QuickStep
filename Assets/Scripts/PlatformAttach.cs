using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script makes the player a child of certain platforms when
/// the player is on that platform, so that the player moves with 
/// the platform.
/// </summary>
public class PlatformAttach : MonoBehaviour {

    /// <summary>
    /// The player.
    /// </summary>
    public GameObject Player;

    /// <summary>
    /// When the player enters the trigger area of the platform, the
    /// platform becomes a parent object of the player. The player will
    /// then move relative to its parent.
    /// </summary>
    /// <param name="other">The gameobject that has entered the collider</param>
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject == Player) {
            Player.transform.parent = transform;
        }
    }

    /// <summary>
    /// Make the player no longer a child of the platform when they exit
    /// the collider.
    /// </summary>
    /// <param name="other">The gameobject that has entered the collider</param>
    private void OnTriggerExit(Collider other) {
        if (other.gameObject == Player) {
            Player.transform.parent = null;
        }
    }
}
