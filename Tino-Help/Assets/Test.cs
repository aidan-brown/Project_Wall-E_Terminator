using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {
    public GameObject player;

	// Use this for initialization
	void Start () {
        Destroy(player);
	}
	
	// Update is called once per frame
	void Awake () {
        Instantiate(player, Vector3.zero, Quaternion.identity);
	}
}
