using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour {
    public GameObject enemy;
    public int xBounds = 50, yBounds = 50;
	
    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            Instantiate(enemy, new Vector3(Random.Range(-(xBounds / 2), (xBounds / 2)), 2, Random.Range(-(yBounds / 2), (yBounds / 2))), Quaternion.identity);
            Instantiate(enemy, new Vector3(Random.Range(-(xBounds / 2), (xBounds / 2)), 2, Random.Range(-(yBounds / 2), (yBounds / 2))), Quaternion.identity);
            Instantiate(enemy, new Vector3(Random.Range(-(xBounds / 2), (xBounds / 2)), 2, Random.Range(-(yBounds / 2), (yBounds / 2))), Quaternion.identity);
            yield return new WaitForSecondsRealtime(5);
        }
    }

    void Start()
    {
        StartCoroutine("SpawnEnemy");
    }
}
