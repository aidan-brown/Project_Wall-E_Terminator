using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour {

    public GameObject bullet, barrelL, barrelR;
    public int fireRate;
    private int counter;
    private bool rightArm;

    // initialises the counter
    void Start()
    {
        counter = fireRate;
        rightArm = true;
    }


    // Update is called once per frame
    void Update ()
    {
        if (Input.GetButton("Fire1"))
        {
            if (counter >= fireRate)
            {
                GameObject clone;
                // new Vector3(transform.position.x, transform.position.y, transform.position.z);
                if (rightArm)
                {
                    clone = Instantiate(bullet, barrelR.transform.position, transform.rotation) as GameObject;
                    rightArm = false;
                }
                else
                {
                    clone = Instantiate(bullet, barrelL.transform.position, transform.rotation) as GameObject;
                    rightArm = true;
                }
                counter = 0;
            }
            else
                counter++;
        }
        else if (counter < fireRate)
            counter++;
	}
}
