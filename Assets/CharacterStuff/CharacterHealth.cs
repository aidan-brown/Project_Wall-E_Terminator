using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterHealth : MonoBehaviour
{
	// fields
	public int health;
	public int maxHealth;
	public Text textBox;

	// desplays health
	private void Update()
	{
		textBox.text = ("Health: " + health);
	}

	// checks for damage
	void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.tag == "Monster")
		{
			health--;
			if (health <= 0)
				Destroy(gameObject);
		}
	}
}
