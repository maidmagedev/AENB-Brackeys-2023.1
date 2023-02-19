using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusic : MonoBehaviour
{
    [SerializeField] private List<AudioClip> day_tracks;
    [SerializeField] private List<AudioClip> night_tracks;
    [SerializeField] private AudioSource myAudio;

    // Start is called before the first frame update
    void Start()
    {
        myAudio.clip = day_tracks[SceneManager.GetActiveScene().buildIndex];
        myAudio.Play();
    }
    public void Play_night_track()
    {
        myAudio.clip = night_tracks[SceneManager.GetActiveScene().buildIndex];
        myAudio.Play();
    }
    public void Play_day_track()
    {
        myAudio.clip = day_tracks[SceneManager.GetActiveScene().buildIndex];
        myAudio.Play();
    }
}
