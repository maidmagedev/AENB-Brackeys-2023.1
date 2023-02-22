using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// It might be important to note that PlayerLight is not directly attached to the player.
// This script mainly handles animations.
public class PlayerLight : MonoBehaviour
{
    [Header("Flashlight Power")]
    [SerializeField] float maxEnergy = 100.0f;
    [SerializeField] float energy = 100.0f;
    [SerializeField] float decayPerSecond = 0.1f;
    [SerializeField] float regenPerSecond = 1f;
    public bool flashLightOn = false;
    private bool doingManagement = false;

    private enum AnimationStates { Inactive, StartUp, Oscillating, ShutDown, FlickeringDead }
    private void updateAnimationState(AnimationStates newvalue)
    {
        myAnim.CrossFade(StateMapping[newvalue], 0, 0);  // crossfade implementation
    }

    //state booleans. This assumes that each transition is simply "other" == true. All new states should be registered here.
    private DictWithKeySet<AnimationStates, string> StateMapping = new DictWithKeySet<AnimationStates, string>
    {
        [AnimationStates.Inactive] = "Inactive",
        [AnimationStates.StartUp] = "StartUp",
        [AnimationStates.Oscillating] = "Oscillating",
        [AnimationStates.ShutDown] = "ShutDown",
        [AnimationStates.FlickeringDead] = "FlickeringDead"
    };

    //actions that should not interrupt each other should have the same priority, overrides higher, ignores lower.
    //all new animations must be registered here.
    static private DictWithKeySet<AnimationStates, int> priorityMapping = new DictWithKeySet<AnimationStates, int>
    {
        [AnimationStates.Inactive] = 0,
        [AnimationStates.Oscillating] = 1,
        [AnimationStates.StartUp] = 2,
        [AnimationStates.ShutDown] = 3,
        [AnimationStates.FlickeringDead] = 4
    };

    Rigidbody2D rb;

    private List<AnimationStates> priority = new List<AnimationStates>(new AnimationStates[] { AnimationStates.Inactive });

    private Comparer<AnimationStates> sorter;

    Animator myAnim;

    private void Start()
    {
        myAnim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sorter = Comparer<AnimationStates>.Create((a, b) => priorityMapping[b] - priorityMapping[a]);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) {
            if (!flashLightOn) {
                StartCoroutine(TurnOnLight());
            } else {
                StartCoroutine(TurnOffLightManual());
            }
        }
        if (!doingManagement) {
            StartCoroutine(ManageEnergy());
        }
        priority.Sort(sorter);

        updateAnimationState(priority[0]);
    }

    IEnumerator TurnOnLight() {
        flashLightOn = true;
        priority.Add(AnimationStates.StartUp);
        yield return new WaitForSeconds(0.167f);
        priority.Remove(AnimationStates.StartUp);
        priority.Add(AnimationStates.Oscillating);
    }

    IEnumerator TurnOffLightManual() {
        flashLightOn = false;
        priority.Remove(AnimationStates.Oscillating);
        priority.Add(AnimationStates.ShutDown);
        yield return new WaitForSeconds(0.167f);
        priority.Remove(AnimationStates.ShutDown);
    }

    IEnumerator TurnOffLightForced() {
        flashLightOn = false;
        priority.Remove(AnimationStates.Oscillating);
        priority.Add(AnimationStates.FlickeringDead);
        yield return new WaitForSeconds(2.567f);
        priority.Remove(AnimationStates.FlickeringDead);
    }

    IEnumerator ManageEnergy() {
        doingManagement = true;
        if (flashLightOn) {
            if (energy <= 0) {
                StartCoroutine(TurnOffLightForced());
            } else {
                energy -= decayPerSecond;
            }
        } else {
            if (energy < maxEnergy) {
                energy += regenPerSecond;
            }
        }
        yield return new WaitForSeconds(1f);
        doingManagement = false;
    }

}
