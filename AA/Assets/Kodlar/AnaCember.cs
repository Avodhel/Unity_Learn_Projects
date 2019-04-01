using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnaCember : MonoBehaviour {

    public GameObject kucukCember;
    GameObject oyunYoneticisi;

	void Start () {
        oyunYoneticisi = GameObject.FindGameObjectWithTag("OyunYoneticisiTag");
	}
	
	void Update ()
    {
        if (Input.GetButtonDown("Fire1")) //mouse'a sol tıklandığında
        {
            kucukCemberOlustur();
        }
	}

    void kucukCemberOlustur()
    {
        Instantiate(kucukCember, transform.position, transform.rotation); //kucuk cember oluşturuyoruz.
        oyunYoneticisi.GetComponent<OyunYoneticisi>().kalanIgneSayisi();
    }
}
