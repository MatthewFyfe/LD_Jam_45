using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotion : MonoBehaviour
{
	public float movementSpeed = 5;
	private Rigidbody rbody;
	private GameObject myArms;
	private float swordTimer;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        myArms = transform.Find("Arms").gameObject;
        myArms.SetActive(false);
        swordTimer = 0f;
    }

    // Update is called once per frame (while time is positive)
    void FixedUpdate()
    {
    	HandleSprinting();
        HandleMovementInput();
        HandleAction();
    }

    //Take care of sprinting and manage stamina / whatever
    private void HandleSprinting()
    {
    	//Speed modifier
    	if(Input.GetKey(KeyCode.LeftShift))
    	{
    		movementSpeed = 10;
    	}
    	else
    	{
    		movementSpeed = 5;
    	}
    }

    //Take care of player moving around world space
    private void HandleMovementInput()
    {
    	// Handle Movement (and rotation)
        if(Input.GetKey(KeyCode.W))
        {
        	transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed, Space.World);
        	transform.localEulerAngles = new Vector3(0,-180,0);
        }
        if(Input.GetKey(KeyCode.S))
        {
        	transform.Translate(Vector3.back * Time.deltaTime * movementSpeed, Space.World);
        	transform.localEulerAngles = new Vector3(0,0,0);
        }
        if(Input.GetKey(KeyCode.A))
        {
        	transform.Translate(Vector3.left * Time.deltaTime * movementSpeed, Space.World);
        	transform.localEulerAngles = new Vector3(0,90,0);
        }
        if(Input.GetKey(KeyCode.D))
        {
        	transform.Translate(Vector3.right * Time.deltaTime * movementSpeed, Space.World);
        	transform.localEulerAngles = new Vector3(0,-90,0);
        }
    }

    //Take care of unique actions such as attacking
    private void HandleAction()
    {
    	//Take care of SWORD action
    	if(Input.GetKey(KeyCode.Space) && swordTimer <= 0)
    	{
    		myArms.SetActive(true);
    		swordTimer = 0.5f;
    	}

    	if(swordTimer > 0)
    	{
    		swordTimer -= Time.deltaTime;
    	}
    	else
    	{
    		myArms.SetActive(false);
    	}
    }
}
