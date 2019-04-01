using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KameraKontrol : MonoBehaviour {

    public GameObject top; //topumuzun lokasyon bilgisini öğrenmek için
    Vector3 aradakiMesafe;

	void Start () {
        aradakiMesafe = transform.position - top.transform.position; // kamera ile top arasındaki mesafe
	}

	void LateUpdate () {
        transform.position = top.transform.position + aradakiMesafe; // top ile kamera arasına, aradaki mesafe kadar fark bırakıyoruz.
	}
}
