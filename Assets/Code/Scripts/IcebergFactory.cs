using UnityEngine;

public class IcebergFactory : MonoBehaviour
{
    public GameObject icebergPrefab; // Assign your iceberg prefab in the Inspector
    public float spawnInterval = 0.5f; // Time between spawns

    private float screenHeight;
    private float screenWidth;

    void Start()
    {
        screenHeight = Camera.main.orthographicSize * 2f;
        screenWidth = screenHeight * Camera.main.aspect;
        InvokeRepeating(nameof(SpawnIceberg), 0f, spawnInterval);
    }

    void SpawnIceberg()
    {
        if (icebergPrefab == null) return;

        float randomY = Random.Range(-screenHeight / 2, screenHeight / 2);
        Vector3 spawnPosition = new Vector3(screenWidth / 2 + 1f, randomY, 0f);

        Instantiate(icebergPrefab, spawnPosition, Quaternion.identity);
    }
}