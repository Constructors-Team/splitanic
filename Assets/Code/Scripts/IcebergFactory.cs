using UnityEngine;

public class IcebergFactory : MonoBehaviour
{
    // Calibration constants (Magic numbers)
    private const float SPAWN_OFFSET_X = 1f; // The offset for iceberg spawning position on the X-axis
    private const float MIN_ICEBERG_SIZE = 0.06f; // Minimum size for the iceberg
    private const float MAX_ICEBERG_SIZE = 0.35f; // Maximum size for the iceberg

    [Header("Iceberg Parameters")]
    [SerializeField] private float spawnInterval = 5f; // Time between spawns (can be adjusted in Inspector)
    [SerializeField] private GameObject icebergPrefab; // Assign your iceberg prefab in the Inspector

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
        // Randomly determine Y position within screen bounds
        float randomY = Random.Range(-screenHeight / 2, screenHeight / 2);

        // Calculate spawn position
        Vector3 spawnPosition = new Vector3(screenWidth / 2 + SPAWN_OFFSET_X, randomY, 0f);

        // Generate a random iceberg size within the defined limits
        float icebergSize = Random.Range(MIN_ICEBERG_SIZE, MAX_ICEBERG_SIZE);

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
