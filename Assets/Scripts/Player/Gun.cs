using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    // This script will fire a basic projectile
    // It is meant to be used in conjunction with Basic_Projectile.cs or similar

    [Header("Stats")]
    [SerializeField] float fireCooldown = 0;
    [SerializeField] float fireRate = 1f;
    [SerializeField] public int burstCount = 3;
    [SerializeField] public float burstGap = 0.08f;

    public int clip = 2;
    public bool reloading = false;
    private bool _mayshoot = true;

    public bool mayshoot{get =>_mayshoot; set =>_mayshoot = value;}

    [Header("Sound")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip fireSound;
    [SerializeField] private float volume = 10f;

    [Header("Components, References")]
    [SerializeField] public GameObject projectile_prefab; // could make this a Resources.Load() call
    [SerializeField] public Transform[] bulletOrigins;

    [Header("Animations")]
    [SerializeField] Animator animator;
    public enum AnimationStates { Static, Fire, Reload }
    public int firingLayer = 0;
    private void updateAnimationState(AnimationStates newvalue)
    {
        animator.CrossFade(StateMapping[newvalue], 0, 0);  // crossfade implementation
    }

    //state booleans. This assumes that each transition is simply "other" == true. All new states should be registered here.
    private DictWithKeySet<AnimationStates, string> StateMapping = new DictWithKeySet<AnimationStates, string>
    {
        [AnimationStates.Static] = "Static",
        [AnimationStates.Fire] = "Fire",
        [AnimationStates.Reload] = "Reload"
    };

    //actions that should not interrupt each other should have the same priority, overrides higher, ignores lower.
    //all new animations must be registered here.
    static private DictWithKeySet<AnimationStates, int> priorityMapping = new DictWithKeySet<AnimationStates, int>
    {
        [AnimationStates.Static] = 0,
        [AnimationStates.Fire] = 1,
        [AnimationStates.Reload] = 2
    };

    public List<AnimationStates> priority = new List<AnimationStates>(new AnimationStates[] { AnimationStates.Static });
    private Comparer<AnimationStates> sorter;

    
    
    // Start is called before the first frame update
    void Start()
    {
        // For the Animations
        sorter = Comparer<AnimationStates>.Create((a, b) => priorityMapping[b] - priorityMapping[a]);

    }
    
    // Update is called once per frame
    void Update()
    {
        //GetInput(); // called externally.

        // Animation Sort
        priority.Sort(sorter);
        updateAnimationState(priority[0]);
        
    }
    
    public abstract void GetInput();
    public abstract IEnumerator FireGun();

    public abstract IEnumerator AnimatorFire();

    public IEnumerator FireRepeatTimer()
    {
        yield return new WaitForSeconds(fireCooldown);
        mayshoot = true;
    }
    
    
    // Hold For Fire
    /*****************************************************************/
    private void FireGunHeld()
    {
        if (Input.GetMouseButton(0))
        {
            spawnBullet();
        }
    }

    private void spawnBullet()
    {
        if (mayshoot)
        {
            //audioSource.PlayOneShot(fireSound, volume);
            mayshoot = false;
            Instantiate(projectile_prefab, transform.position, transform.rotation);
            StartCoroutine(FireRateTimer());

        }
    }
    IEnumerator FireRateTimer()
    {
        yield return new WaitForSeconds(fireRate);
        mayshoot = true;
    }
}
