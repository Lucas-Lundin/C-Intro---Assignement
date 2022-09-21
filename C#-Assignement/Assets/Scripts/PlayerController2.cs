using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    [SerializeField] private float moveForce = 15f;
    [SerializeField] private float rotateSpeed = 4f;
    [SerializeField] private float jumpForce = 150f;
    private float inputUpDown;
    private float inputRightLeft;
    private bool inputSpace;
    private Rigidbody thisRigidBody;
    private float towerAngle;
    [SerializeField] private float towerSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        thisRigidBody = GetComponent<Rigidbody>();
    }


    // Update is called once per frame
    void Update()
    {
        ProcessInput();
        RotateTowardsMouse();
    }

    private void FixedUpdate()
    {
        // Movment
        Move();
        Jump();
    }

    // Checks for inputs
    private void ProcessInput()
    {
        inputRightLeft = Input.GetAxis("Horizontal");
        inputUpDown = Input.GetAxis("Vertical");
        inputSpace = Input.GetKey(KeyCode.Space);
    }

    private void Move()
    {

        // Move forward(W) and backwards(S)      
        transform.Translate(Vector3.forward * moveForce * inputUpDown * Time.deltaTime, Space.World);

        // Move Left(A) and Right(D)  
        transform.Translate(Vector3.right * moveForce * inputRightLeft * Time.deltaTime, Space.World);

      

    }

    private void RotateTowardsMouse()
    {
        Ray rayFromMouse = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane planeAtPlayer = new Plane(Vector3.up, transform.position);
        if (planeAtPlayer.Raycast(rayFromMouse, out float distanceToHit))
        {
            Vector3 hitPosition = rayFromMouse.GetPoint(distanceToHit);
            transform.LookAt(hitPosition);
        }
    }

    private void Jump()
    { 
    if (inputSpace)
        {
            transform.Translate(Vector3.up * moveForce * Time.deltaTime, Space.World);

        }
   
    }

}
