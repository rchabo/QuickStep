using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Author: Abhinav @ https://www.youtube.com/watch?v=L2lldjxHJTo&list=LL5b6GEL0U7GDxiDH4JArM6Q&index=2&t=0s
/// Modified by: Robin Chabouk
/// This class is responsible for the movement of the player and instanciating the projectile 
/// at the starting point of its trajectory.
/// </summary>
public class PlayerScript : MonoBehaviour
{
    /// <summary>
    /// The GameObject that will be representing the projectile.
    /// </summary>
    public GameObject projectile;
    /// <summary>
    /// The position of the 'hand' where the projectile will be
    /// launched from
    /// </summary>
    public Transform hand;
    /// <summary>
    /// How often the projectile can be fired, in other words,
    /// the delay between each shot.
    /// </summary>
    public float shootRate = 0f;
    /// <summary>
    /// How much force the projectile will be fired with.
    /// </summary>
    public float shootForce = 0f;
    /// <summary>
    /// The Time since the last shot was fired.
    /// </summary>
    private float shootRateTimeStamp = 0f;

    /// <summary>
    /// This method is used for initialization, in this case the
    /// Rigidbody representing the player isnt being initialized.
    /// </summary>

    /// <summary>
    /// The Update method is called once every frame.
    /// </summary>
    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            if (Time.time > shootRateTimeStamp)
            {
                ///<summary>
                /// Create a new game object, which in this case a projectile, and launch it
                /// from the position in the forward direction, with force shootForce.
                /// </summary>
                GameObject go = (GameObject)Instantiate(
                    projectile, hand.position, hand.rotation);
                go.GetComponent<Rigidbody>().AddForce(hand.forward * shootForce);
                shootRateTimeStamp = Time.time + shootRate;
            }
        }


    }
}
