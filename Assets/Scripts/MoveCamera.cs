using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour {
    public string direction;
    GameObject camera, player;
    Collider playerCol;
    public float transform = .5f;

	// Use this for initialization
	void Start () {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        player = GameObject.FindGameObjectWithTag("Player");
        playerCol = player.GetComponent<BoxCollider>();
	}

    // Update is called once per frame
    public void OnTriggerEnter(Collider other)
    {
        if(other.Equals(playerCol))
        {
            if(direction == "down")
            {
                //camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y, camera.transform.position.z - 50);
                player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z - 10);
                float time = Time.timeScale;
                Time.timeScale = 0;
                IEnumerator coroutine = MoveCam(0, -transform, time);
                StartCoroutine(coroutine);
            }
            else if (direction == "up")
            {
                //camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y, camera.transform.position.z + 50);
                player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + 10);
                float time = Time.timeScale;
                Time.timeScale = 0;
                IEnumerator coroutine = MoveCam(0, transform, time);
                StartCoroutine(coroutine);
            }
            else if (direction == "left")
            {
                //camera.transform.position = new Vector3(camera.transform.position.x - 50, camera.transform.position.y, camera.transform.position.z);
                player.transform.position = new Vector3(player.transform.position.x - 10, player.transform.position.y, player.transform.position.z);
                float time = Time.timeScale;
                Time.timeScale = 0;
                IEnumerator coroutine = MoveCam(-transform, 0, time);
                StartCoroutine(coroutine);
            }
            else if (direction == "right")
            {
                //camera.transform.position = new Vector3(camera.transform.position.x + 50, camera.transform.position.y, camera.transform.position.z);
                player.transform.position = new Vector3(player.transform.position.x + 10, player.transform.position.y, player.transform.position.z);
                float time = Time.timeScale;
                Time.timeScale = 0;
                IEnumerator coroutine = MoveCam(transform, 0, time);
                StartCoroutine(coroutine);
            }
        }
    }

    IEnumerator MoveCam(float x, float y, float time)
    {
        for (double i = 0; i < 50; i += transform)
        {
            camera.transform.position = new Vector3(camera.transform.position.x + x, camera.transform.position.y, camera.transform.position.z + y);
            yield return new WaitForSecondsRealtime(Time.deltaTime);
        }
        Time.timeScale = time;
    }
}
