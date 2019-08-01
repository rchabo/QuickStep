using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Robin Chabouk.
/// Script for detroying the fire after a set time.
/// </summary>
public class DestroyFire : MonoBehaviour {

    /// <summary>
    /// Countdown until the fire is destroyed.
    /// </summary>
    public float fireCountdown;

    /// <summary>
    /// Delay (how long countdown should be), countdown is set to this.
    /// </summary>
    public float fireDelay = 5f;

    /// <summary>
    /// Initialse fireCountDown to be equal to fireDelay.
    /// </summary>
    void Start () {
        fireCountdown = fireDelay;
    }

    /// <summary>
    /// When fireCountDown reaches 0, destroy the fire patricle effect.
    /// </summary>
    void Update()
    {
        fireCountdown -= Time.deltaTime;
        if (fireCountdown <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
