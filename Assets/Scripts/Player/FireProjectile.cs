using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : MonoBehaviour
{
    
    // This script will fire a basic projectile
    // It is meant to be used in conjunction with Basic_Projectile.cs or similar

    [SerializeField] private GameObject projectile_prefab; // could make this a Resources.Load() call
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip fireSound;
    [SerializeField] private float volume = 10f;

    [SerializeField] float fireCooldown = 0;
    [SerializeField] float fireRate = 1f;
    
    private bool mayshoot = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FireGun();       // press and release for firing
        //FireGunHeld(); // hold for firing
    }
    
    // Press and Release for Fire
    /*********************************************************************/
    private void FireGun()
    {
        if (Input.GetMouseButtonDown(0) && mayshoot)
        {
            //audioSource.PlayOneShot(fireSound, volume);
            Instantiate(projectile_prefab, transform.position, transform.rotation);
            mayshoot = false;
            StartCoroutine(FireRepeatTimer());
        }
    }

    // this is a setter that is useful to access when pausing the game
    public void Set_mayFire(bool FireBool)
    {
        mayshoot = FireBool;
    }

    IEnumerator FireRepeatTimer()
    {
        yield return new WaitForSeconds(fireCooldown);
        mayshoot = true;
    }
    
    
    // Hold For Fire
    /*****************************************************************/
    private void FireGunHeld()
    {
        if (Input.GetMouseButton(0))
        {
            spawnBullet();
        }
    }

    private void spawnBullet()
    {
        if (mayshoot)
        {
            //audioSource.PlayOneShot(fireSound, volume);
            mayshoot = false;
            Instantiate(projectile_prefab, transform.position, transform.rotation);
            StartCoroutine(FireRateTimer());

        }
    }
    IEnumerator FireRateTimer()
    {
        yield return new WaitForSeconds(fireRate);
        mayshoot = true;
    }
}
