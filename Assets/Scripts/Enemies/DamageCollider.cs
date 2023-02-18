using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{

    [SerializeField] BoxCollider2D boxCol;
    [SerializeField] int damagePerHit = 10;


    // Start is called before the first frame update
    void Start()
    {
        if (boxCol == null) {
            GetComponent<BoxCollider2D>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<DamageableComponent>(out DamageableComponent target))
        {
            target.TakeDamage(damagePerHit);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<DamageableComponent>(out DamageableComponent target))
        {
            print("dealing damage");
            target.TakeDamage(damagePerHit);
        }
    }
}
