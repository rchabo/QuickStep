using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// Author: Robin Chabouk
/// This script is responsible for keeping track of the boss' health 
/// and doing what is appropriate based on this information.
/// </summary>
public class BossHealth : MonoBehaviour {

    /// <summary>
    /// The maximum health of the boss.
    /// </summary>
    public float maxBossHealth = 10f;

    /// <summary>
    /// The current health of the boss.
    /// </summary>
    public float currentBossHealth;

    /// <summary>
    /// The number in the GUI displaying the boss' current health to the player.
    /// </summary>
    public TextMeshProUGUI bossHealth;

    /// <summary>
    /// How much damage the boss should take whenever a projectile makes contact with them.
    /// </summary>
    public float damageToTake = 1f;

    /// <summary>
    /// Variable used to countdown for 'next level' UI to pop up.
    /// </summary>
    public float count;

    /// <summary>
    /// Used to initialise how long the countdown should be.
    /// </summary>
    public float procdelay = 3f;

    /// <summary>
    /// An image used for the health bar.
    /// </summary>
    public Image healthBar;

    /// <summary>
    /// The animator that controls the boss.
    /// </summary>
    public Animator anim;

    /// <summary>
    /// Boolean for whether the boss is dead or not.
    /// </summary>
    public bool isDead = false;

    /// <summary>
    /// Audioclip for the grunt sound which is played when the boss takes damage.
    /// </summary>
    public AudioClip grunt;

    /// <summary>
    /// The source of the audio coming from the boss.
    /// </summary>
    private AudioSource source;

    /// <summary>
    /// The UI that pops up when the boss is defeated and level 2 is complete.
    /// </summary>
    public GameObject Level2CompleteUI;

    /// <summary>
    /// Initalise audio source before the game starts.
    /// </summary>
    void Awake() {
        source = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Start is run during the frame that the script is enabled. Count is initalised and
    /// the boss' current health is set to equal the maximum health.
    /// </summary>
    void Start () {
        count = procdelay;
        currentBossHealth = maxBossHealth;
	}
	
	/// <summary>
    /// Update is called every frame. Every frame, the script checks
    /// whether the boss' health has been depleted and if it has runs a series
    /// of actions that result in the player progressing to the next level.
    /// </summary>
	void Update () {
        bossHealth.text = "Cactus Health: ";
        if (currentBossHealth <= 0) {
            isDead = true;
            anim.SetBool("isDead", isDead);
            count -= Time.deltaTime;
            if (count <= 0)
            {
                Level2CompleteUI.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                FindObjectOfType<CameraController>().rotateSpeed = 0f;
                Time.timeScale = 0f;
            }
        }
    }


    /// <summary>
    /// If the boss collides with a projectile fired by the player,
    /// take some damage and play the appropriate animation and sound.
    /// </summary>
    /// <param name="other">The collider that has entered the trigger area</param>
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "PlayerProjectile")
        {
            currentBossHealth -= damageToTake;
            healthBar.fillAmount = currentBossHealth / maxBossHealth;
            source.PlayOneShot(grunt);
            anim.Play("Hit", 0, 0);
        }
    }
}
