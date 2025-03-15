using UnityEngine;

public class IcebergFactory : MonoBehaviour
{
    // Calibration parameters exposed to the inspector for easy adjustment
    [Header("Iceberg Parameters")]
    [SerializeField] public GameObject icebergPrefab; // Assign your iceberg prefab in the Inspector
    [SerializeField] public float initialSpawnInterval = 5f; // Time between spawns (can be adjusted in Inspector)

    [SerializeField] public float collisionDebounceTime = 1f;
    
    // Iceberg spawn-related parameters
    [SerializeField] public float spawnOffsetX = 1f; // The offset for iceberg spawning position on the X-axis
    [SerializeField] public float minIcebergSize = 0.06f; // Minimum size for the iceberg
    [SerializeField] public float maxIcebergSize = 0.35f; // Maximum size for the iceberg

    // Iceberg splitting parameters
    [Header("Iceberg Splitting Parameters")]
    [SerializeField] public float minSizeForSplitting = 0.1f; // Minimum size to stop splitting
    [SerializeField] public float sizeReductionFactor = 0.5f; // Factor by which iceberg size is reduced on split
    [SerializeField] public float icebergOffsetXForSplit = 1f; // X-axis offset for new iceberg position after splitting

    private float screenHeight;
    private float screenWidth;
    void Start()
    {
        screenHeight = Camera.main.orthographicSize * 2f;
        screenWidth = screenHeight * Camera.main.aspect;

        InvokeRepeating(nameof(SpawnIceberg), 0f, initialSpawnInterval);
    }

    // This method is used to spawn an iceberg with a random position and size
    public void SpawnIceberg()
    {
        // Randomly determine Y position within screen bounds
        float randomY = Random.Range(-screenHeight / 2, screenHeight / 2);

        // Calculate spawn position
        Vector3 spawnPosition = new Vector3(screenWidth / 2 + spawnOffsetX, randomY, 0f);

        // Generate a random iceberg size within the defined limits
        float icebergSize = Random.Range(minIcebergSize, maxIcebergSize);

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

        // Set the IcebergFactory as the parent of the newly created iceberg
        newIceberg.transform.SetParent(transform); // 'transform' refers to the IcebergFactory's transform
    }

}
