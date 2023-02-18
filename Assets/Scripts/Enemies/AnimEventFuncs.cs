using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimEventFuncs : MonoBehaviour
{
    [SerializeField] private NavMeshAgent nmAgent;
    float lastSpeed;

    public void SetNewAgentSpeed(float newSpeed) {
        lastSpeed = nmAgent.speed;
        nmAgent.speed = newSpeed;
    }

    public void SetLastAgentSpeed() {
        nmAgent.speed = lastSpeed;
    }
}
