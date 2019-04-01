using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidHareket : MonoBehaviour {

    Rigidbody fizik;
    public float hiz;

	void Start ()
    {
        fizik = GetComponent<Rigidbody>();
        fizik.velocity = transform.forward * -hiz; //geri gitmesini istediğim için -hiz ile çarptım.
	}

}
