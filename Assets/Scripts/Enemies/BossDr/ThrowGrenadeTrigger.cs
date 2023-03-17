using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowGrenadeTrigger : MonoBehaviour
{
    [SerializeField] GameObject grenade;
    [SerializeField] Transform instantiationPoint;
    private bool canThrowGrenade = true;
    [SerializeField] float grenadeCooldown;
    void OnTriggerEnter2D(Collider2D other) {
        if (other.transform.CompareTag("Player") && canThrowGrenade) {
            Instantiate(grenade, instantiationPoint.position, instantiationPoint.rotation);
            StartCoroutine(GrenadeCooldownClock());
        }
    }

    IEnumerator GrenadeCooldownClock() {
        canThrowGrenade = false;
        yield return new WaitForSeconds(grenadeCooldown);
        canThrowGrenade = true;
    }
}
