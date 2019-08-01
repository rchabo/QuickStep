using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Abhinav @ https://www.youtube.com/watch?v=L2lldjxHJTo&list=LL5b6GEL0U7GDxiDH4JArM6Q&index=2&t=0s
/// Modified by: Robin Chabouk
/// This class is responsible for giving projectiles a 'time to live'
/// </summary>
public class Projectile : MonoBehaviour
{

    public float expiryTime = 0f;

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Start()
    {

        Destroy(gameObject, expiryTime);

    }

    /// <summary>
    ///  Update is called once per frame
    /// </summary>
    void Update()
    {

    }
}
