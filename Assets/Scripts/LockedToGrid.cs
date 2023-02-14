using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedToGrid : MonoBehaviour
{
    public Vector3 position {
        get { return transform.position; }
        set { transform.position = new Vector3((float)Math.Round(value.x, 0), (float)Math.Round(value.y, 0), 0);}
    }
}
