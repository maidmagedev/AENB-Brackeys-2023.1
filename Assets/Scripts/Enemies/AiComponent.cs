using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiComponent : MonoBehaviour
{
    NavMeshAgent agent;
    GameObject target;
    //EnemyShoot enemy_shoot;
    [SerializeField] bool isRanged = true;
    [SerializeField] float keepoutDistance = 7f; // this only applies for ranged enemies
    private List<GameObject> possible_targets = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        //enemy_shoot = GetComponentInChildren<EnemyShoot>();

        // target choosing logic
        GameObject[] machines = GameObject.FindGameObjectsWithTag("Machine");  // finds all machines that exist when the enemy is spawned
        target = GameObject.FindWithTag("Player"); 
        possible_targets.AddRange(machines);
        possible_targets.Add(target);
    }

    // Update is called once per frame
    void Update()
    {
        chooseTarget(); // could limit this to once every few seconds... more testing is required
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

    private void chooseTarget()
    {
        float minDist = Vector3.Distance(possible_targets[0].transform.position, transform.position);
        GameObject newTarget = possible_targets[0];
        foreach (GameObject g in possible_targets)
        {
            if (g != null)
            {
                float distance = Vector3.Distance(g.transform.position, transform.position);
                if (distance < minDist)
                {
                    minDist = distance;
                    newTarget = g;
                }
            }
        }
        target = newTarget;
    }
}
