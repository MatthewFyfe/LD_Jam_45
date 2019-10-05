using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotion : MonoBehaviour
{
	public float movementSpeed = 5;
	public float rotationalSpeed = 50;

	private Rigidbody rbody;
	private float moveHorizontal, moveVertical;
	private Vector3 movement;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    	/* Do i want to use axis?
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");
        */

        //Speed modifier
    	if(Input.GetKey(KeyCode.LeftShift))
    	{
    		movementSpeed = 10;
    		rotationalSpeed = 100;
    	}
    	else
    	{
    		movementSpeed = 5;
    		rotationalSpeed = 50;
    	}

        // Handle Movement
        if(Input.GetKey(KeyCode.W))
        {
        	transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed, Space.World);
        }
        if(Input.GetKey(KeyCode.S))
        {
        	transform.Translate(Vector3.back * Time.deltaTime * movementSpeed, Space.World);
        }
        if(Input.GetKey(KeyCode.A))
        {
        	transform.Translate(Vector3.left * Time.deltaTime * movementSpeed, Space.World);
        }
        if(Input.GetKey(KeyCode.D))
        {
        	transform.Translate(Vector3.right * Time.deltaTime * movementSpeed, Space.World);
        }

         // Handle Rotation
        if(Input.GetKey(KeyCode.Q))
        {
        	transform.Rotate(0,0,Time.deltaTime*rotationalSpeed*-1);
        }
        if(Input.GetKey(KeyCode.E))
        {
        	transform.Rotate(0,0,Time.deltaTime*rotationalSpeed*1);
        }
    }
}
