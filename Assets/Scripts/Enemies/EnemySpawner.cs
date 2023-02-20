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
            GameObject randomEnemy = Enemies_Planet1[Random.Range(0, Enemies_Planet1.Count - 1)];
            int j = 0;
            while ((EnemyTypeCounts[randomEnemy.name] >= EnemyTypeLimits[EnemyTypes.IndexOf(randomEnemy)]) && (j < Enemies_Planet1.Count * 2))
            {
                print(randomEnemy.name + " spawned "+ EnemyTypeCounts[randomEnemy.name] + " spawn limit reached");
                randomEnemy = Enemies_Planet1[Random.Range(0, Enemies_Planet1.Count - 1)];
                j++;
            }
            
            Instantiate(randomEnemy, new Vector3(-4, 26, 0), Quaternion.identity);
            Instantiate(randomEnemy, new Vector3(83, -46, 0), Quaternion.identity);
            Instantiate(randomEnemy, new Vector3(-11, -101, 0), Quaternion.identity);
            Instantiate(randomEnemy, new Vector3(-84, -45, 0), Quaternion.identity);
            EnemyTypeCounts[randomEnemy.name] += 1;
        }
        
        print("spawned enemies");
        yield return new WaitForSeconds(waveTime);
        if (!FindObjectOfType<DayNightCycle>().getIsDay())
        {
            StartCoroutine(spawnEnemies());
        }
        else
        {
            reset_EnemyTypeCounts();
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

    private Vector3 getSpawnLocation()
    {
        GameObject player = GameObject.Find("Player");
        Vector3 location = new Vector3(player.transform.position.x, player.transform.position.y + 15, 0);
        return location;
    }
}
