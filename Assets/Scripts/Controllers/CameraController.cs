using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: James Doyle @ https://www.youtube.com/watch?v=ZNhs8lbJDbE&t=5s
/// Modified by: Robin Chabouk
/// This script controls the movement and control of the camera.
/// </summary>
public class CameraController : MonoBehaviour {

    /// <summary>
    /// Store the location and rotation of the target.
    /// </summary>
    public Transform target;

    /// <summary>
    /// The offset of the camera.
    /// </summary>
    public Vector3 offset;

    /// <summary>
    /// bool for whether we want to use camera offset values for camera placement.
    /// </summary>
    public bool useOffsetValues;

    /// <summary>
    /// Store the rotation speed of the camera.
    /// </summary>
    public float rotateSpeed;

    /// <summary>
    /// The location of the pivot for the camera.
    /// </summary>
    public Transform pivot;

    /// <summary>
    /// The maximum angle the camera can be angled at.
    /// </summary>
    public float maxViewAngle;

    /// <summary>
    /// The minimum angle the camera can be angled at.
    /// </summary>
    public float minViewAngle;

    /// <summary>
    /// Truth value for whether the camera is inverted on the Y axis.
    /// </summary>
    public bool invertY;

    /// <summary>
    /// Used for initialization.
    /// </summary>
    /// <remarks>
    /// The position of the pivot is made to be the same as the position
    /// of the target.
    /// Cursor.lockState makes the cursor invisible while the game is running, it
    /// can be brought back during testing by pressing the escape key.
    /// </remarks>
    void Start () {
        if (!useOffsetValues) {
            offset = target.position - transform.position;
        }

        pivot.transform.position = target.transform.position;
        //pivot.transform.parent = target.transform;

        pivot.transform.parent = null;

        Cursor.lockState = CursorLockMode.Locked;
	}

    /// <summary>
    /// LateUpdate() is called once per frame after Update()
    /// </summary>
    /// <remarks>
    /// The location of the cursor influences the position of the camera. They movement
    /// of the camera on the Y axis will be inverted if the boolean of invertY is true since
    /// the variable 'vertical' will be made negative. The camera rotation is limited so that
    /// it cannot go below ground level or past the point directly above the players head.
    /// </remarks>
    void LateUpdate () {

        pivot.transform.position = target.transform.position;

        /// <summary>
        /// get the X position of the mouse and rotate the target
        /// </summary>
        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        pivot.Rotate(0, horizontal, 0);

        /// <summary>
        /// Get the Y position of the mouse and rotate the pivot
        /// </summary>
        float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
        //pivot.Rotate(-vertical, 0, 0);

        if (invertY) {
            pivot.Rotate(vertical, 0, 0);
        }
        else{
            pivot.Rotate(-vertical, 0, 0);
        }

        //Move the camera based on the current rotation of the target & the original offset
        ///<summary>
        /// The desired Y angle of the camera is calculated by the rotation of the target
        /// </summary>
        float desiredYAngle = pivot.eulerAngles.y;
        ///<summary>
        /// The desired X angle of the camera is calculated by the rotation of the target
        /// </summary>
        float desiredXAngle = pivot.eulerAngles.x;

        //Limit the up/down rotation of the camera.
        if (pivot.rotation.eulerAngles.x > maxViewAngle && pivot.rotation.eulerAngles.x < 180f) {
            pivot.rotation = Quaternion.Euler(maxViewAngle, desiredYAngle, 0);
        }

        if (pivot.rotation.eulerAngles.x > 180f && pivot.rotation.eulerAngles.x < 360f + minViewAngle) {
            pivot.rotation = Quaternion.Euler(360f + minViewAngle, desiredYAngle, 0);
        }

        /// <summary>
        /// The rotation of the camera is calculated by using the X and Y desired angle componenents.
        /// </summary>
        Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);
        transform.position = target.position - (rotation * offset);

        //transform.position = target.position - offset;

        if (transform.position.y < target.position.y) {
            transform.position = new Vector3(transform.position.x, target.position.y - 0.8f, transform.position.z);
        }

        transform.LookAt(target);
	}
}
