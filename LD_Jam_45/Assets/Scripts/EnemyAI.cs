using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
	public string enemyType = "destructable";
	public int HP = 1;
	public int AttackValue = 0;
	public float Speed = 1.0f;
	public GameObject player;

	private enum EnemyClass {destructable, monster};

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

        //Check if we should move towards player or whatever
        if(enemyType == "monster")
        {
        	//See if in range
        	if((Vector3.Distance(player.transform.position, gameObject.transform.position)) < 5)
        	{
        		//move towards player
        		gameObject.transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Speed*Time.deltaTime);
        	}
        }
    }

    private void OnTriggerEnter(Collider other)
    {
    	//Collision info for all enemies
    	if(other.name == "Arms" && (enemyType == "destructable" || enemyType == "monster"))
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
}
