using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopKontrol : MonoBehaviour {

    Rigidbody fizik;
    public int hiz;
    int sayac;
    public int toplamPuan;
    public Text skorText;
    public Text oyunBittiText;

	void Start () {
        fizik = GetComponent<Rigidbody>();
	}

	void FixedUpdate () {
        float yatay = Input.GetAxisRaw("Horizontal"); //x ekseni
        float dikey = Input.GetAxisRaw("Vertical");   //z ekseni

        Vector3 vec = new Vector3(yatay, 0, dikey); // y eksenine sıfır atadık çünkü top yukarı hareket etmeyecek.

        fizik.AddForce(vec*hiz);

        //Debug.Log("yatay:" + yatay + " dikey:" + dikey);
	}

    void OnTriggerEnter(Collider other) //Top bir objeye temas ettiğinde bu metot çalışır
    {
        if (other.gameObject.tag == "Puan") //Sadece puanlara temas ettiğimde yok olması için.
        {
            other.gameObject.SetActive(false); // Temas edilen objeyi kapat. (yok etmekten farklı)
            sayac++;
            skorText.text = "Skor: " + sayac; //skoru gösterme
            //Destroy(other.gameObject);//Temas edilen objeyi yok et.
        }

        if (sayac == toplamPuan) // oyun bitti
        {
            oyunBittiText.text = "Tebrikler Oyunu Bitirdiniz.";
        }
    }
}
