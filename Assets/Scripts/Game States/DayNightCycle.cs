using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

public class DayNightCycle : MonoBehaviour
{
    [SerializeField] private Light2D globalLight;
    [SerializeField] private float nightTime = 120f;
    [SerializeField] private float dayTime = 120f;
    private bool isDay = true;

    private float workingTime;
    // Start is called before the first frame update
    void Start()
    {
        workingTime = dayTime;
        StartCoroutine(dayNightTimer(()=>isDay = !isDay));
    }
    
    
    public IEnumerator dayNightTimer(Action onFinish)
    {
        while (workingTime > 0)
        {
            workingTime -= Time.deltaTime;
            yield return null;
        }
        
        onFinish.Invoke();
        workingTime = (isDay ? dayTime : nightTime);
        globalLight.intensity = (isDay ? 1f : 0.5f);
        StartCoroutine(dayNightTimer(()=>isDay = !isDay));
    }

    
    public (float minutes, float seconds, bool isDay) getTime()
    {
        return (Mathf.FloorToInt(workingTime / 60), Mathf.FloorToInt(workingTime % 60), isDay);
    }
    
}
