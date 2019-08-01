using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Author: Robin Chabouk
/// Responsible for showing the appropriate UI for when the player completes 
/// the second level.
/// </summary>
public class Level2Complete : MonoBehaviour {

    /// <summary>
    /// Method for loading the third level, explosions.
    /// </summary>
    public void LoadExplosions()
    {
        SceneManager.LoadScene(3);
        Time.timeScale = 1f;
    }
}
