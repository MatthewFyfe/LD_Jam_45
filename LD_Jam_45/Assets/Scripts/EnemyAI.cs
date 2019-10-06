using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
	public string enemyType = "destructable";
	public int HP = 1;
	public int AttackValue = 0;
	public float Speed = 0.0f;
	public GameObject player;
    public GameObject mainCamera;
    public AudioClip deathSFX;

	private enum EnemyClass {destructable, monster};
	private Vector3 target;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = mainCamera.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(HP <= 0)
        {
        	//Play a death sound
            if(enemyType == "fishermole")
            {
                audioSource.PlayOneShot(deathSFX, 2.0F);
            }
            else
            {
                audioSource.PlayOneShot(deathSFX, 5.0F);
            }

            
        	Destroy(gameObject);
        }

       HandleMovement();
    }

    private void OnTriggerEnter(Collider other)
    {
    	//Collision info for all enemies
    	if(other.name == "Arms" && (enemyType == "destructable" || enemyType == "slime" || enemyType == "caterquito" || enemyType == "badgermoth" || enemyType == "fishermole"))
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

 		//Check if we hit terrain as badgermoth
 		if(enemyType == "badgermoth" && other.tag == "Terrain")
 		{
 			transform.position += -transform.forward;
 			float myRandom = Random.Range(0.0f,1.0f);
 			
 			if(myRandom<0.25)
 			{
 				transform.localEulerAngles = new Vector3(0,-180,0);	
 			}
 			else if(myRandom<0.5)
 			{
 				transform.localEulerAngles = new Vector3(0,0,0);
 			}
 			else if(myRandom<0.75)
 			{
 				transform.localEulerAngles = new Vector3(0,90,0);
 			}
 			else
 			{
 				transform.localEulerAngles = new Vector3(0,-90,0);
 			}
 			//gameObject.transform.rotation = Random.range(0,1);
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
        else if(enemyType == "badgermoth")
        {
        	//go forward until we crash, then back up a bit and go in a random cardinal direction
        	transform.position += transform.forward * Time.deltaTime * Speed;
        }
        else if(enemyType == "fishermole")
        {
            //constantly rotate to try and hit player
            transform.Rotate(5, 0, 0);

            //move while head it down and we can "see" player
            RaycastHit hit;
            Vector3 PlayerPosNoHeight = player.transform.position;
            PlayerPosNoHeight.y = 0;

            if(Physics.Raycast(transform.position+(transform.up * 2), player.transform.position, out hit, Mathf.Infinity))
            {
                
            }
            else
            {
                //Debug.DrawLine(transform.position+(transform.up * 2), player.transform.position, Color.yellow);
                gameObject.transform.position = Vector3.MoveTowards(transform.position, PlayerPosNoHeight, Speed*Time.deltaTime);
            }
        }
    }
}
