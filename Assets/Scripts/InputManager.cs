using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public static class InputManager
{

    private static List<InputContext> notPaused = new(){InputContext.INVENTORY, InputContext.NONE};
    private static List<InputContext> any = new(){InputContext.INVENTORY, InputContext.PAUSE, InputContext.NONE};

    public static Dictionary<String, KeyAction> inputBindings = new(){
        {"Inventory", new KeyAction(notPaused, KeyCode.I, null, false)},
        {"Primary Item Use", new KeyAction(notPaused, KeyCode.Mouse0, null, true)},
        {"Pause", new KeyAction(any, KeyCode.Escape, null, false)},
        {"Up", new KeyAction(notPaused, KeyCode.W, ()=>{GameObject.FindGameObjectWithTag("Player").GetComponent<TopDownMovementComponent>().movementDirection += Vector2.up;}, true)},
        {"Down", new KeyAction(notPaused, KeyCode.S, ()=>{GameObject.FindGameObjectWithTag("Player").GetComponent<TopDownMovementComponent>().movementDirection += Vector2.down;}, true)},
        {"Left", new KeyAction(notPaused, KeyCode.A, ()=>{GameObject.FindGameObjectWithTag("Player").GetComponent<TopDownMovementComponent>().movementDirection += Vector2.left;}, true)},
        {"Right", new KeyAction(notPaused, KeyCode.D, ()=>{GameObject.FindGameObjectWithTag("Player").GetComponent<TopDownMovementComponent>().movementDirection += Vector2.right;}, true)},
        {"Flashlight", new KeyAction(notPaused, KeyCode.F, null, false)},
        {"Drop Item", new KeyAction(notPaused, KeyCode.Q, null, false)},
    };

    public static InputContext primaryContext;

    public enum InputContext{
        INVENTORY,
        PAUSE,
        NONE
    }   
}

public class KeyAction
{
    public bool continuous;
    public KeyCode key;
    public Action result;

    public List<InputManager.InputContext> contexts;

    public KeyAction(List<InputManager.InputContext> contexts, KeyCode key, Action result, bool continuous){
        this.contexts = contexts;
        this.key = key;
        this.result = result;
        this.continuous = continuous;
    }
}