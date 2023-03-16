using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script will fire upon enemies. The Primary Script (BossDrEnemyTrigger) handles rotation.
public class BossDrFireTrigger : MonoBehaviour
{
    [SerializeField] BossDrEnemyTrigger primaryTriggerScript;
    [SerializeField] BossDrEnemy mainEnemyScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*
    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("DamageHitbox")) {
            return;
        }
        
        if (primaryTriggerScript.currentTarget != null) {
            StartCoroutine(TrackStayTime());
        }
    }
    void OnTriggerStay2D(Collider2D other) {
        if (other.CompareTag("DamageHitbox")) {
            return;
        }
        
        //print("stay");
        
        if (primaryTriggerScript.currentTarget.transform.CompareTag("Player")) {
            if (canFire && aimedIn) {
                // attack the player
                print("Fire at Player");
                StartCoroutine(FireCooldown(fireCooldown));
                StartCoroutine(mainEnemyScript.FireGun(currentTarget.GetComponent<BoxCollider2D>()));
            } 
        } else if (canFire) {
            print("Fire else");
            StartCoroutine(FireCooldown(2f));
            StartCoroutine(mainEnemyScript.FireGun(primaryTriggerScript.currentTarget.GetComponent<BoxCollider2D>()));
        }

        
    }*/

    
}
