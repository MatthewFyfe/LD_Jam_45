using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
	public string enemyType = "destructable";
	public int HP = 1;
	public int AttackValue = 0;

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
    }

    private void OnTriggerEnter(Collider other)
    {
    	//Collision info for all enemies
    	if(other.name == "Arms" && enemyType == "destructable")
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
