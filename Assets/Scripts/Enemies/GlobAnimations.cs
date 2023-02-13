using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;


// This script controls which animations are active for the glob enemy at any given time

public class GlobAnimations : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navMeshobj;

    // This section of code handles the logic of setting previous animation states to false
    // when changing animations.  Any new animations need to be added to the various enums and lists
    // for more specifics, see individual comments--otherwise this section never needs to be changed

    //Currently playing animation things. Register all new in enum!
    private enum AnimationStates { Idle, Attack }


    private void updateAnimationState(AnimationStates newvalue)
    {
        myAnim.CrossFade(StateMapping[newvalue], 0, 0);  // crossfade implementation
    }


    //state booleans. This assumes that each transition is simply "other" == true. All new states should be registered here.
    private DictWithKeySet<AnimationStates, string> StateMapping = new DictWithKeySet<AnimationStates, string>
    {
        [AnimationStates.Idle] = "Glob Idle Anim",
        [AnimationStates.Attack] = "Glob Attack Anim"
    };



    //actions that should not interrupt each other should have the same priority, overrides higher, ignores lower.
    //all new animations must be registered here.
    static private DictWithKeySet<AnimationStates, int> priorityMapping = new DictWithKeySet<AnimationStates, int>
    {
        [AnimationStates.Idle] = 0,
        [AnimationStates.Attack] = 1
    };

    // initial animation state
    private List<AnimationStates> priority = new List<AnimationStates>(new AnimationStates[] { AnimationStates.Idle });


    private Comparer<AnimationStates> sorter;


    Animator myAnim;
    
    // Start is called before the first frame updates
    void Start()
    {

        myAnim = GetComponent<Animator>();
        sorter = Comparer<AnimationStates>.Create((a, b) => priorityMapping[b] - priorityMapping[a]);

    }


    // Update is called once per frame   
    void Update()
    {
        
        // this is where the animation states get set
        AnimationStates newState;

       /* if (navMeshobj.velocity == Vector3.zero)
        {
            if (!priority.Contains(AnimationStates.Idle)) // idling
            {
                priority.Add(AnimationStates.Idle);
            }
        }
        else
        {
            priority.Remove(AnimationStates.Idle);
        }*/



        // this is necessary stuff I don't touch
        priority.Sort(sorter);

        updateAnimationState(priority[0]);

    }

    // Sets the animation state to a generic side attack
    public void SetAttackAnimationState(bool isAttacking)
    {
        if (isAttacking)
        {
            priority.Add(AnimationStates.Attack);
        }
        else
        {
            priority.Remove(AnimationStates.Attack);
        }
    }


}

public class DictWithKeySet<TKey, TValue> : Dictionary<TKey, TValue>
{

    public TKey[] GetKeys()
    {
        TKey[] ret = new TKey[this.Count];

        this.Keys.CopyTo(ret, 0);

        return ret;
    }
}

