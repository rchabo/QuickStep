using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: James Doyle @ https://www.youtube.com/watch?v=ZNhs8lbJDbE&t=5s
/// Modified by: Robin Chabouk
/// This script is responsible for decreasing the players health
/// when they take damage.
/// </summary>
public class HurtPlayer : MonoBehaviour {

    /// <summary>
    /// How much damage the player should take when this script is called.
    /// </summary>
    public int damageToGive = 1;

    /// <summary>
    /// If the player collides with an object that can hurt them, call
    /// the HurtPlayer script. If the player enters the trigger area,
    /// the HurtPlayer method is called.
    /// </summary>
    /// <param name="other">The collider that has entered the trigger area</param>
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            Vector3 hitDirection = other.transform.position - transform.position;
            hitDirection = hitDirection.normalized;
            FindObjectOfType<HealthManager>().HurtPlayer(damageToGive, hitDirection);
        }
    }
}
