using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {

    [SerializeField]
    private GameObject[] spicyObjects;

	void Start ()
    {
        spawnRandom();
	}

    public void spawnRandom()
    {
        int index = Random.Range(0, spicyObjects.Length);
        Instantiate(spicyObjects[index], transform.position, Quaternion.identity);
    }
}
