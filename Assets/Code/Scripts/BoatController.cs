using Unity.Mathematics.Geometry;
using UnityEngine;
using UnityEngine.Serialization;

public class BoatController : MonoBehaviour
{
    [SerializeField]
    private float acceleration;
    [SerializeField]
    private float rotationSpeed;
    
    private KeyCode keyForward;
    private KeyCode keyBackward;
    private KeyCode keyTurnLeft;
    private KeyCode keyTurnRight;
    
    
    private Rigidbody2D rb;

    public BoatController()
    {
        keyForward = KeyCode.W;
        keyTurnRight = KeyCode.D;
        keyBackward = KeyCode.S;
        keyTurnLeft = KeyCode.A;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    // Update is called once per frame
    void Update()
    {
        handleMove();
        handleRotation();
        
        rb.centerOfMass = new Vector2(0, Mathf.Max(1, rb.linearVelocity.y) * -Mathf.Sign(rb.linearVelocity.y));
    }

    private void handleRotation()
    {
        var torque = 0f;
        
        if(Input.GetKey(keyTurnLeft))
        {
            torque += rotationSpeed;
        }
        
        if(Input.GetKey(keyTurnRight))
        {
            torque -= rotationSpeed;
        }
        
        rb.AddTorque(torque * Time.deltaTime, ForceMode2D.Force);
    }
    
    private void handleMove()
    {
        var move = Vector3.zero;
        
        if (Input.GetKey(keyForward))
        {
            move += transform.up * acceleration;
        }
            
        if(Input.GetKey(keyBackward))
        {
            move -= transform.up * (acceleration / 2);
        }
        
        rb.AddForce(move, ForceMode2D.Force);
    }
}
