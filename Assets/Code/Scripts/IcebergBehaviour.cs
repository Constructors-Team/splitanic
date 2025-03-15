using UnityEngine;

public class IcebergBehaviour : MonoBehaviour
{
    public float forceMagnitude = 1f; // Force strength
    public float forceInterval = 0.5f; // Time in seconds between force applications
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
        if (rb == null)
        {
            Debug.LogError("No Rigidbody2D found on " + gameObject.name);
            return;
        }
        InvokeRepeating(nameof(ApplyRandomForce), 0f, forceInterval); // Apply force every X seconds
    }

    void ApplyRandomForce()
    {
        if (rb == null) return;

        Vector2 randomForce = Random.insideUnitCircle.normalized * forceMagnitude; // Random force direction
        Vector2 randomPoint = rb.position + Random.insideUnitCircle * 0.5f; // Random point near the rigidbody

        rb.AddForceAtPosition(randomForce, randomPoint, ForceMode2D.Impulse);
    }
}