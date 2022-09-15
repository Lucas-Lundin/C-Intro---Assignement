using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveForce = 15f;
    [SerializeField] private float rotateSpeed = 4f;
    [SerializeField] private float jumpForce = 150f;
    private float inputUpDown;
    private float inputRightLeft;
    private bool inputSpace;
    private Rigidbody thisRigidBody;

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

        // Movment Forward and Backwards
        thisRigidBody.AddForce(transform.forward * inputUpDown * moveForce);
        // Rotation Right and Left
        transform.Rotate(Vector3.up, rotateSpeed * inputRightLeft);


    }
    private void Jump()
    { 
    if (inputSpace)
        {
            thisRigidBody.AddForce(transform.up * moveForce);
        }
   
    }

}
