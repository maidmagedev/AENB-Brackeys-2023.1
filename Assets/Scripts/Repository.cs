using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repository : MonoBehaviour
{
    private GameObject _Player;

    public GameObject Player{get {return _Player;} set {_Player = value;} }
}
