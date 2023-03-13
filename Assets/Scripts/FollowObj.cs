using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObj : MonoBehaviour
{
    [SerializeField] GameObject objectToFollow;
    public bool updatePosition = true;
    public bool updateAngle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        if (updatePosition) {
            this.transform.position = objectToFollow.transform.position;
        }
        if (updateAngle) {
            this.transform.eulerAngles = objectToFollow.transform.eulerAngles;
        }
    }
}
