using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDrEnemyTrigger : MonoBehaviour
{
    public bool fireOnStay = false;
    public bool rotate;
    [SerializeField] BossDrEnemy mainEnemyScript;
    [SerializeField] TextScroller textScroller;
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
            //rotateCoroutine = mainEnemyScript.RotateToPoint(other.transform);
            //StopCoroutine(rotateCoroutine);
            //StartCoroutine(rotateCoroutine);        
        }
        print("ENTERED SNIPER TRIGGER");
        if (textScroller != null) {
            mainEnemyScript.randomText(textScroller);
        }
   
        if (fireOnStay) {
            StartCoroutine(TrackStayTime());
        }

        mainEnemyScript.priority.Add(BossDrEnemy.AnimationStates.ZeroMove);
    }

    void OnTriggerStay2D(Collider2D other) {
        if (rotate) {
            rotateCoroutine = mainEnemyScript.RotateToPoint(other.transform);
            StopCoroutine(rotateCoroutine);
            StartCoroutine(rotateCoroutine);
        }
        //print("stay");
        
        if (other.transform.CompareTag("Player")) {
            if (fireOnStay && canFire && aimedIn) {
                print("Fire");
                StartCoroutine(FireCooldown(fireCooldown));
                StartCoroutine(mainEnemyScript.FireGun(other));
            } 
        } else if (canFire) {
            StartCoroutine(FireCooldown(2f));
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

    private IEnumerator FireCooldown(float time) {
        canFire = false;
        yield return new WaitForSeconds(time);
        canFire = true;
        
        
    }

    
    
}
