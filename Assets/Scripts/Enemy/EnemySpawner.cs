using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;   // Префаб врага
    public Transform spawnPoint1;    // Первая точка спавна
    public Transform spawnPoint2;    // Вторая точка спавна
    
    private void Start()
    {
        // Спавним врагов в каждой из точек спавна с передачей точек патрулирования
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        // Спавним врага в каждой из трех точек спавна
        SpawnEnemyAt(spawnPoint1);
        SpawnEnemyAt(spawnPoint2);
   
    }

    void SpawnEnemyAt(Transform spawnPoint)
    {
        if (spawnPoint != null)
        {
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
