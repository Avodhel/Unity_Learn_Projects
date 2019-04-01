using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArkaplanKaydirma : MonoBehaviour { //!!önemli: arkaplan kaydirma için arayüzde arkaplani duplicate edip arkaplana ekledik. (parent-child hiyerarşisi)

    public float kaymaHizi; //arkaplanın z eksenindeki kayma hızı
    public float tileSizeZ; // z ekseninde ne kadar birimlik bir alanda kayma işlemi gerçekleşecek.
    private Vector3 baslangicPoz; //arkaplanımın başlangıç pozisyonu

	void Start ()
    {
        baslangicPoz = transform.position; //arkaplanımızın pozisyonu
	}
	
	void Update ()
    {
        float yeniPoz = Mathf.Repeat(Time.time * kaymaHizi, tileSizeZ); //yeni pozisyonun belirlenmesi
        transform.position = baslangicPoz + Vector3.forward * yeniPoz; //arkaplanımızın pozisyonun güncellenmesi
	}
}
