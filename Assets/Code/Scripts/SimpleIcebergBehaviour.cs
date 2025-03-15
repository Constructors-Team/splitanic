using UnityEngine;

public class SimpleIcebergBehaviour : BaseIceberg
{
    // Calibration constants (Magic numbers)
    private const float MIN_SPEED = 1f; // Minimum speed for iceberg movement
    private const float MAX_SPEED = 3f; // Maximum speed for iceberg movement
    private const float MIN_TORQUE = -100f; // Minimum torque for dynamic rotation
    private const float MAX_TORQUE = 100f; // Maximum torque for dynamic rotation
    private const float MIN_ROTATION = 0f; // Minimum rotation angle (degrees)
    private const float MAX_ROTATION = 360f; // Maximum rotation angle (degrees)
    private const float TARGET_X_OFFSET = -2f; // X position offset for target position

    private float speed;
    private Vector3 targetPosition;
    private Rigidbody2D rb;

    public void Initialize(Vector3 startPosition, float icebergSize)
    {
        float screenHeight = Camera.main.orthographicSize * 2f;
        float screenWidth = screenHeight * Camera.main.aspect;

        // Set position based on the provided value
        transform.position = startPosition;

        // Get Rigidbody2D for dynamic physics behavior
        rb = GetComponent<Rigidbody2D>();

        // Set a random speed for each iceberg
        speed = Random.Range(MIN_SPEED, MAX_SPEED);

        // Set target position with some offset
        float targetX = -screenWidth / 2 + TARGET_X_OFFSET;
        float targetY = Random.Range(-screenHeight / 2, screenHeight / 2);

        targetPosition = new Vector3(targetX, targetY, 0f);

        // Set the iceberg size (scale) passed from the factory
        transform.localScale = new Vector3(icebergSize, icebergSize, 1f);

        // Set a random rotation on the Z-axis
        float randomRotation = Random.Range(MIN_ROTATION, MAX_ROTATION); // Random rotation from 0 to 360 degrees
        transform.rotation = Quaternion.Euler(0f, 0f, randomRotation); // Apply the rotation

        // Apply an initial torque for dynamic rotation
        if (rb != null)
        {
            float randomTorque = Random.Range(MIN_TORQUE, MAX_TORQUE); // Random torque for rotation
            rb.AddTorque(randomTorque, ForceMode2D.Force); // Apply torque to rotate the iceberg
        }
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
}
