using UnityEngine;
using System.Collections;

/// <summary>
/// Author: Abhinav @ https://www.youtube.com/watch?v=L2lldjxHJTo&list=LL5b6GEL0U7GDxiDH4JArM6Q&index=2&t=0s
/// Modified by: Robin Chabouk
/// This class if responsible for managing the trajectory of the projectile
/// </summary>
public class ThrowSimulation : MonoBehaviour
{
    /// <summary>
    /// The location of the target.
    /// </summary>
    public Transform Target;
    /// <summary>
    /// The angle at which the projectile will be fired.
    /// </summary>
    public float firingAngle = 45.0f;
    /// <summary>
    /// The gravity force that will be acting on the projectile.
    /// </summary>
    public float gravity = 9.8f;

    /// <summary>
    /// The position of the projectile
    /// </summary>
    public Transform Projectile;

    /// <summary>
    /// Store a location.
    /// </summary>
    private Transform myTransform;

    /// <summary>
    /// Initialize anyvariables and game state before the game starts.
    /// Called when the script is initialized, not necessarily when it 
    /// is enabled.
    /// </summary>
    void Awake()
    {
        myTransform = transform;
    }

    /// <summary>
    /// Called when the script is run.
    /// </summary>
    /// <remarks>
    /// StartCoroutine prevents SimulateProjectile() from executing in one frame.
    /// This allows the player to see the ball move gradually. StartCoroutine(SimulateProjectile())
    /// has the ability to 'pause' the SimulateProjectile method and return control to Unity until the
    /// next frame. This means SimulateProjectile is run more incrementaly over several frames rather
    /// than just one.
    /// </remarks>
    void Start()
    {
        StartCoroutine(SimulateProjectile());
    }

    /// <summary>
    /// This method computes the velocity of the projectile.
    /// </summary>
    /// <remarks>
    /// IEnumerator supports an iteration over a non generic collection of which
    /// we don't know the size of. 
    /// </remarks>
    /// <returns>
    /// Returns control to the coroutine for WaitForSeconds amount of time, and once every iteration
    /// of the while loop near the end of the method.
    /// </returns>
    IEnumerator SimulateProjectile()
    {
        // Short delay added before Projectile is thrown
        yield return new WaitForSeconds(1.5f);

        // Move projectile to the position of throwing object + add some offset if needed.
        Projectile.position = myTransform.position + new Vector3(0, 0.0f, 0);

        // Calculate distance to target
        float target_Distance = Vector3.Distance(Projectile.position, Target.position);

        // Calculate the velocity needed to throw the object to the target at specified angle.
        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

        // Extract the X  Y componenent of the velocity
        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        // Calculate flight time.
        float flightDuration = target_Distance / Vx;

        // Rotate projectile to face the target.
        Projectile.rotation = Quaternion.LookRotation(Target.position - Projectile.position);

        /// <summary>
        /// Time elapsed since the launch of the projectile.
        /// </summary>
        float elapse_time = 0;

        while (elapse_time < flightDuration)
        {
            Projectile.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);

            elapse_time += Time.deltaTime;

            yield return null;
        }
    }
}