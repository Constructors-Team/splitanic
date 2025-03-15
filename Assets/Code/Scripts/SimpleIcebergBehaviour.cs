using UnityEngine;

public class SimpleIcebergBehaviour : MonoBehaviour
{
    private float speed;
    private Vector3 targetPosition;

    void Start()
    {
        float screenHeight = Camera.main.orthographicSize * 2f;
        float screenWidth = screenHeight * Camera.main.aspect;

        // Set a random speed for each iceberg
        speed = Random.Range(1f, 3f);

        float randomY = Random.Range(-screenHeight / 2, screenHeight / 2);
        float startX = screenWidth / 2 + 1f;
        float targetX = -screenWidth / 2 - 2f;
        float targetY = Random.Range(-screenHeight / 2, screenHeight / 2);

        transform.position = new Vector3(startX, randomY, 0f);
        targetPosition = new Vector3(targetX, targetY, 0f);

        // Randomize size
        float randomSize = Random.Range(0.05f, 0.35f);
        transform.localScale = new Vector3(randomSize, randomSize, 1f);
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (transform.position.x <= targetPosition.x)
        {
            Destroy(gameObject);
        }
    }
}