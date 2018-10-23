using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour {

    public GameObject obj;
    public int cameraHeight;

	// Update is called once per frame
	void Update ()
    {
        transform.position = new Vector3(obj.transform.position.x, cameraHeight, obj.transform.position.z);
	}
}
