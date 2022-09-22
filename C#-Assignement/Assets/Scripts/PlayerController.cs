using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    private Rigidbody thisRigidBody;
    private float moveForce = 10f;
    private float jumpForce = 150f;
    private float inputUpDown;
    private float inputRightLeft;
    private bool inputSpace;
    private bool inputMouseLeftPress;
    private bool inputMouseLeftDown;
    private bool inputMouseLeftUp;
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private GameObject bomboSpawnPosition;
    

    private Vector3 mouseSubTargetPosition;
    private Vector3 mouseTargetPosition;



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
        Jump();
        Move();
        ThrowBomb();
    }

    private void FixedUpdate()
    {
        
        

    }

    // Checks for inputs
    private void ProcessInput()
    {
        
        if (gameObject == TurnManager.GetInstance().GetCurrentObj())
        {
            inputRightLeft = Input.GetAxis("Horizontal");
            inputUpDown = Input.GetAxis("Vertical");
            inputSpace = Input.GetKeyDown(KeyCode.Space);
            inputMouseLeftPress = Input.GetMouseButton(0);
            inputMouseLeftDown = Input.GetMouseButtonDown(0);
            inputMouseLeftUp = Input.GetMouseButtonUp(0);
        }
        else
        {
            inputRightLeft = 0f;
            inputUpDown = 0f;
            inputSpace = false;
            inputMouseLeftPress = false;
            inputMouseLeftDown = false;
            inputMouseLeftUp = false;
        }
    }

    private void Move()
    {
        //Debug.Log("MouseX: " + Mathf.Abs(mouseSubTargetPosition.x - transform.position.x));
        if (gameObject == TurnManager.GetInstance().GetCurrentObj())
        {
            if (Mathf.Abs(mouseSubTargetPosition.x - transform.position.x) >= 0.1 )
            {
                transform.Translate(Vector3.forward * moveForce * inputUpDown * Time.deltaTime);    // Transalte forward/backwards

                // Move forward(W) and backwards(S)      
                //thisRigidBody.AddForce(transform.forward * moveForce * inputUpDown * Time.deltaTime * boostMovment);           //Force forward/backwards
                //thisRigidBody.AddForce(0, 0, moveForce * inputUpDown * boostZDirctionChange * Time.deltaTime * boostMovment); //Force Sotuh/North
                // transform.Translate(Vector3.forward * moveForce * inputUpDown * Time.deltaTime, Space.World);    // Transalte Sotuh/North


                // Move Left(A) and Right(D)  
                //thisRigidBody.AddForce(transform.right * moveForce * inputRightLeft * Time.deltaTime * boostMovment);                 // Force right/left
                //thisRigidBody.AddForce(moveForce * inputRightLeft * boostXDirctionChange * Time.deltaTime * boostMovment, 0, 0);    // Force West/east
                //transform.Translate(Vector3.right * moveForce * inputRightLeft * Time.deltaTime, Space.World);            // Transalte West/east
                //transform.Translate(Vector3.right * moveForce * inputRightLeft * Time.deltaTime);            // Transalte right/left
            }
        }
    }

    private void Jump()
    {
        if (inputSpace)
        {
            //transform.Translate(Vector3.up * jumpForce * Time.deltaTime, Space.World);
            thisRigidBody.AddForce(transform.up * jumpForce);
        }

    }

    private void ThrowBomb()
    {
        if (inputMouseLeftDown)
        {
            GameObject bomb;
           
            bomb = Instantiate(bombPrefab, bomboSpawnPosition.transform.position, transform.rotation); //+ new Vector3(0f, 1f, 1f)
            bomb.GetComponent<Rigidbody>().AddForce(transform.forward * 1000);
            bomb.GetComponent<Rigidbody>().AddForce(transform.up * 100);

        }
    }


    private void RotateTowardsMouse()
    {
        if (gameObject == TurnManager.GetInstance().GetCurrentObj() || gameObject == TurnManager.GetInstance().IfNoonesTurnGetNextPlayerObj())
        { 
            Ray rayFromMouse = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane planeAtPlayer = new Plane(Vector3.up, transform.position);

            if (planeAtPlayer.Raycast(rayFromMouse, out float distanceToHit))
            {
                mouseTargetPosition = rayFromMouse.GetPoint(distanceToHit);
                mouseSubTargetPosition = Vector3.Lerp(mouseSubTargetPosition, mouseTargetPosition, 0.1f);
                transform.LookAt(mouseSubTargetPosition);
            }
        }
    }



}
