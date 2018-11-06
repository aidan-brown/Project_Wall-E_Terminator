using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    int floormask;
    Rigidbody rb;
    Vector3 movement;
	public bool controller;

    // IMPORTANT!!!!!
    // Camera must be set to "Main Camera"
    // Must be a "Turner" plane under the player

    // initializes rb
    private void Start()
    {
        floormask = LayerMask.GetMask("Turner");
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        movePlayer();
		if (controller)
			faceController();
		else
			faceMouse();
	}

    // moves the player
    void movePlayer()
    {
        float x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;
        float z = Input.GetAxisRaw("Vertical") * Time.deltaTime * speed;

        movement.Set(x, 0f, z);
        
        movement = movement.normalized * speed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);
    }

	// makes the player use controller rightstick for turning
	void faceController()
	{
		float x = Input.GetAxisRaw("Horisontal2");
		float z = Input.GetAxisRaw("Verticle2");
		
		if (x != 0.0f || z != 0.0f)
		{
			float angle = Mathf.Atan2(z, x) * Mathf.Rad2Deg;

			transform.rotation = Quaternion.Euler(0f, angle, 0f);
		}
		//Quaternion newRotation = Quaternion.LookRotation(new Vector3(x, 0, z));

		//rb.MoveRotation(newRotation);
	}

    // turns the player to face the mouse
    void faceMouse()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit floorHit;
        
        if (Physics.Raycast(camRay, out floorHit, 100f, floormask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;

            playerToMouse.y = 0f;
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

            rb.MoveRotation(newRotation);
        }
    }
}
