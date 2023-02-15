using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Based on the PlayerAnimations.cs script from the Unity Script Components
public class PlayerAnimations : MonoBehaviour
{
    private enum AnimationStates { Idle, Walk, Death }
    private void updateAnimationState(AnimationStates newvalue)
    {
        myAnim.CrossFade(StateMapping[newvalue], 0, 0);  // crossfade implementation
    }

    //state booleans. This assumes that each transition is simply "other" == true. All new states should be registered here.
    private DictWithKeySet<AnimationStates, string> StateMapping = new DictWithKeySet<AnimationStates, string>
    {
        [AnimationStates.Idle] = "Idle",
        [AnimationStates.Walk] = "Walk",
        [AnimationStates.Death] = "Death"
    };

    //actions that should not interrupt each other should have the same priority, overrides higher, ignores lower.
    //all new animations must be registered here.
    static private DictWithKeySet<AnimationStates, int> priorityMapping = new DictWithKeySet<AnimationStates, int>
    {
        [AnimationStates.Idle] = 0,
        [AnimationStates.Walk] = 1,
        [AnimationStates.Death] = 2
    };

    Rigidbody2D rb;

    private List<AnimationStates> priority = new List<AnimationStates>(new AnimationStates[] { AnimationStates.Idle });

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
        AnimationStates newState;

        if(Mathf.Abs(rb.velocity.x) > 0 || Mathf.Abs(rb.velocity.y) > 0)
        {
            if (!priority.Contains(AnimationStates.Walk))
            {
                priority.Add(AnimationStates.Walk);
            }
        } else
        {
            priority.Remove(AnimationStates.Walk);
        }

        priority.Sort(sorter);

        updateAnimationState(priority[0]);
    }

}