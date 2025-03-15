using UnityEngine;

public class SimpleIcebergBehaviour : BaseIceberg
{
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
        speed = Random.Range(1f, 3f);

        float targetX = -screenWidth / 2 - 2f;
        float targetY = Random.Range(-screenHeight / 2, screenHeight / 2);

        targetPosition = new Vector3(targetX, targetY, 0f);

        // Set the iceberg size (scale) passed from the factory
        transform.localScale = new Vector3(icebergSize, icebergSize, 1f);

        // Set a random rotation on the Z-axis
        float randomRotation = Random.Range(0f, 360f); // Random rotation from 0 to 360 degrees
        transform.rotation = Quaternion.Euler(0f, 0f, randomRotation); // Apply the rotation

        // Apply an initial torque for dynamic rotation
        if (rb != null)
        {
            float randomTorque = Random.Range(-100f, 100f); // Random torque for rotation
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

    void OnMouseDown()
    {
        SplitIceberg();
    }
}