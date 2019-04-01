using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KursunKontrol : MonoBehaviour {

    DusmanKontrol dusmanKontrol;
    Rigidbody2D fizik;

	void Start ()
    {
        dusmanKontrol = GameObject.FindGameObjectWithTag("dusmanTag").GetComponent<DusmanKontrol>();
        fizik = GetComponent<Rigidbody2D>();
        fizik.AddForce(dusmanKontrol.getYon() * 1000); //kuvvet uygulayarak ates etmesini sağlıyoruz. (1000 kursun hizi)
        /*kursunun yön bilgisini dusmankontrol scriptindeki getYon metodundan alıyoruz*/	
	}
}
