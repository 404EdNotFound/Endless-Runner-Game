using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject groundPrefab;
    public GameObject obstaclePrefab;
    public Vector3 spawnPoint;
    public Transform obstacleSpawn;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnGround()
    {
        GameObject tempGround = Instantiate(groundPrefab, spawnPoint, Quaternion.identity);
        spawnPoint = tempGround.transform.GetChild(0).transform.position;

        obstacleSpawn = tempGround.transform.Find("ObstaclePos");

        if (obstacleSpawn != null) {
            GameObject tempObstacle = Instantiate(obstaclePrefab, obstacleSpawn.position, Quaternion.identity, transform);
            Destroy(tempObstacle, 5f);
        }
    }
}
