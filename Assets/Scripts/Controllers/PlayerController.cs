using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Author: James Doyle @ https://www.youtube.com/watch?v=ZNhs8lbJDbE&t=5s
/// Modified by: Robin Chabouk
/// This script is responsible for the movement of the player.
/// </summary>
public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// Store the number of units the player will move each frame.
    /// </summary>
    public float moveSpeed;

    /// <summary>
    /// The number of units the player will move up by when they jump.
    /// </summary>
    public float jumpForce;

    /// <summary>
    /// Boolean of whether the player currently meets the conditions to preform 
    /// a second jump.
    /// </summary>
    private bool candoublejump;

    /// <summary>
    /// A CharacterController is used to move objects without having to
    /// deal with the physics of that object. It comes with its own set
    /// of methods for movement.
    /// </summary>
    public CharacterController controller;

    /// <summary>
    /// Vector3 is a data type which represents a 3 dimensional vector.
    /// </summary>
    private Vector3 moveDirection;

    /// <summary>
    /// The scale by which the gravity should be increased/decreased by.
    /// </summary>
    public float gravityScale;

    /// <summary>
    /// The animator that controls the animations of the player character.
    /// </summary>
    public Animator anim;

    /// <summary>
    /// The pivot the player character rotates around.
    /// </summary>
    public Transform pivot;

    /// <summary>
    /// The speed the player rotates at.
    /// </summary>
    public float rotateSpeed;

    /// <summary>
    /// The model that represents the player.
    /// </summary>
    public GameObject playerModel;

    /// <summary>
    /// The force to knock the player back by should they take damage.
    /// </summary>
    public float knockBackForce;

    /// <summary>
    /// The time they should be knocked back for.
    /// </summary>
    public float knockBackTime;

    /// <summary>
    /// Counter for how much longer the player should be knocked
    /// back for.
    /// </summary>
    private float knockBackCounter;

    /// <summary>
    /// Used for initialization, in this case the controller is being
    /// initialized
    /// </summary>
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    /// <summary>
    /// Update is called once per frame. The players position, direction they face etc.
    /// is calculated every frame.
    /// </summary>
    void Update()
    {
        if (knockBackCounter <= 0)
        {
            /// <summary>
            /// Store the y component of the character.
            /// </summary>
            float yStore = moveDirection.y;
            moveDirection = (transform.forward * Input.GetAxisRaw("Vertical")) + (transform.right * Input.GetAxisRaw("Horizontal"));
            moveDirection = moveDirection.normalized * moveSpeed;
            moveDirection.y = yStore;

            // If the character is on the ground, pressing the spacebar will make the character jump.
            if (controller.isGrounded)
            {
                moveDirection.y = 0f;
                if (Input.GetButtonDown("Jump"))
                {
                    moveDirection.y = jumpForce;
                    candoublejump = true;

                }
            }
            else
            {
                if (Input.GetButtonDown("Jump") && candoublejump)
                {
                    candoublejump = false;
                    moveDirection.y = jumpForce;
                }
            }
        }
        else {
            knockBackCounter -= Time.deltaTime;
        }

        //Physics.gravity is a default gravity mechanism that is built into Unity.
        //Time.deltaTime is the time it took to complete the last frame, so that framerate doesn't affect moveSpeed
        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
        controller.Move(moveDirection * Time.deltaTime);

        //Moves the player based on the direction the camera is facing
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) {
            transform.rotation = Quaternion.Euler(0f, pivot.rotation.eulerAngles.y, 0f);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
            playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
        }

        anim.SetBool("isGrounded", controller.isGrounded);
        anim.SetFloat("Speed", (Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal"))));
    }

    /// <summary>
    /// Method for knocking back the player, used when the player takes damage.
    /// </summary>
    /// <param name="direction">The direction the player should be knocked in</param>
    public void KnockBack(Vector3 direction) {
        knockBackCounter = knockBackTime;

        moveDirection = direction * knockBackForce;
        moveDirection.y = knockBackForce;
    }
}
