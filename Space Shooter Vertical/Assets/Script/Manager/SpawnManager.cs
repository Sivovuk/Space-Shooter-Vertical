using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private int minTimeSpawn;
    [SerializeField]
    private int maxTimeSpawn;

    [Space]

    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private GameObject enemyContainer;
    [SerializeField]
    private GameObject[] powerups;

    private bool _stopSpawning = false;

    void Start()
    {
        
    }


    public void StartSpawning() 
    {
        StartCoroutine(SpawnRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }

    IEnumerator SpawnRoutine() 
    {
        yield return new WaitForSeconds(1f);
        while (!_stopSpawning && enemyPrefab != null)
        {
            Vector3 position = new Vector3(Random.Range(-8, 8), 6, 0);
            GameObject spawn = Instantiate(enemyPrefab, position, Quaternion.identity);
            spawn.transform.parent = enemyContainer.transform;

            yield return new WaitForSeconds(5);

        }
    }

    IEnumerator SpawnPowerUpRoutine() 
    {
        yield return new WaitForSeconds(1.5f);
        while (!_stopSpawning) 
        {
            Vector3 position = new Vector3(Random.Range(-8, 8), 6, 0);

            int random = Random.Range(0, powerups.Length);

            GameObject spawn = Instantiate(powerups[random], position, Quaternion.identity);
            spawn.transform.parent = enemyContainer.transform;

            int radnom = Random.Range(minTimeSpawn,maxTimeSpawn);
            yield return new WaitForSeconds(radnom);
        }
    }

    public void OnPlayerDeath() 
    {
        _stopSpawning = true;
    }
	
}
