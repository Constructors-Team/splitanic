using UnityEngine;

public class IcebergFactory : MonoBehaviour
{
    public GameObject icebergPrefab; // Assign your iceberg prefab in the Inspector
    public float spawnInterval = 5f; // Time between spawns

    private float screenHeight;
    private float screenWidth;

    void Start()
    {
        screenHeight = Camera.main.orthographicSize * 2f;
        screenWidth = screenHeight * Camera.main.aspect;
        InvokeRepeating(nameof(SpawnIceberg), 0f, spawnInterval);
    }

    // This method is used to spawn an iceberg with a random position and size
    public void SpawnIceberg()
    {
        float randomY = Random.Range(-screenHeight / 2, screenHeight / 2);
        Vector3 spawnPosition = new Vector3(screenWidth / 2 + 1f, randomY, 0f);

        // Generate a random iceberg size
        float icebergSize = Random.Range(0.05f, 0.35f);

        // Call the method to spawn the iceberg with position and size
        SpawnIceberg(spawnPosition, icebergSize);
    }

    // This method spawns an iceberg at a specific position and with a specific size
    public void SpawnIceberg(Vector3 position, float icebergSize)
    {
        if (icebergPrefab == null) return;

        // Instantiate the iceberg at the given position
        GameObject newIceberg = Instantiate(icebergPrefab, position, Quaternion.identity);

        // Access the SimpleIcebergBehaviour script to initialize the iceberg
        SimpleIcebergBehaviour icebergScript = newIceberg.GetComponent<SimpleIcebergBehaviour>();

        if (icebergScript != null)
        {
            // Initialize the iceberg with the given position and size
            icebergScript.Initialize(position, icebergSize);
        }
    }
}