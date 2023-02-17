using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    
    // This script will fire a basic projectile
    // It is meant to be used in conjunction with Basic_Projectile.cs or similar

    [Header("Stats")]
    [SerializeField] float fireCooldown = 0;
    [SerializeField] float fireRate = 1f;
    [SerializeField] int pelletCount = 5;
    public int clip = 2;
    private bool reloading = false;
    private bool mayshoot = true;

    [Header("Sound")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip fireSound;
    [SerializeField] private float volume = 10f;

    [Header("Components, References")]
    [SerializeField] private GameObject projectile_prefab; // could make this a Resources.Load() call
    [SerializeField] Transform[] bulletOrigins;
    //public GameObject shotgunObj;

    [Header("Animations")]
    [SerializeField] Animator shotgunAnim;
    private enum AnimationStates { Static, Fire, Reload }
    private int firingLayer = 0;
    private void updateAnimationState(AnimationStates newvalue)
    {
        shotgunAnim.CrossFade(StateMapping[newvalue], 0, 0);  // crossfade implementation
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

    private List<AnimationStates> priority = new List<AnimationStates>(new AnimationStates[] { AnimationStates.Static });
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

    public void GetInput() {
        if (Input.GetMouseButtonDown(0) && mayshoot) {
            if (clip > 0) {
                StartCoroutine(FireGun());       // press and release for firing
                //FireGunHeld(); // hold for firing
                StartCoroutine(AnimatorFire());
                clip--;
            }
        }
    } 
    
    

    // Press and Release for Fire
    /*********************************************************************/
    private IEnumerator FireGun()
    {
        mayshoot = false;

        //audioSource.PlayOneShot(fireSound, volume);
        
        yield return new WaitForSeconds(0.04f);
        foreach (Transform t in bulletOrigins) {
            Instantiate(projectile_prefab, t.position, t.rotation);
        }
        
        
        yield return new WaitForSeconds(0.25f);
        StartCoroutine(FireRepeatTimer());
        
    }

    private IEnumerator AnimatorFire() {
        firingLayer++;
        priority.Remove(AnimationStates.Fire);
        yield return new WaitForEndOfFrame();
        priority.Add(AnimationStates.Fire);
        yield return new WaitForSeconds(0.75f);
        firingLayer--;
        if (firingLayer == 0) {
            priority.Remove(AnimationStates.Fire);
        }
         if (clip == 0 && !reloading) {
            reloading = true;
            StartCoroutine(Reloading());
        }

    }

    private IEnumerator Reloading() {
        priority.Add(AnimationStates.Reload);
        yield return new WaitForSeconds(1.25f);
        priority.Remove(AnimationStates.Reload);
        reloading = false;
        mayshoot = true;
        clip = 2;
    }

    // this is a setter that is useful to access when pausing the game
    public void Set_mayFire(bool FireBool)
    {
        mayshoot = FireBool;
    }

    IEnumerator FireRepeatTimer()
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
