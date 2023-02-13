using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiComponent : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] GameObject target;
    //EnemyShoot enemy_shoot;
    [SerializeField] bool isRanged = true;
    [SerializeField] float keepoutDistance = 7f; // this only applies for ranged enemies

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        //enemy_shoot = GetComponentInChildren<EnemyShoot>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isRanged)
        {
            if((Vector3.Distance(this.transform.position, target.transform.position) > keepoutDistance /*|| !enemy_shoot.GetLineOfSight()*/))
            {
                agent.destination = target.transform.position;

            }
            else
            {
                agent.destination = this.transform.position;
            }
        }
        else
        {
            agent.destination = target.transform.position;
        }

    }
}
