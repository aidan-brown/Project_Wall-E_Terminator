using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFly : MonoBehaviour {

    public float speed;
    public float range;
	public Rigidbody rb;

    void Start()
    {
        Destroy(gameObject, range);
		rb.velocity = gameObject.transform.forward * speed;
	}

    // Update is called once per frame
    void Update ()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        
    }

    // destroys the bullet if it hits anything
    void OnCollisionEnter(Collision collision)
    {
        if (!(collision.collider.tag == "Player") && !(collision.collider.tag == "Bullet"))
            Destroy(gameObject);
	}
}
