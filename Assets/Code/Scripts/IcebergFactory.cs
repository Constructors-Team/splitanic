using UnityEngine;

public class IcebergFactory : MonoBehaviour
{
    [Header("Iceberg Parameters")]
    [SerializeField] public GameObject icebergPrefab; // The prefab for the iceberg to spawn
    [SerializeField] public float initialSpawnInterval = 5f; // Initial interval (in seconds) between iceberg spawns
    [SerializeField] public float spawnIntervalDecrement = 0.95f; // Increment (in seconds) to add to the spawn interval each minute

    [SerializeField] public float collisionDebounceTime = 1f; // Time (in seconds) to wait before allowing collisions again after a split
    
    [Header("Iceberg Spawn Parameters")]
    [SerializeField] public float spawnOffsetX = 1f; // The offset for iceberg spawning position on the X-axis
    [SerializeField] public float minIcebergSize = 0.06f; // Minimum size for the iceberg
    [SerializeField] public float maxIcebergSize = 0.35f; // Maximum size for the iceberg

    [SerializeField] public float initialCollisionDelay = 0.05f; // Initial delay before the iceberg can start colliding after being spawned

    [Header("Iceberg Splitting Parameters")]
    [SerializeField] public float minSizeForSplitting = 0.1f; // Minimum size an iceberg must have before it can split
    [SerializeField] public float sizeReductionFactor = 0.5f; // Factor by which iceberg size is reduced when it splits
    [SerializeField] public float icebergOffsetXForSplit = 1f; // The offset for the position of the new iceberg after splitting

    private float screenHeight; // Height of the screen, used for calculating spawn positions
    private float screenWidth; // Width of the screen, used for calculating spawn positions

    void Start()
    {
        screenHeight = Camera.main.orthographicSize * 2f; // Calculate screen height based on camera settings
        screenWidth = screenHeight * Camera.main.aspect; // Calculate screen width based on aspect ratio

        // Start spawning icebergs repeatedly based on the initial spawn interval
        InvokeRepeating(nameof(SpawnIceberg), 0f, initialSpawnInterval);
        
        // Start increasing spawn interval every minute (60 seconds)
        InvokeRepeating(nameof(DecreaseSpawnInterval), 60f, 60f); // Every 60 seconds
    }

    // Method to increase the spawn interval every minute
    private void DecreaseSpawnInterval()
    {
        initialSpawnInterval = spawnIntervalDecrement * spawnIntervalDecrement;
        CancelInvoke(nameof(SpawnIceberg)); // Cancel the current spawn invoke
        InvokeRepeating(nameof(SpawnIceberg), initialSpawnInterval, initialSpawnInterval); // Re-invoke with new interval
    }

    // Method to spawn an iceberg at a random position and with a random size
    public void SpawnIceberg()
    {
        // Randomly determine Y position within screen bounds
        float randomY = Random.Range(-screenHeight / 2, screenHeight / 2);

        // Calculate spawn position with an offset on the X-axis
        Vector3 spawnPosition = new Vector3(screenWidth / 2 + spawnOffsetX, randomY, 0f);

        // Generate a random iceberg size within the defined range
        float icebergSize = Random.Range(minIcebergSize, maxIcebergSize);

        // Call the method to spawn the iceberg at the calculated position and with the determined size
        SpawnIceberg(spawnPosition, icebergSize);
    }

    // Method to spawn an iceberg at a specific position with a specific size
    public void SpawnIceberg(Vector3 position, float icebergSize)
    {
        if (icebergPrefab == null) return; // Ensure that the prefab is assigned before spawning

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
