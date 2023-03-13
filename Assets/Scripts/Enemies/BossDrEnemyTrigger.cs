using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDrEnemyTrigger : MonoBehaviour
{
    public bool fireOnStay = false;
    public bool rotate;
    [SerializeField] BossDrEnemy mainEnemyScript;
    private IEnumerator rotateCoroutine;
    bool did = false;
    bool isInCollider = false;
    float stayTime = 0;
    float timeTillFire = 1f;
    float fireCooldown = 5f;
    public bool canFire = true;
    public bool aimedIn = false;
    

    void Start() {
    }

    void OnTriggerEnter2D(Collider2D other) {
        isInCollider = true;
        if (rotate) {
            //StopCoroutine(rotateCoroutine);
            rotateCoroutine = mainEnemyScript.RotateToPoint(other.transform);
        }
        print("ENTERED SNIPER TRIGGER");
   
        if (fireOnStay) {
            StartCoroutine(TrackStayTime());
        }

        mainEnemyScript.priority.Add(BossDrEnemy.AnimationStates.ZeroMove);
    }

    void OnTriggerStay2D(Collider2D other) {
        if (rotate) {
            rotateCoroutine = mainEnemyScript.RotateToPoint(other.transform);
            StartCoroutine(rotateCoroutine);
        }
        //print("stay");
        
        
        if (fireOnStay && canFire && aimedIn) {
            print("Fire");
            StartCoroutine(FireCooldown());
            StartCoroutine(mainEnemyScript.FireGun(other));
        } 
    }

    void OnTriggerExit2D(Collider2D other) {
        isInCollider = false;
        StopCoroutine(TrackStayTime());
        mainEnemyScript.priority.Remove(BossDrEnemy.AnimationStates.ZeroMove);
    }


    private IEnumerator TrackStayTime() {
        float currentTime = 0;
        while (isInCollider) {
            if (currentTime > timeTillFire) {
                aimedIn = true;
            } else {
                aimedIn = false;
            }
            currentTime += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator FireCooldown() {
        canFire = false;
        yield return new WaitForSeconds(fireCooldown);
        canFire = true;
        
        
    }
    
}
