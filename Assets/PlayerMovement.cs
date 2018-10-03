using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    int floormask;
    Rigidbody rb;
    Vector3 movement;

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
