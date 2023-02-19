using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [Header("Keybinds")]
    public KeyCode pauseKey = KeyCode.Escape;
    public KeyCode inventoryKey = KeyCode.I;
    public KeyCode interactKey = KeyCode.E;

    [Header("Audio")]
    public float masterVolume = 0.5f; // percentage
    [SerializeField] private Slider slider;
    [SerializeField] private AudioSource mainAudio;
    // Start is called before the first frame update
    void Start()
    {
        mainAudio.volume = masterVolume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetVolume()
    {
        masterVolume = slider.value;
        mainAudio.volume = masterVolume;
    }
}
