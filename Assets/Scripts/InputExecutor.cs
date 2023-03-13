using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;


public class InputExecutor : MonoBehaviour
{
    public List<KeyAction> keys;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        KeyAction[] inputs = new KeyAction[InputManager.inputBindings.Count];
        InputManager.inputBindings.Values.CopyTo(inputs, 0);
        keys = inputs.ToList();
    }

    // Update is called once per frame
    void Update()
    {
        keys.ForEach(key=>{
            //bool isPressed = key.continuous ? Input.GetKey(key.key) : Input.GetKeyDown(key.key);

            //bool isContextMatch = key.context == InputManager.primaryContext;

            if (key.contexts.Contains(InputManager.primaryContext) && (key.continuous ? Input.GetKey(key.key) : Input.GetKeyDown(key.key))){
                key.result();
            }

        });
    }
}
