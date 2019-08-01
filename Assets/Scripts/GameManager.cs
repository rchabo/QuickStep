using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// Author: James Doyle @ https://www.youtube.com/watch?v=ZNhs8lbJDbE&t=5s
/// Modified by: Robin Chabouk
/// Manages certain aspects of the game, such as scores.
/// </summary>
public class GameManager : MonoBehaviour {

    /// <summary>
    /// The current number of pickups the player has.
    /// </summary>
    public int currentPickup;

    /// <summary>
    /// The text on the User Interface in game, will show
    /// the player how many pickups they currently have.
    /// </summary>
    public TextMeshProUGUI pickupText;

    /// <summary>
    /// Increase the number of pickups the player has picked up.
    /// </summary>
    /// <param name="pickupToAdd">How many pickups should be added</param>
    public void AddPickup(int pickupToAdd) {
        currentPickup += pickupToAdd;
        pickupText.text = "Fish: "+currentPickup;
    }
}
