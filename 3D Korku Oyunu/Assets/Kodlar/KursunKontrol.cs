using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KursunKontrol : MonoBehaviour {

	void Start ()
    {
        KarakterKontrol karakter = GameObject.FindGameObjectWithTag("Player").GetComponent<KarakterKontrol>();
        GetComponent<Rigidbody>().AddForce(karakter.pozisyonAl() * 3000); //pozisyon al fonksiyonu ile kursunumuzun yonunu belirledik
        transform.rotation = Quaternion.LookRotation(karakter.pozisyonAl()); // kursunun namludan yamuk çıkmaması için
        Destroy(gameObject, 5); // namludan çıktıktan 5 saniye sonra kursunu yok et
	}

}
