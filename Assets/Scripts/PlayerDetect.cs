using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetect : MonoBehaviour {
    public GameObject enemy;
    Vector3 randomPos;
    Collider playerCol, trigger;

	// Use this for initialization
	void Start () {
        playerCol = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider>();
        trigger = GetComponent<BoxCollider>();
	}

    public void OnTriggerEnter(Collider other)
    {
        if(other.Equals(playerCol))
        {
            SpawnEnemies(transform.position, (int)Random.Range(5, 25));
        }
        Destroy(trigger);
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.Equals(playerCol))
        {
            Destroy(trigger);
        }
    }

    public void SpawnEnemies(Vector3 pos, int numOfEnemies)
    {
        for (int i = 0; i < numOfEnemies; i++)
        {
            randomPos = new Vector3(Random.Range(pos.x - 20, pos.x + 20), 2.5f, Random.Range(pos.z - 20, pos.z + 20));
            if (!Physics.CheckSphere(randomPos, .001f))
            {
                Instantiate(enemy, randomPos, Quaternion.identity);
            }
            else
            {
                i--;
            }
        }
    }
}
