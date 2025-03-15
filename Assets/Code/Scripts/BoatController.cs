using UnityEngine;

public class BoatController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float acceleration = 30f;  // How fast the boat speeds up
    [SerializeField] private float maxSpeed = 20f;     // Max forward speed
    [SerializeField] private float maxReverseSpeed = 10f;  // Max reverse speed
    [SerializeField] private float turnSpeed = 7f;     // Turning power
    [SerializeField] private float currentSpeed = 7f;     // Force applied to player when not moving

    [Header("Boat Setup")]
    [SerializeField] private Transform rudderPosition; // Place this at the back of the boat

    private Rigidbody2D rb;

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
    }

    private void HandleMovement()
    {
        float moveInput = 0f;

        if (Input.GetKey(keyForward)) moveInput = 1f;
        if (Input.GetKey(keyBackward)) moveInput = -1f;

        // Apply acceleration, limit speed
        var moveSpeed = moveInput * acceleration;
        moveSpeed = Mathf.Clamp(moveSpeed, -maxReverseSpeed, maxSpeed);

        if (Mathf.Abs(moveInput) > Mathf.Epsilon)
        {
            // Apply force in the direction the boat is facing
            rb.AddForce(transform.up * moveSpeed, ForceMode2D.Force);
        }
        else
        {
            // Apply force to move player on the left when he is not moving
            rb.AddForce(Vector3.left * currentSpeed, ForceMode2D.Force);
        }

        
    }

    private void HandleTurning()
    {
        float turnInput = 0f;

        if (Input.GetKey(keyTurnLeft)) turnInput = 1f;
        if (Input.GetKey(keyTurnRight)) turnInput = -1f;

        float turnForce = turnInput * turnSpeed; 

        rb.AddForceAtPosition(transform.right * turnForce, rudderPosition.position, ForceMode2D.Force);
    }
}
