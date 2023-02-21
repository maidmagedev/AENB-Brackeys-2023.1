using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


// 1. Make sure you have this package installed: https://github.com/h8man/NavMeshPlus.git
// 2. Create an empty GameObject called "NavMesh Manager" and give it the following components: NavMesh Collect Surfaces 2D and Navigation Surface
// 3. For each tile-map in your grid, add a Navigation Modifier component and set the override to "Walkable" or "Unwalkable"
// 4. Go back to the NavMesh Manager and set the X rotation to -90 if you haven't already.  Finally, press "bake" to generate the navmesh
// 5. Nav Agents should follow this basic structure: Agent>>EnemyType>>DmgLight
// 6. Agent: Requires a navmesh agent component and the "AI Component" Script. X rotation: -90
// 7. EnemyType: Sprite Renderer and TestEnemy.cs or similar. X rotation: 90

public class TongueEnemy : MonoBehaviour, IKillable
{
    [Header("Stats")]
    [SerializeField] int damagePerHit = 10; // Note that the damage per hit doesn't reflect the tongue attack which happens on a separate collider.
    
    [SerializeField] private int maxHealth = 400;
    [SerializeField] private float attackCooldown = 1f;


    [Header("Components")]
    [SerializeField] private NavMeshAgent navMeshobj;
    [SerializeField] private GameObject damageLight;
    [SerializeField] private float spriteScale = 1f;

    DamageableComponent damageableComponent;
    BoxCollider2D boxCollider;
    


    private bool mayAttack = true;
    private bool currentlyAttacking = false;


    // Start is called before the first frame update
    void Start()
    {
        damageableComponent = this.gameObject.AddComponent<DamageableComponent>();
        damageableComponent.SetMaxHealth(maxHealth);

    }

    // Update is called once per frame
    void Update()
    {
        //print(navMeshobj.remainingDistance);
        //print(mayAttack);
        FlipSprite();
        if (navMeshobj.remainingDistance <= 3  && navMeshobj.remainingDistance > 0 && mayAttack)
        {
            //Debug.Log("IN ATTACK RANGE");
            //print("attacking");
            GetComponent<TongueAnimations>().SetAttackAnimationState(true);
            mayAttack = false;
            StartCoroutine(AttackCooldown());
            
        }
    }

    private void FlipSprite()
    {
        if (navMeshobj.velocity.x < 0)
        {
            transform.localScale = new Vector2(spriteScale, gameObject.transform.localScale.y);
        }
        else if (navMeshobj.velocity.x > 0)
        {
            transform.localScale = new Vector2(spriteScale*-1, gameObject.transform.localScale.y);
        }
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

    public void Die() {
        //FindObjectOfType<UIScore>().score += 10;
        Destroy(this.transform.parent.gameObject);  
    }
    public void NotifyDamage()
    {
        StartCoroutine(DamageLightToggle());
    }

    IEnumerator DamageLightToggle()
    {
        damageLight.SetActive(true);
        yield return new WaitForSeconds(.5f);
        damageLight.SetActive(false);
    }

    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(2.75f);
        GetComponent<TongueAnimations>().SetAttackAnimationState(false);
        yield return new WaitForSeconds(attackCooldown);
        mayAttack = true;
        //print(mayAttack);
    }

}
