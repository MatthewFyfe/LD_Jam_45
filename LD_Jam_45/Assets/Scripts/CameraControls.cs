using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
	Quaternion rotation;
	public GameObject player;

	public float cameraOffsetHeight = 5.0f;
	public float cameraOffsetBack = 3.5f;

	private Vector3 position;
    
    void Awake()
    {
    	
    }

    void LateUpdate()
    {
    	//Update camera to player position (and offset)
    	position = player.transform.position;
    	position.z -= cameraOffsetBack;
    	position.y += cameraOffsetHeight;
    	transform.position = position;
    }
}
