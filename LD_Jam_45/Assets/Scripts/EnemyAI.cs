﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
	public string enemyType = "destructable";
	public int HP = 1;
	public int AttackValue = 0;
	public float Speed = 0.0f;
	public GameObject player;

	private enum EnemyClass {destructable, monster};
	private Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(HP <= 0)
        {
        	//Play a death sound
        	Destroy(gameObject);
        }

       HandleMovement();
    }

    private void OnTriggerEnter(Collider other)
    {
    	//Collision info for all enemies
    	if(other.name == "Arms" && (enemyType == "destructable" || enemyType == "slime" || enemyType == "caterquito"))
    	{
    		HP -= 1;
 		}
    }

    private void OnCollisionEnter(Collision c)
    {
    	Collider other = c.collider;

    	if((other.name == "Head" || other.name == "Torso" || other.name == "Legs") && AttackValue > 0)
 		{
 			//Apply damage to our player (his invulnerability might resist it)
 			other.transform.parent.GetComponent<PlayerMotion>().applyDamage(AttackValue, other.name);
 		}
    }

    private void HandleMovement()
    {
    	 //Check if we should move towards player or whatever
        if(enemyType == "slime")
        {
        	//See if in range
        	if((Vector3.Distance(player.transform.position, gameObject.transform.position)) < 5)
        	{
        		//move towards player and rotate to face them
        		gameObject.transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Speed*Time.deltaTime);
        		gameObject.transform.rotation = Quaternion.LookRotation(player.transform.position-transform.position);
        	}
        }
        else if(enemyType == "caterquito")
        {
        	//See if in range
        	if((Vector3.Distance(player.transform.position, gameObject.transform.position)) < 15)
        	{
        		//Is the player looking at us? Calculate angle between his forward and the line between us
	        	float viewAngle = Vector3.Angle(player.transform.forward, player.transform.position - transform.position);

	        	if(viewAngle < 45)
	        	{
	        		//run away!
	        		gameObject.transform.position = Vector3.MoveTowards(transform.position, player.transform.position, -Speed*Time.deltaTime);
	        		gameObject.transform.rotation = Quaternion.LookRotation(transform.position-player.transform.position);
	        	}
	        	else
	        	{
	        		//move towards player and rotate to face them
	        		gameObject.transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Speed*Time.deltaTime);
	        		gameObject.transform.rotation = Quaternion.LookRotation(player.transform.position-transform.position);
        		}
        	}
        }
    }
}