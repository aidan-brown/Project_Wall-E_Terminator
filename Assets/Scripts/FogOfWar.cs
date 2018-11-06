using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWar : MonoBehaviour {
    MeshRenderer rend;
    GameObject player;
    Color clear;

	// Use this for initialization
	void Start () {
        rend = GetComponent<MeshRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
        clear.a = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(player.transform.position.x >= transform.position.x - 25 && player.transform.position.x <= transform.position.x + 25 && player.transform.position.z <= transform.position.z + 25 && player.transform.position.z >= transform.position.z - 25)
        {
            rend.enabled = false;
        }
        else
        {
            rend.enabled = true;
        }
	}
}
