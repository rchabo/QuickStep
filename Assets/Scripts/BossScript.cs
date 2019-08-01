using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Author: Abhinav @ https://www.youtube.com/watch?v=L2lldjxHJTo&list=LL5b6GEL0U7GDxiDH4JArM6Q&index=2&t=0s
/// Modified by: Robin Chabouk
/// This class is responsible for the movement of the boss and instaniating the projectile 
/// at the starting point of its trajectory.
/// </summary>
public class BossScript : MonoBehaviour
{
    /// <summary>
    /// The GameObject that will be representing the projectile.
    /// </summary>
    public GameObject eprojectile;
    /// <summary>
    /// The position of the 'hand' where the projectile will be
    /// launched from
    /// </summary>
    public Transform ehand;

    /// <summary>
    /// The radius which the boss is aware of. If the player leaves
    /// this radius the boss will start moving towards them.
    /// </summary>
    public float lookRadius;

    /// <summary>
    /// The boss' target.
    /// </summary>
    public Transform target;

    /// <summary>
    /// How often the projectile can be fired, in other words,
    /// the delay between each shot.
    /// </summary>
    public float eshootRate = 0f;
    /// <summary>
    /// How much force the projectile will be fired with.
    /// </summary>
    public float eshootForce = 0f;
    /// <summary>
    /// The Time since the last shot was fired.
    /// </summary>
    private float eshootRateTimeStamp = 0f;

    /// <summary>
    /// The Update method is called once every frame.
    /// </summary>
    void Update()
    {
        ehand.LookAt(target);
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius && FindObjectOfType<BossHealth>().isDead == false)
        {
            if (Time.time > eshootRateTimeStamp)
            {
                ///<summary>
                /// Create a new game object, which in this case a projectile, and launch it
                /// from the position of the empty object ehand in the forward direction, with force shootForce.
                /// </summary>
                GameObject go = (GameObject)Instantiate(
                    eprojectile, ehand.position, ehand.rotation);
                go.GetComponent<Rigidbody>().AddForce(ehand.forward * eshootForce);
                eshootRateTimeStamp = Time.time + eshootRate;
            }
        }


    }
}
