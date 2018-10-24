using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
	public int health;
	
	void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.name == "Bullet(Clone)")
			health--;
		if (health <= 0)
		{
			Destroy(gameObject);
		}
	}
}
