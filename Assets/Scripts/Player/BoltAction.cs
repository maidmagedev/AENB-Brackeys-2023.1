using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltAction : Gun
{
    private bool mustBolt = true;
    [SerializeField] AudioClip boltSound;


    public override void GetInput() {
        
        if (Input.GetMouseButtonDown(0) && mayshoot && !reloading && !mustBolt) {
            
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
        print("fire");
        mayshoot = false;
        mustBolt = true;
        
        yield return new WaitForSeconds(0.07f);
        Instantiate(projectile_prefab, bulletOrigins[0].position, bulletOrigins[0].rotation);
        audioSource.PlayOneShot(fireSound, volume);
        yield return new WaitForSeconds(0.747f);
       
        //StartCoroutine(FireRepeatTimer());
        mayshoot = true;
    }

    public override IEnumerator AnimatorFire() {
        priority.Add(AnimationStates.Fire);
        yield return new WaitForSeconds(0.817f);
        priority.Remove(AnimationStates.Fire);
        if (clip == 0 && !reloading) {
            reloading = true;
            StartCoroutine(Reloading());
            yield return new WaitForSeconds(1.433f);
        }
        if (mustBolt) {
            StartCoroutine(Bolt());
        }
        
    }
    private IEnumerator Bolt() {
        print("Bolt bolt");
        priority.Add(AnimationStates.Extra);
        yield return new WaitForSeconds(0.567f);
        priority.Remove(AnimationStates.Extra);
        audioSource.PlayOneShot(boltSound, volume);
        mustBolt = false;
    }

    private IEnumerator Reloading() {
        print("Reload Bolt");
        priority.Add(AnimationStates.Reload);
        yield return new WaitForSeconds(1.433f);
        priority.Remove(AnimationStates.Reload);
        reloading = false;
        clip = 5;
        mayshoot = true;
    }

    public IEnumerator Equip() {
        print("Equip bolt");
        mayshoot = false;
        priority.Add(AnimationStates.Equip);
        yield return new WaitForSeconds(0.667f);
        priority.Remove(AnimationStates.Equip);
        mayshoot = true;
        if (mustBolt) {
            StartCoroutine(Bolt());
        }
    }
}
