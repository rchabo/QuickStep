using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Brackeys (Asbjørn Thirslund) @ https://www.youtube.com/watch?v=BYL6JtUdEY0
/// Modified by: Robin Chabouk
/// </summary>

public class Dynamite : MonoBehaviour {

    /// <summary>
    /// Delay before dynamite explodes
    /// </summary>
    public float delay = 3f;

    /// <summary>
    /// The radius around dynamite that is affected by the blast.
    /// </summary>
    public float blastRadius = 5f;

    /// <summary>
    /// The force of the explosion.
    /// </summary>
    public float explosionForce = 700f;

    /// <summary>
    /// The particle effect used for the explosion.
    /// </summary>
    public GameObject explosionEffect;

    /// <summary>
    /// Variable used for a countdown.
    /// </summary>
    float countdown;

    /// <summary>
    /// Whether the dynamite has exploded or not.
    /// </summary>
    bool hasExploded = false;

	/// <summary>
    /// Initialise countdown to equal delay.
    /// </summary>
	void Start () {
        countdown = delay;
	}
	
	/// <summary>
    /// When the countdown reaches 0, explode the dynamite.
    /// </summary>
	void Update () {
        countdown -= Time.deltaTime;
        if (countdown <= 0f && !hasExploded) {
            Explode();
        }
	}

    /// <summary>
    /// Explode the dynamite, check if anything around it should have a force applied to it.
    /// </summary>
    void Explode() {
        Instantiate(explosionEffect, transform.position, transform.rotation);

        Collider[] collidersToDestroy = Physics.OverlapSphere(transform.position, blastRadius);

        foreach (Collider nearbyObject in collidersToDestroy) {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();

            if (rb != null) {
                rb.AddExplosionForce(explosionForce, transform.position, blastRadius);
            }
            Destructible dest = nearbyObject.GetComponent<Destructible>();
            if (dest != null) {
                dest.DestroyStuff();
            }
        }

        Collider[] collidersToMove = Physics.OverlapSphere(transform.position, blastRadius);

        foreach (Collider nearbyObject in collidersToMove) {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null) {
                rb.AddExplosionForce(explosionForce, transform.position, blastRadius);
            }
        }

        Destroy(gameObject);
    }
}
