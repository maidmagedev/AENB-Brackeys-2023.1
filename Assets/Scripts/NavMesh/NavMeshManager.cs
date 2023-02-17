using System;
using System.Collections;
using System.Collections.Generic;
using NavMeshPlus.Components;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshManager : MonoBehaviour
{
    [SerializeField] private NavMeshSurface[] _surfaces;
    
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < _surfaces.Length; i++)
        {
            _surfaces[i].BuildNavMesh();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
