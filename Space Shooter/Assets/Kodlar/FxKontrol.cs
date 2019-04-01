using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxKontrol : MonoBehaviour {

    Rigidbody fizik;
    public float hiz;

    void Start ()
    {
        fizik = GetComponent<Rigidbody>();
        fizik.velocity = transform.forward * hiz; //lazer transform'un ilerisinde hareket etsin istiyoruz "forward" bu işe yarıyor. 
                                                  //Start metodunda yazdık çünkü bir kere hızını değiştireceğiz ve hep o hızda gidecek.
        //Debug.Log(fizik.velocity);
        //Debug.Log(transform.forward);
	}
}
