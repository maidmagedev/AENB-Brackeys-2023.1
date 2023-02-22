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
            print("stop spawning enemies");
            // Kill any remaining enemies at start of day--NOTE this is not very scalable or efficient I will rework this later when I have time
            GameObject[] all_enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in all_enemies)
            {
                print("killing enemies during day time");
                if (enemy.TryGetComponent<SpiderEnemy>(out SpiderEnemy spider))
                {
                    spider.Die();
                }
                else
                {
                    Destroy(enemy);
                }
            }
            FindObjectOfType<BackgroundMusic>().Play_day_track();
        }
        else
        {
            workingTime = nightTime;
            globalLight.intensity = 0.0f;
            print("begin spawning enemies");
            StartCoroutine(FindObjectOfType<EnemySpawner>().spawnEnemies());
            FindObjectOfType<BackgroundMusic>().Play_night_track();
        }
        StartCoroutine(dayNightTimer(()=>isDay = !isDay));
    }

    
    public (float minutes, float seconds, bool isDay) getTime()
    {
        return (Mathf.FloorToInt(workingTime / 60), Mathf.FloorToInt(workingTime % 60), isDay);
    }

    public bool getIsDay()
    {
        return isDay;
    }
    
}
