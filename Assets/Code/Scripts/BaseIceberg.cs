using UnityEngine;

public class BaseIceberg : MonoBehaviour
{
    protected IcebergFactory factory; // Reference to IcebergFactory
    private bool canCollide = true; // Debouncer flag

    void Start()
    {
        // Find the factory in the scene
        factory = FindObjectOfType<IcebergFactory>(); // Automatically finds the IcebergFactory in the scene
    }

    protected void SplitIceberg()
    {
        if (factory == null) return;

        // Get the settings from the factory
        float minSizeForSplitting = factory.minSizeForSplitting;
        float sizeReductionFactor = factory.sizeReductionFactor;
        float icebergOffsetX = factory.icebergOffsetXForSplit;
        float debounceTime = factory.collisionDebounceTime;

        // Check if the iceberg size is less than the minimum size for splitting
        if (transform.localScale.x < minSizeForSplitting)
        {
            return; // No further splitting
        }

        // Reduce size by the defined factor
        float newSize = transform.localScale.x * sizeReductionFactor;

        // Apply the reduced size to the current iceberg
        transform.localScale = new Vector3(newSize, newSize, 1f);

        // Calculate offset and spawn a new iceberg
        Vector3 newPosition = transform.position + new Vector3(icebergOffsetX, 0f, 0f);
        factory.SpawnIceberg(newPosition, newSize);
        factory.SpawnIceberg(newPosition, newSize);
        Destroy(gameObject);
    }

    private void ResetCollision()
    {
        canCollide = true;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (canCollide && other.gameObject.CompareTag("IceBreaker"))
        {
            SplitIceberg();
        }
    }
}