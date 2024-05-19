using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGameObjectEnable : TriggerBase
{

    
    [SerializeField] List<GameObject> clients;
    [SerializeField] Behavior behavior;
    public enum Behavior {
        SetActive,
        SetInactive,
        Toggle
    }

    public override void DoAction()
    {
        if (clients.Count < 1)
        {
            return;
        }
        foreach (GameObject client in clients) {
            switch(behavior) {
                case Behavior.SetActive:
                    client.SetActive(true);
                    break;
                case Behavior.SetInactive:
                    client.SetActive(false);
                    break;
                case Behavior.Toggle:
                    client.SetActive(!client.activeSelf);
                    break;
            }
        }


    }
}
