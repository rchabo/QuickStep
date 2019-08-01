using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Robin Chabouk
/// The script responsible for controlling the movement and animations
/// of the boss in the second level.
/// </summary>
public class BossController : MonoBehaviour {

    /// <summary>
    /// The radius which the boss checks the player for. If the player is
    /// in this range the boss wont move towards the player and instead shoots them.
    /// </summary>
    public float bossLookRadius;

    /// <summary>
    /// The target the boss will move towards, face and shoot.
    /// </summary>
    public Transform target;

    /// <summary>
    /// The animator that will control the boss' animations.
    /// </summary>
    public Animator anim;

    /// <summary>
    /// Update is called once per frame. Make sure the boss is always facing the target
    /// and measure the distance beteen the boss and the target each frame.
    /// </summary>
    void Update () {
        float distance = Vector3.Distance(target.position, transform.position);

        anim.SetFloat("lookDistance", Mathf.Abs(distance));
    }
}
