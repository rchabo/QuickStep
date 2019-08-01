using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Sebastian Lague @ https://www.youtube.com/watch?v=dn1XRIaROM4&list=PLFt_AvWsXl0cq5Umv3pMC9SPnKjfp9eGW&index=5
/// Modified by: Robin Chabouk.
/// </summary>
public class Unit : MonoBehaviour {

    /// <summary>
    /// The Unit will only move if the target is not within its radius.
    /// </summary>
    public int lookRadius = 50;

    /// <summary>
    /// The target/target destination.
    /// </summary>
    public Transform target;

    /// <summary>
    /// The speed of the Unit.
    /// </summary>
    public float speed = 20;

    /// <summary>
    /// The path the Unit will take.
    /// </summary>
    Vector3[] path;

    /// <summary>
    /// The index of the target node in the heap.
    /// </summary>
    int targetIndex;

    /// <summary>
    /// Make sure the Unit facing the target at all times.
    /// </summary>
    void Update () {
        PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
        if (FindObjectOfType<BossHealth>().isDead == false) {
            transform.LookAt(target);
        }
    }

    /// <summary>
    /// If a path is successfully found, start following that path.
    /// </summary>
    /// <param name="newPath">The new path to be followed.</param>
    /// <param name="pathSuccessful">Boolean for whether a path was successfully found.</param>
    public void OnPathFound(Vector3[] newPath, bool pathSuccessful) {
        if (pathSuccessful) {
            path = newPath;
            targetIndex = 0;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    /// <summary>
    /// Follow the path that was found.
    /// </summary>
    /// <returns>null, and continue execution of this method in the next frame.</returns>
    IEnumerator FollowPath() {
        Vector3 currentWaypoint = path[0];
        while (true) {
            if (transform.position == currentWaypoint) {
                targetIndex++;
                if (targetIndex >= path.Length) {
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }
            float distance = Vector3.Distance(target.position, transform.position);
            if (distance >= lookRadius && FindObjectOfType<BossHealth>().isDead == false) {
                transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
            }
            yield return null;

        }
    }

    /// <summary>
    /// Gizmo for drawing the found path, only visible in editor mode.
    /// </summary>
    public void OnDrawGizmos() {
        if (path != null) {
            for (int i = targetIndex; i < path.Length; i++) {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], Vector3.one);

                if (i == targetIndex) {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }
}
