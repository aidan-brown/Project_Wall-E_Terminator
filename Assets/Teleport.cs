using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {
    Collider playerCol;
    GameObject player, teleOut, camera;
    FollowPlayer follow;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        teleOut = GameObject.FindGameObjectWithTag("TOut");
        playerCol = player.GetComponent<BoxCollider>();
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        follow = camera.GetComponent<FollowPlayer>();
	}

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if(other.Equals(playerCol))
        {
            player.transform.position = teleOut.transform.position;
            follow.enabled = true;
        }
    }
}
