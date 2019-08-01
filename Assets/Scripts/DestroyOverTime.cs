using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is responsible for destroying an object
/// when its lifetime has ended.
/// </summary>
public class DestroyOverTime : MonoBehaviour {

    /// <summary>
    /// The lifetime of the object to be destroyed.
    /// </summary>
    public float lifetime;
    
	
	// Update is called once per frame
	void Update () {
        Destroy(gameObject, lifetime);
	}
}
