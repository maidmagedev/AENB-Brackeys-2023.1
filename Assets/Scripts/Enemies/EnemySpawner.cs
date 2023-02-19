using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [Header("EnemyTypes List")] 
    [SerializeField] private List<GameObject> EnemyTypes;
    [Header("EnemyType Limits List")] 
    [SerializeField] private List<int> EnemyTypeLimits;
    
    private Dictionary<string, int> EnemyTypeCounts;

    [Header("General Settings")]
    [SerializeField] private float waveTime = 5f;
    [SerializeField] private int numEnemiesPerWave = 2;
    [SerializeField] private List<GameObject> Enemies_Planet1;
    
    [Header("Currently not implemented")]
    [SerializeField] private List<GameObject> Enemies_Planet2;
    
    private void Start()
    {
        EnemyTypeCounts = new();
        foreach (GameObject g in EnemyTypes)
        {
            EnemyTypeCounts.Add(g.name, 0);
        }
    }

    public IEnumerator spawnEnemies()
    {
        // spawn a random set of enemies from the list of enemy types
        // Use a switch statement based off the current scene to decide whether to use Enemies_Planet1 or Enemies_Planet2...
        for (int i = 0; i < numEnemiesPerWave; i++)
        {
            GameObject randomEnemy = Enemies_Planet1[Random.Range(0, 1)];
            if ( (EnemyTypeCounts[randomEnemy.name] >= EnemyTypeLimits[EnemyTypes.IndexOf(randomEnemy)]))
            {
                print(randomEnemy.name + " spawned "+ EnemyTypeCounts[randomEnemy.name] + " spawn limit reached");
                if (randomEnemy = Enemies_Planet1[0])
                {
                    randomEnemy = Enemies_Planet1[1];
                }
                else
                {
                    randomEnemy = Enemies_Planet1[0];
                }
            }
            
            // should probably spawn at random(ish) coordinates 
            Instantiate(randomEnemy, new Vector3(0, 15, 0), Quaternion.identity);
            EnemyTypeCounts[randomEnemy.name] += 1;
        }
        reset_EnemyTypeCounts();
        print("spawned enemies");
        yield return new WaitForSeconds(waveTime);
        if (!FindObjectOfType<DayNightCycle>().getIsDay())
        {
            StartCoroutine(spawnEnemies());
        }
        //StartCoroutine(spawnEnemies());
    }

    private void reset_EnemyTypeCounts()
    {
        foreach (GameObject g in EnemyTypes)
        {
            EnemyTypeCounts[g.name] = 0;
        }
    }
}
