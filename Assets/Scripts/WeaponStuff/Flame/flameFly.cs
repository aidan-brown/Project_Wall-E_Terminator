using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flameFly : MonoBehaviour
{
	public Rigidbody rb;
	public float speed;

	private float scale;
	private Vector3 Scale { get { return new Vector3(scale, scale, scale); } }

	void Start ()
	{
		Destroy(gameObject, 1f);
		rb.velocity = gameObject.transform.forward * speed;
	}

	void Update()
	{
		scale += 0.01f;
		transform.localScale = Scale;
	}

	void OnCollisionEnter(Collision collision)
	{
		if (!(collision.collider.name == "Character") && !(collision.collider.name == "Flame(Clone)"))
			Destroy(gameObject);
	}
}
