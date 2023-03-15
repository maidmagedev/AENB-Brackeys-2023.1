using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic_Projectile : MonoBehaviour
{
    [Header("Customizable Stats")]
    [SerializeField] int damagePerHit = 25; // Damage to enemies
    [SerializeField] int damageToPlayer = 0; // Deal a different # of damage to the player, if damagePlayer is true. If you want damage to be the same, just set to the same val.
    [SerializeField] float lifeTime = 3f;
    [SerializeField] float moveSpeed = 0.4f;
    [SerializeField] bool destroyOnHit = true;
    [SerializeField] bool damagePlayer;
    
    private void Start() {
        StartCoroutine(lifetimer());
    }

    private void FixedUpdate()
    {
        move();
    }
    
    private void move()
    {
        transform.Translate(Vector2.right * moveSpeed);
    }
    
    IEnumerator lifetimer()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if touching something damageable other than the player, deal damage
        if ((collision.gameObject.TryGetComponent<DamageableComponent>(out DamageableComponent target))) {
            if (damagePlayer && collision.CompareTag("Player")) {
                // deal damage to the player, if this is player.
                target.TakeDamage(damageToPlayer);
            } else if (!collision.CompareTag("Player") && (!collision.CompareTag("Machine"))) {
                // deal damage to non-player and non-machine, so standard enemies.
                target.TakeDamage(damagePerHit);
            }
        } 
        
        // if touching anything other than the player
        if (!collision.CompareTag("Player") && destroyOnHit)
        {
            Destroy(this.gameObject);
        }

    }
    
   
}
