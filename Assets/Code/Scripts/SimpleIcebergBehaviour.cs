using UnityEngine;

public class SimpleIcebergBehaviour : MonoBehaviour
{
    public float speed = 2f; // Constant movement speed
    private Vector3 targetPosition;

    void Start()
    {
        float screenHeight = Camera.main.orthographicSize * 2f;
        float screenWidth = screenHeight * Camera.main.aspect;

        float randomY = Random.Range(-screenHeight / 2, screenHeight / 2); // Random Y position
        float startX = screenWidth / 2 + 1f; // Start slightly offscreen (right)
        float targetX = -screenWidth / 2 - 1f; // End slightly offscreen (left)
        float targetY = Random.Range(-screenHeight / 2, screenHeight / 2); // Random target Y

    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }
}