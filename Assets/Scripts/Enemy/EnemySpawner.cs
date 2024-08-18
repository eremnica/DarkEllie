using UnityEngine;


public class EnemySpawner: MonoBehaviour
{
    public static EnemySpawner Instance;
    public GameObject enemyPrefab;
    public Transform spawnPoint;
    public float respawnDelay = 30f;

    private GameObject currentEnemy;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        SpawnEnemy();
    }

    void SpawnEnemy()
    {
        if (currentEnemy == null)
        {
            currentEnemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }

    public void EnemyDied()
    {

        currentEnemy = null;


        Invoke("SpawnEnemy", respawnDelay);
    }
}

