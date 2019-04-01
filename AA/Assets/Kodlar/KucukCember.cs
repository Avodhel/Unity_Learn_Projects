using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KucukCember : MonoBehaviour { //İgne görünümlü nesne

    Rigidbody2D fizik;
    public float hiz;
    bool hareketKontrol;
    GameObject oyunYoneticisi; 

	void Start ()
    {
        fizik = GetComponent<Rigidbody2D>();
        oyunYoneticisi = GameObject.FindGameObjectWithTag("OyunYoneticisiTag");
    }

	void FixedUpdate () //Fizik olaylarında fixedupdate metodunu kullanırız.
    {
        if (!hareketKontrol)
        {
            fizik.MovePosition(fizik.position + Vector2.up * hiz * Time.deltaTime); // igneli kucuk cemberin yukarı dogru hareket etmesi.
        }
	}

    void OnTriggerEnter2D(Collider2D col) //"col" kucuk cemberin temas ettiği objeyi temsil ediyor.
    {
        if (col.tag == "DonenCemberTag")
        {
            transform.SetParent(col.transform); //kucuk cember, donen cembere carptıginda, donen cember onun parenti oluyor bu sayede donen cemberle birlikte donuyor.
            hareketKontrol = true; // büyük çembere çarptığında durması için
        }
        if (col.tag == "KucukCemberTag") //kendisi gibi bir kucuk cembere temas ederse.
        {
            oyunYoneticisi.GetComponent<OyunYoneticisi>().oyunBitti(); // temas gerçekleştiğinde oyun yoneticisi sınıfındaki oyunBitti() metodu çağrılacak ve oyun bitti yazacak.
        }
    }
}
