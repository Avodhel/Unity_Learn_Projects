using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatlamaEfektTemizle : MonoBehaviour { //Patlama sonrası arkaplanda kalan patlama efektlerini temizliyoruz.

	void Start ()
    {
        Destroy(gameObject, 3); //patlama efekti oluştuktan 3 saniye sonra silinecek.
	}
	
}
