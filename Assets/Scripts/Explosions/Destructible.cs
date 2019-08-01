// --------------------------------------
// This script is totally optional. It is an example of how you can use the
// destructible versions of the objects as demonstrated in my tutorial.
// Watch the tutorial over at http://youtube.com/brackeys/.
// --------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Brackeys @ https://www.youtube.com/watch?v=EgNV0PWVaS8
/// Modified by: Robin Chabouk
/// </summary>
public class Destructible : MonoBehaviour {

    /// <summary>
    /// Reference to the shattered version of the object
    /// </summary>
	public GameObject destroyedVersion;

    /// <summary>
    /// Initalise variables for explosions level.
    /// </summary>
    void Start() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
    }

    /// <summary>
    /// If the player clicks on the object, throw a dynamite.
    /// </summary>
    public void DestroyStuff()
	{
		// Spawn a shattered object
		Instantiate(destroyedVersion, transform.position, transform.rotation);
		// Remove the current object
		Destroy(gameObject);
	}

}
