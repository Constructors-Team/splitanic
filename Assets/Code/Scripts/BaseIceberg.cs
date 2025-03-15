using UnityEngine;

public class BaseIceberg : MonoBehaviour
{
    
    private IcebergFactory factory; // Reference to IcebergFactory

    void Start()
    {
        // Find the factory in the scene
        factory = FindObjectOfType<IcebergFactory>(); // Automatically finds the IcebergFactory in the scene
    }
    protected void SplitIceberg()
    {
        if (transform.localScale.x < 0.1f) return; // Avoid infinite splitting

        float newSize = transform.localScale.x * 0.5f; // Reduce size

        // Apply the reduced size to the current iceberg
        transform.localScale = new Vector3(newSize, newSize, 1f);

        // Calculate iceberg size based on current scale
        float icebergSize = transform.localScale.x;  // Since the iceberg is square, the size is equal in both dimensions

        // Calculate offset based on current iceberg size
        float offset = 1f;

        Vector3 newPosition = transform.position + new Vector3(offset, 0f, 0f); // Offset on X-axis

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
