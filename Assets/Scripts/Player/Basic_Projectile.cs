using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic_Projectile : MonoBehaviour
{
    [SerializeField] int damagePerHit = 25;
    private float moveSpeed = 0.4f;
    
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
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if touching something damageable other than the player, deal damage
        if ((collision.gameObject.TryGetComponent<DamageableComponent>(out DamageableComponent target)) && (!collision.CompareTag("Player")) )
        {
            target.TakeDamage(damagePerHit);
        }
        
        // if touching anything other than the player
        if (!collision.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }

    }
    
   
}
