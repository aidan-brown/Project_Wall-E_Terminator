using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFly : MonoBehaviour {

    public float speed;
    public float range;

    void Start()
    {
        Destroy(gameObject, range);
    }

    // Update is called once per frame
    void Update ()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        
    }

    // destroys the bullet if it hits anything
    void OnCollisionEnter(Collision collision)
    {
        if (!(collision.collider.name == "Character") && !(collision.collider.name == "Bullet(Clone)"))
            Destroy(gameObject);    
    }
}
