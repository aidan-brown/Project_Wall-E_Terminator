using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
	public int health;
	
	void OnCollisionEnter(Collision collision)
	{
		
		if (collision.transform.CompareTag("Bullet"))
			health--;
		if (health <= 0)
		{
			Destroy(gameObject);
		}
	}
}
