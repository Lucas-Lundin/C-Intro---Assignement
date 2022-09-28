using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
    private bool inputMouseRightPress;
    private bool inputMouseRightDown;
    private bool inputMouseRightUp;
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private GameObject bombReversePrefab;
    [SerializeField] private GameObject bomboSpawnPosition;
    public bool uncontrolablePostDeath = false;
    private float uncontrolablePostDeathSet = 1.0f;
    private float uncontrolablePostDeathCurrenttime;

    private Vector3 mouseSubTargetPosition;
    private Vector3 mouseTargetPosition;
    private bool allowedToJump = true;
    public int bulletsBlack;
    public int bulletsWhite;

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
        ThrowBombReverse();
        UncontrolablePostDeathCountDown();
        CheckIfAllowedToJump();
        Debug.Log(gameObject.name + " bulletsWhite: " + bulletsWhite);
    }

    private void FixedUpdate()
    {
        
        

    }

    // Checks for inputs
    private void ProcessInput()
    {
        
        if (gameObject == TurnManager.GetInstance().GetCurrentObj() && (uncontrolablePostDeath == false))
        {
            inputRightLeft = Input.GetAxis("Horizontal");
            inputUpDown = Input.GetAxis("Vertical");
            inputSpace = Input.GetKeyDown(KeyCode.Space);
            inputMouseLeftPress = Input.GetMouseButton(0);
            inputMouseLeftDown = Input.GetMouseButtonDown(0);
            inputMouseLeftUp = Input.GetMouseButtonUp(0);
            inputMouseRightPress = Input.GetMouseButton(1);
            inputMouseRightDown = Input.GetMouseButtonDown(1);
            inputMouseRightUp = Input.GetMouseButtonUp(1);
        }
        else
        {
            inputRightLeft = 0f;
            inputUpDown = 0f;
            inputSpace = false;
            inputMouseLeftPress = false;
            inputMouseLeftDown = false;
            inputMouseLeftUp = false;
            inputMouseRightPress = false;
            inputMouseRightDown = false;
            inputMouseRightUp = false;
        }
    }

    private void UncontrolablePostDeathCountDown()
    {
        uncontrolablePostDeathCurrenttime -= 1 * Time.deltaTime;
        if (uncontrolablePostDeathCurrenttime <= 0)
        {
            uncontrolablePostDeath = false;
        }
    }

    public void UncontrolablePostDeathSet()
    {
        uncontrolablePostDeathCurrenttime = uncontrolablePostDeathSet;
        uncontrolablePostDeath = true;
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
        if (inputSpace && allowedToJump)
        {
            //transform.Translate(Vector3.up * jumpForce * Time.deltaTime, Space.World);
            thisRigidBody.AddForce(transform.up * jumpForce);
        }

    }

    /*
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
    */
    private float channledForceMultipler = 0f;
    [SerializeField] private Image ui_ChannelForceLeftClick;

    private void ThrowBomb()
    {
        if (bulletsBlack >= 1)
        { 
            if (inputMouseLeftPress)
            {
                
                channledForceMultipler += 3 * Time.deltaTime;
                channledForceMultipler = Mathf.Clamp(channledForceMultipler, 0.2f, 1f);
            }
            if (inputMouseLeftUp || (inputMouseLeftPress && TurnManager.GetInstance().turnTimeCurrentLeft <= 0.05f))
            {
                bulletsBlack -= 1;
                GameObject bomb;

                bomb = Instantiate(bombPrefab, bomboSpawnPosition.transform.position, transform.rotation); //+ new Vector3(0f, 1f, 1f)
                bomb.GetComponent<Rigidbody>().AddForce(transform.forward * 1000 * channledForceMultipler);
                bomb.GetComponent<Rigidbody>().AddForce(transform.up * 100 * channledForceMultipler);
                channledForceMultipler = 0f;
            }
            ui_ChannelForceLeftClick.fillAmount = channledForceMultipler;
        }
    }

    private void ThrowBombReverse()
    {
        if (bulletsWhite >= 1)
        {
            if (inputMouseRightPress)
            {
                Debug.Log("Rightclick!");
                channledForceMultipler += 3 * Time.deltaTime;
                channledForceMultipler = Mathf.Clamp(channledForceMultipler, 0.2f, 1f);
            }
            if (inputMouseRightUp || (inputMouseRightPress && TurnManager.GetInstance().turnTimeCurrentLeft <= 0.05f))
            {
                bulletsWhite -= 1;
                GameObject bomb;

                bomb = Instantiate(bombReversePrefab, bomboSpawnPosition.transform.position, transform.rotation); //+ new Vector3(0f, 1f, 1f)
                bomb.GetComponent<Rigidbody>().AddForce(transform.forward * 1000 * channledForceMultipler);
                bomb.GetComponent<Rigidbody>().AddForce(transform.up * 100 * channledForceMultipler);
                channledForceMultipler = 0f;
            }
            ui_ChannelForceLeftClick.fillAmount = channledForceMultipler;
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


    private void CheckIfAllowedToJump()
    {
        if (thisRigidBody.velocity.y < 0.1f && thisRigidBody.velocity.y > -0.1f)
        {
            allowedToJump = true;
        }
        else 
        {
            allowedToJump = false;
        }
    }


}
