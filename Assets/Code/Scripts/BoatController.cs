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
        rb.centerOfMass = new Vector3(0, 3f, 0);

    }
    
    void Update()
    {
        handleMove();
        handleRotation();
        
        // when boat is not moving -> motor push from the back sideway which will move the front
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
