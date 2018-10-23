using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBitch : MonoBehaviour {

    private void Awake()
    {
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(4);
    }

    void Update () {
        transform.Translate(Time.deltaTime, 0, 0);
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject player = GameObject.FindWithTag("Player");
            for (int i = -1; i < 2; i+=2)
            {
                GameObject newPlayer = Instantiate(player, player.transform.position, Quaternion.Euler(0, 0, Random.Range(0f, 359.0f)));
                newPlayer.transform.localScale = new Vector3(.5f, .5f, .5f);
            }
            Destroy(GameObject.FindGameObjectWithTag("Player"));
        }
    }
}
