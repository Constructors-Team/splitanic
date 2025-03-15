using UnityEngine;

public class BoatController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float acceleration = 30f;  // How fast the boat speeds up
    [SerializeField] private float maxSpeed = 20f;     // Max forward speed
    [SerializeField] private float maxReverseSpeed = 10f;  // Max reverse speed
    [SerializeField] private float turnSpeed = 7f;     // Turning power

    [Header("Boat Setup")]
    [SerializeField] private Transform rudderPosition; // Place this at the back of the boat

    private Rigidbody2D rb;
    private float currentSpeed = 0f;

    private KeyCode keyForward = KeyCode.W;
    private KeyCode keyBackward = KeyCode.S;
    private KeyCode keyTurnLeft = KeyCode.A;
    private KeyCode keyTurnRight = KeyCode.D;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleMovement();
        HandleTurning();
        currentSpeed = 0f;
    }

    private void HandleMovement()
    {
        float moveInput = 0f;

        if (Input.GetKey(keyForward)) moveInput = 1f;
        if (Input.GetKey(keyBackward)) moveInput = -1f;

        // Apply acceleration, limit speed
        currentSpeed += moveInput * acceleration;
        currentSpeed = Mathf.Clamp(currentSpeed, -maxReverseSpeed, maxSpeed);

        // Apply force in the direction the boat is facing
        rb.AddForce(transform.up * currentSpeed, ForceMode2D.Force);
    }

    private void HandleTurning()
    {
        float turnInput = 0f;

        if (Input.GetKey(keyTurnLeft)) turnInput = 1f;
        if (Input.GetKey(keyTurnRight)) turnInput = -1f;


        if (Mathf.Abs(currentSpeed) < Mathf.Epsilon)
        {
            currentSpeed = acceleration;
        }
        
        float turnForce = turnInput * turnSpeed * (currentSpeed / maxSpeed);

        // Apply force at the rudder position for natural turning
        rb.AddForceAtPosition(transform.right * turnForce, rudderPosition.position, ForceMode2D.Force);
    }
}
