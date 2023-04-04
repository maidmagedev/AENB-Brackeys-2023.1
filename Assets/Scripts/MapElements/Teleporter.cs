using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : TriggerBase
{
    [Header("Settings")]
    //[SerializeField] ModeOfActivation activationMode;
    [SerializeField] Transform destination;
    [SerializeField] GameObject client; // user to be teleported

    //[Header("OPTIONAL: Target Selectivity Options")]
    //[SerializeField] bool onlyTargetColWithTag = true; // Only target colliders with the tagToTarget tag when enabled, ignoring everything else.
    //[SerializeField] string tagToTarget = "Player"; // case sensitive probably

    public override void DoAction()
    {
        Debug.Log("New DoAction()");
        if (client == null)
        {
            client = GameObject.FindWithTag("Player");
        }
        client.transform.position = destination.position;
    }
}
