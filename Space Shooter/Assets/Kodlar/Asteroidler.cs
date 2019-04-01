using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroidler : MonoBehaviour {

    Rigidbody fizik;
    public float hiz;

	void Start()
    {
        fizik = GetComponent<Rigidbody>();
        fizik.angularVelocity = Random.insideUnitSphere * hiz; //Nesneye her başlangıçta random bir dönüş kazandırıyor.
    }
}
