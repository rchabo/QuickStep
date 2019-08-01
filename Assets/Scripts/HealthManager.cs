using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

/// <summary>
/// Author: James Doyle @ https://www.youtube.com/watch?v=ZNhs8lbJDbE&t=5s
/// Modified by: Robin Chabouk
/// This script changes the health of the player as required.
/// </summary>
public class HealthManager : MonoBehaviour {

    /// <summary>
    /// Store the starting health of the player
    /// </summary>
    public int maxHealth;
    /// <summary>
    /// Store the current health of the player
    /// </summary>
    public int currentHealth;

    /// <summary>
    /// The player character that is being played.
    /// </summary>
    public PlayerController thePlayer;

    /// <summary>
    /// The length of time that the player is invincible for.
    /// </summary>
    public float invincibilityLength;
    /// <summary>
    /// The counter to count how much time left until invincible time is up.
    /// </summary>
    private float invincibilityCounter;

    /// <summary>
    /// The renderer for the mesh of the player.
    /// </summary>
    public Renderer playerRenderer;

    /// <summary>
    /// Counter for how much time left for flashing.
    /// </summary>
    private float flashCounter;

    /// <summary>
    /// Length of time between each flash.
    /// </summary>
    public float flashLength = 0.1f;

    /// <summary>
    /// The text renderer that displays the player's health.
    /// </summary>
    public TextMeshProUGUI healthHUD;

    /// <summary>
    /// The UI for when the player is defeated/gameover.
    /// </summary>
    public GameObject GameOverUI;

    /// <summary>
    /// Used for initialization. Set the players current health to their
    /// maximum health. Find the player once when the script starts so that
    /// they do not need to be found every time their health needs to be changed.
    /// </summary>
    void Start () {
		currentHealth = maxHealth;

        thePlayer = FindObjectOfType<PlayerController>();
    }

    /// <summary>
    /// Update is called once per frame. Keeps track of the players current health.
    /// If the players health reaches 0, the game is over and the gameover UI is set
    /// to active. When the player takes damage they flash for a short period of time
    /// and gain invincibility during this time.
    /// </summary>
    void Update () {
        if (currentHealth <= 0) {
            GameOverUI.SetActive(true);
            Time.timeScale = 0f;
            FindObjectOfType<CameraController>().rotateSpeed = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        if (invincibilityCounter > 0) {
            invincibilityCounter -= Time.deltaTime;

            flashCounter -= Time.deltaTime;
            if (flashCounter <= 0) {
                playerRenderer.enabled = !playerRenderer.enabled;
                flashCounter = flashLength;
            }

            if (invincibilityCounter <= 0) {
                playerRenderer.enabled = true;
            }
        }
        healthHUD.text = "Health: " + (currentHealth);
    }

    /// <summary>
    /// Called by the GameManager when the player is hurt.
    /// </summary>
    /// <param name="damage">The amount of damage the player should take</param>
    public void HurtPlayer (int damage, Vector3 direction){

        if (invincibilityCounter <= 0)
        {

            if (currentHealth <= 0)
            {
                currentHealth = 0;
            }
            else
            {
                currentHealth -= damage;
                healthHUD.text = "Health: " + currentHealth;
            }

            thePlayer.KnockBack(direction);

            invincibilityCounter = invincibilityLength;

            playerRenderer.enabled = false;

            flashCounter = flashLength;
        }
    }

    /// <summary>
    /// Called by the GameManager script when the player should be healed
    /// </summary>
    /// <param name="heal">The amount of healing the player should receive.</param>
    public void HealPlayer(int heal) {
        currentHealth += heal;

        //prevent the player from being overhealed.
        if(currentHealth > maxHealth){
            currentHealth = maxHealth;
        }
    }
}
