using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// main intent: 
public class BossDrAnimationEvents : MonoBehaviour
{
    [Header("Bullet")]
    [SerializeField] GameObject projectile_prefab;
    [SerializeField] Transform bulletOrigin;

    [Header("Sound")]
    [SerializeField] public AudioSource audioSource;
    [SerializeField] public AudioClip fireSound;
    [SerializeField] public AudioClip deathSound;
    [SerializeField] public float volume = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void fireBullet() {
        Instantiate(projectile_prefab, bulletOrigin.position, bulletOrigin.rotation);
        audioSource.PlayOneShot(fireSound, volume);
    }

    
}
