using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Gun
{
    public override void GetInput() {
        if (Input.GetMouseButtonDown(0) && mayshoot && !reloading) {
            if (clip > 0) {
                clip--;
                StartCoroutine(FireGun());       // press and release for firing
                //FireGunHeld(); // hold for firing
                StartCoroutine(AnimatorFire());
                
            }
        }
    } 
    
    // Press and Release for Fire
    /*********************************************************************/
    public override IEnumerator FireGun()
    {
        mayshoot = false;

        
        yield return new WaitForSeconds(0.04f);
        Instantiate(projectile_prefab, bulletOrigins[0].position, bulletOrigins[0].rotation);
        audioSource.PlayOneShot(fireSound, volume);

        
        
        
        yield return new WaitForSeconds(0.11f);
        //StartCoroutine(FireRepeatTimer());
        
    }

    public override IEnumerator AnimatorFire() {
        firingLayer++;
        priority.Remove(AnimationStates.Fire);
        yield return new WaitForEndOfFrame();
        priority.Add(AnimationStates.Fire);
        yield return new WaitForSeconds(0.483f);
        firingLayer--;
        if (firingLayer == 0) {
            priority.Remove(AnimationStates.Fire);
            mayshoot = true;
        }
        if (clip == 0 && !reloading) {
            reloading = true;
            StartCoroutine(Reloading());
        }

    }

    private IEnumerator Reloading() {
        priority.Add(AnimationStates.Reload);
        yield return new WaitForSeconds(1.05f);
        priority.Remove(AnimationStates.Reload);
        reloading = false;
        mayshoot = true;
        clip = 12;
    }
}
