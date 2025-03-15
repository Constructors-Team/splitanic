using UnityEngine;

public class BoatController : MonoBehaviour
{
    [SerializeField]
    private float acceleration;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private float currentForce;
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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        handleMove();
        handleRotation();
        
        // try to adjust center of mass to make boat behaviour more "interresting" :) 
        rb.centerOfMass = new Vector2(0, -3f) + rb.linearVelocity * 1.4f;
        centerOfMass.transform.position = transform.TransformPoint(rb.centerOfMass);
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

        // add current force if player is not moving
        if (move.Equals(Vector3.zero))
        {
            move = Vector3.left * currentForce;
        }
        
        rb.AddForce(move, ForceMode2D.Force);
    }
}
