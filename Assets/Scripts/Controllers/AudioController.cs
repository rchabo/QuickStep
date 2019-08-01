using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Author: Arshel Tutorials @ https://www.youtube.com/watch?v=VJwbPm98dBg
/// Modified by: Robin Chabouk.
/// This script is responsible for controlling the game's audio.
/// </summary>
public class AudioController : MonoBehaviour {

    /// <summary>
    /// The slider gameobject that is to control the volume.
    /// </summary>
    public Slider volume;

    /// <summary>
    /// The audiosource to be controlled.
    /// </summary>
    public AudioSource music;

    /// <summary>
    /// Initialise the starting volume of the audio for the level.
    /// </summary>
    void Start() {
        volume.value = 100;
    }

    /// <summary>
    /// Update is called once per frame. Make the volume oif the audiosource
    /// equal to the value given by the slider.
    /// </summary>
    void Update () {
        music.volume = volume.value;
	}
}
