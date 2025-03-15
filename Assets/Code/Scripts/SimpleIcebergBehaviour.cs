using UnityEngine;

public class SimpleIcebergBehaviour : BaseIceberg
{
    [Header("Movement Settings")]
    [SerializeField] private float minSpeed = 1f; // Minimum speed for iceberg movement
    [SerializeField] private float maxSpeed = 3f; // Maximum speed for iceberg movement

    [Header("Rotation Settings")]
    [SerializeField] private float minTorque = -100f; // Minimum torque for dynamic rotation
    [SerializeField] private float maxTorque = 100f; // Maximum torque for dynamic rotation
    [SerializeField] private float minRotation = 0f; // Minimum rotation angle (degrees)
    [SerializeField] private float maxRotation = 360f; // Maximum rotation angle (degrees)

    [Header("Target Position Settings")]
    [SerializeField] private float targetXOffset = -2f; // X position offset for target position

    private float speed;
    private Vector3 targetPosition;
    private Rigidbody2D rb;

    public void Initialize(Vector3 startPosition, float icebergSize)
    {
        float screenHeight = Camera.main.orthographicSize * 2f;
        float screenWidth = screenHeight * Camera.main.aspect;

        // Set position based on the provided value
        transform.position = startPosition;

        // Get Rigidbody2D for physics behavior
        rb = GetComponent<Rigidbody2D>();

        // Set a random speed for each iceberg
        speed = Random.Range(minSpeed, maxSpeed);

        // Set target position with some offset
        float targetX = -screenWidth / 2 + targetXOffset;
        float targetY = Random.Range(-screenHeight / 2, screenHeight / 2);
        targetPosition = new Vector3(targetX, targetY, 0f);

        // Set the iceberg size (scale) passed from the factory
        transform.localScale = new Vector3(icebergSize, icebergSize, 1f);

        // Set a random rotation on the Z-axis
        float randomRotation = Random.Range(minRotation, maxRotation);
        transform.rotation = Quaternion.Euler(0f, 0f, randomRotation);

        // Apply an initial torque for dynamic rotation
        if (rb != null)
        {
            float randomTorque = Random.Range(minTorque, maxTorque);
            rb.AddTorque(randomTorque, ForceMode2D.Force);
        }
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Destroy the iceberg when it reaches the target position
        if (transform.position.x <= targetPosition.x)
        {
            Destroy(gameObject);
        }
    }
}
