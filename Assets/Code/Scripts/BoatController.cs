using Unity.Mathematics.Geometry;
using UnityEngine;
using UnityEngine.Serialization;

public class BoatController : MonoBehaviour
{
    [SerializeField]
    private float acceleration;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private GameObject centerOfMass;
    
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
        
        rb.centerOfMass = new Vector2(
            Mathf.Min(0.5f, Mathf.Abs(rb.linearVelocity.x)) * -Mathf.Sign(rb.linearVelocity.x),
            Mathf.Min(1, Mathf.Abs(rb.linearVelocity.y)) * -Mathf.Sign(rb.linearVelocity.y)
        );
        centerOfMass.transform.position = transform.position + new Vector3(rb.centerOfMass.x, rb.centerOfMass.y, 0);
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
