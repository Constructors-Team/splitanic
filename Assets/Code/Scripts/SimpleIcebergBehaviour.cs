using UnityEngine;

public class SimpleIcebergBehaviour : BaseIceberg
{
    private float speed;
    private Vector3 targetPosition;
    private IcebergFactory factory; // Reference to IcebergFactory

    void Start()
    {
        // Find the factory in the scene
        factory = FindObjectOfType<IcebergFactory>(); // Automatically finds the IcebergFactory in the scene
    }

    // Modified Initialize method to accept size as a parameter
    public void Initialize(Vector3 startPosition, float icebergSize)
    {
        float screenHeight = Camera.main.orthographicSize * 2f;
        float screenWidth = screenHeight * Camera.main.aspect;

        // Set position based on the provided value
        transform.position = startPosition;

        // Set a random speed for each iceberg
        speed = Random.Range(1f, 3f);

        float targetX = -screenWidth / 2 - 2f;
        float targetY = Random.Range(-screenHeight / 2, screenHeight / 2);

        targetPosition = new Vector3(targetX, targetY, 0f);

        // Set the size of the iceberg based on the parameter passed
        transform.localScale = new Vector3(icebergSize, icebergSize, 1f);
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Destroy the iceberg when it reaches the target position
        if (transform.position.x <= targetPosition.x)
        {
            Destroy(gameObject);
        }
    }

    void OnMouseDown()
    {
        SplitIceberg();
    }

    void SplitIceberg()
    {
        if (transform.localScale.x < 0.1f) return; // Avoid infinite splitting

        float newSize = transform.localScale.x * 0.5f; // Reduce size

        // Apply the reduced size to the current iceberg
        transform.localScale = new Vector3(newSize, newSize, 1f);

        // Calculate offset based on current iceberg size
        float offset = 1f;
        Vector3 newPosition = transform.position + new Vector3(offset, 0f, 0f); // Offset on X-axis

        if (factory != null)
        {
            factory.SpawnIceberg(newPosition, newSize); // Pass the adjusted position and size to the factory
        }
    }
}
