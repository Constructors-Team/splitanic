using UnityEngine;

public class IcebergFactory : MonoBehaviour
{
    public GameObject icebergPrefab; // Assign your iceberg prefab in the Inspector
    public float spawnInterval = 2f; // Time between spawns

    public float minSpeed = 1f;
    public float maxSpeed = 3f;
    public Vector2 minSize = new Vector2(0.5f, 0.5f);
    public Vector2 maxSize = new Vector2(1.5f, 1.5f);

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

        GameObject iceberg = Instantiate(icebergPrefab, spawnPosition, Quaternion.identity);

        // Randomize speed and size
        float randomSpeed = Random.Range(minSpeed, maxSpeed);
        Vector2 randomScale = new Vector2(Random.Range(minSize.x, maxSize.x), Random.Range(minSize.y, maxSize.y));
        iceberg.transform.localScale = randomScale;

        // Apply parameters to the iceberg script
        SimpleIcebergBehaviour icebergScript = iceberg.GetComponent<SimpleIcebergBehaviour>();
        if (icebergScript != null)
        {
            icebergScript.speed = randomSpeed;
        }
    }
}