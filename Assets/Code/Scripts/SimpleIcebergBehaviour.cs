using UnityEngine;

public class SimpleIcebergBehaviour : BaseIceberg
{
    private float speed;
    private Vector3 targetPosition;

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

        // Set the iceberg size (scale) passed from the factory
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
}
