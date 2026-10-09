using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private float spawnInterval = 1f;  
    [SerializeField] private float initialDelay = 1f;
    void Start()
    {
        InvokeRepeating(nameof(SpawnObstacle), initialDelay, spawnInterval);
    }

    private void SpawnObstacle()
    {
        Instantiate(obstaclePrefab, transform.position, transform.rotation);
    }
}
