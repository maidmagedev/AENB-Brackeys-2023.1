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
        if (isDay)
        {
            workingTime = dayTime;
            globalLight.intensity = 1.0f;
        }
        else
        {
            workingTime = nightTime;
            globalLight.intensity = 0.5f;
            StartCoroutine(FindObjectOfType<EnemySpawner>().spawnEnemies());
        }
        StartCoroutine(dayNightTimer(()=>isDay = !isDay));
    }

    
    public (float minutes, float seconds, bool isDay) getTime()
    {
        return (Mathf.FloorToInt(workingTime / 60), Mathf.FloorToInt(workingTime % 60), isDay);
    }
    
}
