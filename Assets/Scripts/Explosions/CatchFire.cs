using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Robin Chabouk
/// Script for object catching fire.
/// </summary>
public class CatchFire : MonoBehaviour {

    /// <summary>
    /// The effect to be instansiated on the object. In this case fire.
    /// </summary>
    public GameObject fire;


	/// <summary>
    /// Instantiate a fire on the object this script is attached to when the script is called.
    /// </summary>
	void Start () {
        Instantiate(fire, transform.position, transform.rotation);
    }
}
