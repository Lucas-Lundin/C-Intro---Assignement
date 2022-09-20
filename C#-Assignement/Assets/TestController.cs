using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TestController : MonoBehaviour
{
    private float moveSpeed = 10f;
    private float rotateSpeed = 150f;
    

    
    void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed * Input.GetAxis("Vertical") * Time.deltaTime, Space.Self);

        
        transform.Rotate(Vector3.up * rotateSpeed * Input.GetAxis("Horizontal") * Time.deltaTime);
        
        // För 90-graders snappy svängar: 
        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.Rotate(Vector3.up * 90);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.Rotate(Vector3.up * -90);
        }




    }
}
