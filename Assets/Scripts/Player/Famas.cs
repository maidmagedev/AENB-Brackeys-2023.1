using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Famas : Gun
{
    public override void GetInput() {
        if (Input.GetMouseButtonDown(0) && mayshoot) {
            StartCoroutine(FireGun());       // press and release for firing
            //FireGunHeld(); // hold for firing
            StartCoroutine(AnimatorFire());

        }
    } 
    
    

    // Press and Release for Fire
    /*********************************************************************/
    public override IEnumerator FireGun()
    {
        mayshoot = false;

        audioSource.PlayOneShot(fireSound, volume);
        // Burst Fire
        
        for (int i = 0; i < burstCount; i++) {
            
            Instantiate(projectile_prefab, bulletOrigins[0].position, bulletOrigins[0].rotation);
            yield return new WaitForSeconds(burstGap);
        }
        
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

    }
    

    public IEnumerator Equip() {
        mayshoot = false;
        priority.Add(AnimationStates.Equip);
        yield return new WaitForSeconds(0.833f);
        priority.Remove(AnimationStates.Equip);
        mayshoot = true;
    }
}
