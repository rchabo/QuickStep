using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: James Doyle @ https://www.youtube.com/watch?v=ZNhs8lbJDbE&t=5s
/// Modified by: Robin Chabouk
/// This script is responsible for increasing the number of pickups
/// the player has collected.
/// </summary>
public class Pickup : MonoBehaviour {

    /// <summary>
    /// The value of the pickup.
    /// </summary>
    public int value;

    /// <summary>
    /// The effect that plays when the player picks up a 
    /// pickup.
    /// </summary>
    public GameObject pickupEffect;

    /// <summary>
    /// If the player enteres the region of the trigger, the AddPickup
    /// method is called.
    /// </summary>
    /// <param name="other">The collider that has entered the trigger region</param>
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            FindObjectOfType<GameManager>().AddPickup(value);

            Instantiate(pickupEffect, transform.position, transform.rotation);

            Destroy(gameObject);
        }
    }
}
