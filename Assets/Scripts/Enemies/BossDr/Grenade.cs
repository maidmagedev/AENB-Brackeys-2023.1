using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is currently being used by the Boss Doctor.
// This script throws a rigidbody to a target position, and then targets animations for the grenade.
// Once the anim is over, we activate an explosion game object which deals the damage.
//
// This script assumes the hierarchy:
// Container Object
// - Grenade Script Holder
// - Landing Collider (Using FollowObj.cs w/ MatchXOnly - Only follow the X axis movement)
// - - Explosion GameObject (With BasicProjectile.cs, or a not yet existing at time of writing explosion damage script) 
public class Grenade : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float airTime = 1.5f;
    [SerializeField] float tickTime = 2f;
    [SerializeField] bool targetPlayer = true;
    [Header("References")]
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Transform origin;
    [SerializeField] Transform target;
    [SerializeField] Animator anim;
    [SerializeField] GameObject explosion;
    [Header("Sound")]
    [SerializeField] public AudioSource audioSource;
    [SerializeField] public AudioClip fireSound;
    [SerializeField] public float volume = 5f;
    
    // Start is called before the first frame update
    void Start()
    {
        if (audioSource == null) {
            audioSource = FindObjectOfType<AudioSource>();
        }
        if (targetPlayer && target == null) {
            target = FindObjectOfType<PlayerActions>().transform;
        }
        Throw();
    }

    void Throw() {
        print("adding force");
        //rb.AddForce(Vector2.left * 100f, ForceMode2D.Force);
        
        float distance = target.position.x - origin.position.x;
        print("Distance to target: " + distance);

        rb.velocity = new Vector3(distance / airTime, 0, 0); 
        rb.AddForce(Vector2.up * 2, ForceMode2D.Impulse);
        StartCoroutine(StartTickTimer());
    }

    IEnumerator StartTickTimer() {
        yield return new WaitForSeconds(airTime);
        anim.SetTrigger("IsTicking");
        yield return new WaitForSeconds(tickTime);
        explosion.SetActive(true);
        audioSource.PlayOneShot(fireSound, volume);
        yield return new WaitForSeconds(0.1f);
        anim.SetTrigger("Hide");
        StartCoroutine(DestroyThis());
    }


    IEnumerator DestroyThis() {
        
        yield return new WaitForSeconds(0.5f);
        Destroy(this.transform.parent.gameObject); // If the hierarchy has changed since the original creation of this script, this line should change as well.
    }
}
