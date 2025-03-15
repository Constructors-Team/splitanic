using UnityEngine;

public class BaseIceberg : MonoBehaviour
{
    // Calibration constants (Magic numbers)
    private const float MIN_SIZE = 0.1f; // Minimum size to stop splitting
    private const float SIZE_REDUCTION_FACTOR = 0.5f; // Factor by which iceberg size is reduced on split
    private const float OFFSET_X = 1f; // X-axis offset for new iceberg position

    [Header("Iceberg Parameters")]
    [SerializeField] private float minSizeForSplitting = 0.1f; // Minimum size to stop splitting
    [SerializeField] private float sizeReductionFactor = 0.5f; // Factor by which iceberg size is reduced on split
    [SerializeField] private float icebergOffsetX = 1f; // X-axis offset for new iceberg position

    private IcebergFactory factory; // Reference to IcebergFactory

    void Start()
    {
        // Find the factory in the scene
        factory = FindObjectOfType<IcebergFactory>(); // Automatically finds the IcebergFactory in the scene
    }

    protected void SplitIceberg()
    {
        // Check if the iceberg size is less than the minimum size for splitting
        if (transform.localScale.x < minSizeForSplitting)
        {
            // Remove the tag when the iceberg is smaller than the threshold size
            gameObject.tag = "Untagged"; // or use gameObject.tag = null if you want to remove the tag completely
            return; // No further splitting
        }

        // Reduce size by the defined factor
        float newSize = transform.localScale.x * sizeReductionFactor; // Reduce size

        // Apply the reduced size to the current iceberg
        transform.localScale = new Vector3(newSize, newSize, 1f);

        // Calculate iceberg size based on current scale
        float icebergSize = transform.localScale.x;  // Since the iceberg is square, the size is equal in both dimensions

        // Calculate offset based on current iceberg size
        Vector3 newPosition = transform.position + new Vector3(icebergOffsetX, 0f, 0f); // Offset on X-axis

        // Spawn a new iceberg using the factory
        if (factory != null)
        {
            factory.SpawnIceberg(newPosition, icebergSize); // Pass the adjusted position and size to the factory
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SplitIceberg();
        }
    }
}
