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
    /*void Update ()
    {
        transform.translate(vector3.forward * speed * time.deltatime);
        
    }*/

    // destroys the bullet if it hits anything
    void OnCollisionEnter(Collision collision)
    {
        if (!(collision.collider.name == "Character") && !(collision.collider.name == "Bullet(Clone)"))
            Destroy(gameObject);
	}
}
