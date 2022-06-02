using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObjectBoundsSO spawnerBounds;
        
    [Header("Spawnable Game Object References")]
    [SerializeField] Enemy enemyPrefab;
    [SerializeField] GameObject[] powerups;
    [SerializeField] GameObject powerupContainer;
    [SerializeField] GameObject enemyContainer;

    [Header("Spawnable Game Objects Spawn Frequency Tuning")]
    [SerializeField] float minTimeBetweenEnemySpawns = 2f;
    [SerializeField] float maxTimeBetweenEnemySpawns = 5f;
    [SerializeField] float minTimeBetweenPowerupSpawns = 3f;
    [SerializeField] float maxTimeBetweenPowerupSpawns = 7f;

    private bool spawnerActive = true;


    public void StartSpawning()
    {
        StartCoroutine("SpawnEnemies");
        StartCoroutine("SpawnPowerups");
    }

    IEnumerator SpawnEnemies()
    {
        yield return new WaitForSecondsRealtime(2f);
        while (spawnerActive)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(spawnerBounds.minXPosition, spawnerBounds.maxXPosition), spawnerBounds.maxYPosition, 0);
            Enemy newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            newEnemy.transform.parent = enemyContainer.transform;

            yield return new WaitForSecondsRealtime(Random.Range(minTimeBetweenEnemySpawns, maxTimeBetweenEnemySpawns));
        }
    }

    IEnumerator SpawnPowerups()
    {
        while (spawnerActive)
        {
            yield return new WaitForSecondsRealtime(Random.Range(minTimeBetweenPowerupSpawns, maxTimeBetweenPowerupSpawns));

            int randomPowerUp = Random.Range(0, powerups.Length);
            Vector3 spawnPosition = new Vector3(Random.Range(spawnerBounds.minXPosition, spawnerBounds.maxXPosition), spawnerBounds.maxYPosition, 0);

            GameObject newPowerup = Instantiate(powerups[randomPowerUp], spawnPosition, Quaternion.identity);
            newPowerup.transform.parent = powerupContainer.transform;
        }
    }

    public void OnPlayerDeath()
    {
        spawnerActive = false;
        Destroy(powerupContainer.gameObject);
        Destroy(enemyContainer.gameObject);
        Destroy(gameObject);
    }
}