using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoppingEnemyAttack : MonoBehaviour
{
	public int jumpCooldown;
	public GameObject target;
	private int counter;
	private Rigidbody rb;

	// Use this for initialization
	void Start ()
	{
		target = GameObject.FindGameObjectWithTag("Player");
		counter = 0;
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		Turn();
		counter++;
		if (counter == 100)
			rb.velocity = new Vector3(0f, 0f, 0f);
		if (counter >= jumpCooldown)
		{
			Jump();
			counter = 0;

		}
	}

	void Jump()
	{
		
		rb.velocity = (transform.forward * 5 + transform.up * 3);
		
	}

	void Turn()
	{
		transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
	}
}
