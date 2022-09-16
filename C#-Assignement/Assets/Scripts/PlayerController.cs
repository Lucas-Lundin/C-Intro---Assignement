using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 15f;
    [SerializeField] private float boostDirctionChange = 15f;
    [SerializeField] private string horizontalMoveKey = "Horizontal";
    [SerializeField] private string verticalMoveKey = "Vertical";
    private float xInput;
    private float zInput;
    public Rigidbody thisRigidBody;

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
    }

    // Checks for inputs
    private void ProcessInput()
    {
        xInput = Input.GetAxis(horizontalMoveKey);
        zInput = Input.GetAxis(verticalMoveKey);
    }

    private void Move()
    {
        Debug.Log("X Vel: " + thisRigidBody.velocity.x + " xInput: " + xInput);

        // Boosting force when changing Horizontal direction
        float boostXDirctionChange = 1f;
        if ((thisRigidBody.velocity.x < 0 && xInput > 0) || (thisRigidBody.velocity.x > 0 && xInput < 0))
        {
            boostXDirctionChange = boostDirctionChange;
        }

        // Boosting force when changing Vertical direction
        float boostZDirctionChange = 1f;
        if ((thisRigidBody.velocity.z < 0 && zInput > 0) || (thisRigidBody.velocity.z > 0 && zInput < 0))
        {
            boostZDirctionChange = boostDirctionChange;
        }


        thisRigidBody.AddForce(new Vector3(xInput * boostXDirctionChange, 0f, zInput * boostZDirctionChange) * moveSpeed);


        //thisRigidBody.AddForce(new Vector3(xInput, 0f, zInput) * moveSpeed);
    }
      

}
