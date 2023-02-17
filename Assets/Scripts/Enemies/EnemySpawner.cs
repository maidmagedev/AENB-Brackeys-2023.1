using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float waveTime = 5f;
    [SerializeField] private List<GameObject> Enemies_1;
    [SerializeField] private List<GameObject> Enemies_2;
    
    public IEnumerator spawnEnemies()
    {
        // spawn a random set of enemies from the list of enemy types
        for (int i = 0; i < 2; i++)
        {
            GameObject randomEnemy = Enemies_1[Random.Range(0, Enemies_1.Count - 1)];
            Instantiate(randomEnemy, new Vector3(0, 15, 0), Quaternion.identity);
        }
        print("spawned 2 enemies");
        yield return new WaitForSeconds(waveTime);
        if (!FindObjectOfType<DayNightCycle>().getIsDay())
        {
            StartCoroutine(spawnEnemies());
        }
        //StartCoroutine(spawnEnemies());
    }
}
