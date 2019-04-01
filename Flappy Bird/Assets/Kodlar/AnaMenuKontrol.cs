using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AnaMenuKontrol : MonoBehaviour {

    public Text puanText;
    public Text enYuksekPuanText;

	void Start ()
    {
        int puan = PlayerPrefs.GetInt("puanKayit"); //puan bilgisini alıyorum
        puanText.text = "Puanınız: " + puan;

        int enYuksekPuan = PlayerPrefs.GetInt("enYuksekPuanKayit"); // en yüksek puan bilgisini alıyorum.
        enYuksekPuanText.text = "En Yüksek Puan: " + enYuksekPuan;

        /*reklam gösterme*/
        int reklamSayaci = PlayerPrefs.GetInt("reklamSayaciKayit"); //reklam sayaci kaydımı aldım (buna göre reklam göstericem.)
        if (reklamSayaci == 3) // oyuna 3 kere yeniden başlandığında
        {
            GameObject.FindGameObjectWithTag("ReklamTag").GetComponent<ReklamKontrol>().reklamiGoster(); //reklamı oynat
            PlayerPrefs.SetInt("reklamSayaciKayit", 0); //reklam sayaci kaydımı sıfırlıyorum.
        }
	}
	
	void Update ()
    {
		
	}

    public void oyunaBasla() // eğer public demezsek arayüzde butona tanıtırken gözükmez
    {
        SceneManager.LoadScene("1"); // "1" isimli oyun sahnesini getiriyoruz.
    }

    public void oyundanCik()
    {
        Application.Quit(); // oyundan çıkıyoruz.
    }
}
