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
    float timeTillFire = 0.5f;
    float fireCooldown = 2f;
    public bool canFire = true;
    public bool aimedIn = false;
    
    public GameObject currentTarget;

    void Start() {
    }

    void Update() {
        if (currentTarget == null) {
            mainEnemyScript.priority.Remove(BossDrEnemy.AnimationStates.ZeroMove);
        } else {
            mainEnemyScript.priority.Add(BossDrEnemy.AnimationStates.ZeroMove);

        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("DamageHitbox")) {
            return;
        }
        isInCollider = true;
        
        //print("ENTERED SNIPER TRIGGER");
        if (textScroller != null) {
            mainEnemyScript.randomText(textScroller);
        }

        if (currentTarget == null) {
            currentTarget = other.gameObject;
        }
        
        if (fireOnStay) {
            StartCoroutine(TrackStayTime());
        }
    }

    void OnTriggerStay2D(Collider2D other) {
        if (other.CompareTag("DamageHitbox")) {
            return;
        }
        if (currentTarget == null) {
            currentTarget = other.gameObject;
        }

        if (rotate) {
            rotateCoroutine = mainEnemyScript.RotateToPoint(currentTarget.transform);
            StopCoroutine(rotateCoroutine);
            StartCoroutine(rotateCoroutine);
        }
        //print("stay");
        
        if (currentTarget.transform.CompareTag("Player")) {
            if (fireOnStay && canFire && aimedIn) {
                // attack the player
                print("Fire at Player");
                StartCoroutine(FireCooldown(fireCooldown));
                StartCoroutine(mainEnemyScript.FireGun(currentTarget.GetComponent<BoxCollider2D>()));
            } 
        } else if (fireOnStay && canFire) {
            print("Fire else");
            StartCoroutine(FireCooldown(2f));
            StartCoroutine(mainEnemyScript.FireGun(currentTarget.GetComponent<BoxCollider2D>()));
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("DamageHitbox")) {
            return;
        }
        isInCollider = false;
        //StopCoroutine(TrackStayTime());
        
    }


    private IEnumerator TrackStayTime() {
        float currentTime = 0;
        while (isInCollider && currentTarget != null) {
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
