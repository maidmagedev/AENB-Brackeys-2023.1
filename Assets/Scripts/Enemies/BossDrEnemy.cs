using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class BossDrEnemy : MonoBehaviour, IKillable
{
    
    [SerializeField] int damagePerHit = 2;

    [SerializeField] private int maxHealth = 500;
    [SerializeField] private float spriteScale = 1f;

    

    private bool mayAttack = true;
    private bool currentlyAttacking = false;
    public CurrentState currState = CurrentState.Tracking;
    public List<GameObject> targetList;

    [Header("Bullet")]
    [SerializeField] GameObject projectile_prefab;
    [SerializeField] Transform bulletOrigin;

    [Header("References")]
    [SerializeField] GameObject gunObj;
    [SerializeField] private NavMeshAgent navMeshobj;
    [SerializeField] private GameObject damageLight;
    [SerializeField] Animator bodyAnim;
    [SerializeField] Animator gunAnim;
    [SerializeField] Animator lightAnim;
    [SerializeField] SpriteRenderer bossSprite;
    DamageableComponent damageableComponent;
    BoxCollider2D boxCollider;

    [Header("Sound")]
    [SerializeField] public AudioSource audioSource;
    [SerializeField] public AudioClip fireSound;
    [SerializeField] public AudioClip boltSound;
    [SerializeField] public List<AudioClip> vocalCries;
    [SerializeField] public AudioClip deathSound;
    [SerializeField] public float volume = 10f;
    [SerializeField] float vocalCryCD = 2f;
    private bool vocalCryOnCooldown = false;
    
    public enum AnimationStates { Static, Fire, Reload, Equip, Bolt, ZeroMove, Scanning }

    public enum CurrentState { Tracking, Scanning }
    private void updateAnimationState(AnimationStates newvalue)
    {
        gunAnim.CrossFade(StateMapping[newvalue], 0, 0);  // crossfade implementation
    }

    //state booleans. This assumes that each transition is simply "other" == true. All new states should be registered here.
    private DictWithKeySet<AnimationStates, string> StateMapping = new DictWithKeySet<AnimationStates, string>
    {
        [AnimationStates.Static] = "Static",
        [AnimationStates.Fire] = "Fire",
        [AnimationStates.Reload] = "Reload",
        [AnimationStates.Equip] = "Equip",
        [AnimationStates.Bolt] = "Bolt",
        [AnimationStates.ZeroMove] = "ZeroMove",
        [AnimationStates.Scanning] = "Scanning"
    };

    //actions that should not interrupt each other should have the same priority, overrides higher, ignores lower.
    //all new animations must be registered here.
    static private DictWithKeySet<AnimationStates, int> priorityMapping = new DictWithKeySet<AnimationStates, int>
    {
        [AnimationStates.Static] = 0,
        [AnimationStates.Fire] = 20,
        [AnimationStates.Reload] = 30,
        [AnimationStates.Equip] = 40,
        [AnimationStates.Bolt] = 50,
        [AnimationStates.ZeroMove] = 5,
        [AnimationStates.Scanning] = 1
    };

    public List<AnimationStates> priority = new List<AnimationStates>(new AnimationStates[] { AnimationStates.Static });
    private Comparer<AnimationStates> sorter;



    // Start is called before the first frame update
    void Start()
    {
        sorter = Comparer<AnimationStates>.Create((a, b) => priorityMapping[b] - priorityMapping[a]);

        //bodyAnim.SetTrigger("Walk");
        damageableComponent = this.gameObject.AddComponent<DamageableComponent>();
        damageableComponent.SetMaxHealth(maxHealth);
        lightAnim.SetTrigger("Oscillating");

    }

    // Update is called once per frame
    void Update()
    {
        //print(navMeshobj.remainingDistance);
        //print(mayAttack);
        //FlipSprite();
        if (targetList.Count == 0) {
        }
        
        FlipGun();
        priority.Sort(sorter);
        updateAnimationState(priority[0]);
    }

    private void FlipSprite()
    {
        if (navMeshobj.velocity.x < 0)
        {
            //transform.localScale = new Vector2(spriteScale*-1, gameObject.transform.localScale.y);
            bossSprite.flipX = true;
        }
        else if (navMeshobj.velocity.x > 0)
        {
            bossSprite.flipX = false;
            //transform.localScale = new Vector2(spriteScale, gameObject.transform.localScale.y);
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
            //print("dealing damage");
            target.TakeDamage(damagePerHit);
        }
    }

    public void Die() {
        audioSource.PlayOneShot(deathSound, volume * 0.1f);
        //FindObjectOfType<UIScore>().score += 10;
        StartCoroutine(DieAnim());
    }

    private IEnumerator DieAnim() {
        //bodyAnim.SetTrigger("Die");
        yield return new WaitForSeconds(0.5f);
        Destroy(this.transform.parent.gameObject);  
    }

    public void NotifyDamage()
    {
        StartCoroutine(DamageLightToggle());
        randomVocalCry(true);
    }

    IEnumerator DamageLightToggle()
    {
        damageLight.SetActive(true);
        yield return new WaitForSeconds(.5f);
        damageLight.SetActive(false);
    }

    // used to send the gun to a position
    // does this with lerp
    // this gets called inside of a ontriggerstay, this might be costly to performance, unsure.
    public IEnumerator RotateToPoint(Transform point) {
        currState = CurrentState.Tracking;
        /*
        // set instantly
        currState = CurrentState.Tracking;
        Vector2 direction = point.position - transform.position;
        float angle = Vector2.SignedAngle(Vector2.right, direction);
        gunObj.transform.eulerAngles = new Vector3(gunObj.transform.eulerAngles.x, gunObj.transform.eulerAngles.y, angle);
        
        yield return null;
        */
        if (gunObj.transform.eulerAngles.z > 360) {
            gunObj.transform.eulerAngles = new Vector3(gunObj.transform.eulerAngles.x, gunObj.transform.eulerAngles.y, gunObj.transform.eulerAngles.z - 360);
        }
        float totalTime = 0.2f;
        float current = 0f;
        float startAngle = gunObj.transform.eulerAngles.z;
        float endAngle;

        Vector2 direction = point.position - transform.position;
        endAngle = Vector2.SignedAngle(Vector2.right, direction); // default end angle
        

        if (Mathf.Abs(endAngle + 360) - startAngle < Mathf.Abs(endAngle - startAngle)) {
            endAngle += 360;
        } else {
        }
        //print("start: " + startAngle + " end: " + endAngle);
        while(current <= totalTime) {
            // sets to position
           
            float angle = Mathf.Lerp(startAngle, endAngle, current / totalTime);
            //print("angle:" + angle);
            gunObj.transform.eulerAngles = new Vector3(gunObj.transform.eulerAngles.x, gunObj.transform.eulerAngles.y, angle);
            current += 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
    }

    private void FlipGun() {
        if (gunObj.transform.eulerAngles.z > 90 && gunObj.transform.eulerAngles.z < 270 || gunObj.transform.eulerAngles.z < -90f &&  gunObj.transform.eulerAngles.z > -270f) {
            //print("LEFT");
            gunObj.transform.localScale = new Vector3(-1f, -1f, 1f);
            bossSprite.flipX = true;
        } else {
            //print("RIGHT");
            gunObj.transform.localScale = new Vector3(-1f, 1f, 1f);
            bossSprite.flipX = false;
        }
    }

    public IEnumerator FireGun(Collider2D other) {
        print("recieved fire call");
        if ((other.gameObject.TryGetComponent<DamageableComponent>(out DamageableComponent target))) 
        {
            
            priority.Add(AnimationStates.Fire);
            yield return new WaitForSeconds(0.07f);
            
            //target.TakeDamage(50);
            Instantiate(projectile_prefab, bulletOrigin.position, bulletOrigin.rotation);
            audioSource.PlayOneShot(fireSound, volume);
            yield return new WaitForSeconds(0.747f);
            priority.Remove(AnimationStates.Fire);
            
            priority.Add(AnimationStates.Bolt);
            yield return new WaitForSeconds(0.567f);
            priority.Remove(AnimationStates.Bolt);
            audioSource.PlayOneShot(boltSound, volume);
        }
    }

    public void randomText(TextScroller textScroller) {
        if (vocalCryOnCooldown) {
            return;
        } 
        string text = "";
        string[] options = {"DIE!", "LEAVE.", "SCUM!", "PERISH!"};

        int pick = Random.Range(0, options.Length);
        text = options[pick];
        randomVocalCry(false);
        textScroller.DisplayText(text);
        StartCoroutine(TextAndScreamCD());
    }

    public void randomVocalCry(bool overrideCD) {
        if (!overrideCD && vocalCryOnCooldown) {
            return;
        }
        int pick = Random.Range(0, vocalCries.Count);
        audioSource.PlayOneShot(vocalCries[pick], volume * 0.08f);
    }

    IEnumerator TextAndScreamCD() {
        vocalCryOnCooldown = true;
        yield return new WaitForSeconds(vocalCryCD);
        vocalCryOnCooldown = false;
    }

}
