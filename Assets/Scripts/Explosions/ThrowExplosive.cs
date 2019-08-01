using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Brackeys (Asbjørn Thirslund) @ https://www.youtube.com/watch?v=BYL6JtUdEY0
/// Modified by: Robin Chabouk
/// </summary>
public class ThrowExplosive : MonoBehaviour {

    /// <summary>
    /// Force with which to throw the explosive.
    /// </summary>
    public float throwForce = 40f;

    /// <summary>
    /// The explosive to throw.
    /// </summary>
    public GameObject explosivePrefab;
	
	/// <summary>
    /// If the player clicks, throw some dynamite.
    /// </summary>
	void Update () {
        if (Input.GetMouseButtonDown(0)) {
            throwGrenade();
        }
	}

    /// <summary>
    /// Method for throwing a grenade.
    /// </summary>
    void throwGrenade() {
        GameObject explosive = Instantiate(explosivePrefab, transform.position, transform.rotation);
        Rigidbody rb = explosive.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
    }
}
