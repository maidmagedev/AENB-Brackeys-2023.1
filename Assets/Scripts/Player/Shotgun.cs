using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
{
    public override void GetInput() {
        if (Input.GetMouseButtonDown(0) && mayshoot) {
            if (clip > 0) {
                StartCoroutine(FireGun());       // press and release for firing
                //FireGunHeld(); // hold for firing
                StartCoroutine(AnimatorFire());
                clip--;
            }
        }
    } 
    
    // Press and Release for Fire
    /*********************************************************************/
    public override IEnumerator FireGun()
    {
        mayshoot = false;

        //audioSource.PlayOneShot(fireSound, volume);
        
        yield return new WaitForSeconds(0.04f);
        foreach (Transform t in bulletOrigins) {
            Instantiate(projectile_prefab, t.position, t.rotation);
        }
        
        
        yield return new WaitForSeconds(0.25f);
        StartCoroutine(FireRepeatTimer());
        
    }

    public override IEnumerator AnimatorFire() {
        firingLayer++;
        priority.Remove(AnimationStates.Fire);
        yield return new WaitForEndOfFrame();
        priority.Add(AnimationStates.Fire);
        yield return new WaitForSeconds(0.75f);
        firingLayer--;
        if (firingLayer == 0) {
            priority.Remove(AnimationStates.Fire);
        }
         if (clip == 0 && !reloading) {
            reloading = true;
            StartCoroutine(Reloading());
        }

    }

    private IEnumerator Reloading() {
        priority.Add(AnimationStates.Reload);
        yield return new WaitForSeconds(1.25f);
        priority.Remove(AnimationStates.Reload);
        reloading = false;
        mayshoot = true;
        clip = 2;
    }
}
