using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiComponent : MonoBehaviour
{
    NavMeshAgent agent;
    GameObject player;
    //EnemyShoot enemy_shoot;
    [SerializeField] bool isRanged = true;
    [SerializeField] float keepoutDistance = 7f;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        player = GameObject.Find("Player");
        //enemy_shoot = GetComponentInChildren<EnemyShoot>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isRanged )
        {
            if((Vector3.Distance(this.transform.position, player.transform.position) > keepoutDistance /*|| !enemy_shoot.GetLineOfSight()*/))
            {
                agent.destination = player.transform.position;

            }
            else
            {
                agent.destination = this.transform.position;
            }
        }
        else
        {
            agent.destination = player.transform.position;
        }

    }
}
