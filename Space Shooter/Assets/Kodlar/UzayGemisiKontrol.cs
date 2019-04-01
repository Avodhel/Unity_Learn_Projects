using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput; //mobil kontrol için
using UnityEngine.EventSystems;

public class UzayGemisiKontrol : MonoBehaviour
{

    Rigidbody fizik;
    float yatay = 0f;
    float dikey = 0f;
    Vector3 vec;
    public int hiz;
    public float maxX;
    public float minX;
    public float maxZ;
    public float minZ;
    public int egim;
    float atesSuresi = 0;
    public float ateslemeSuresi;
    public GameObject lazer;
    public Transform lazerCikisYeri;
    AudioSource audioS;

	void Start ()
    {
        fizik = GetComponent<Rigidbody>();
        audioS = GetComponent<AudioSource>();
	}

    void Update()//update her frame'de çalışır, tıklandığında lazer ateşleneceği için update metoduna yazıyoruz.
    { 
        //Debug.Log(Time.time); //oyunda geçen süre

        //if (CrossPlatformInputManager.GetButton("Jump") && Time.time > atesSuresi) // mouse'a tıklandığında ateş edilmesini sağlar. (Edit->Project Settings->Input)
        //{
        //    atesSuresi = Time.time + ateslemeSuresi; // sürekli ateş edilememesini sağladık. (ateslemeGecenSure'ye göre bir ates edecek.)
        //    //Debug.Log("ateş edildi");
        //    Instantiate(lazer, lazerCikisYeri.position, Quaternion.identity); //bu metot ile ateş etme işlemi sırasında oluşacak obje, posizyonu ve rotasyonu belirlenir.       
        //    audioS.Play(); //lazer ateşleme sesi
        //}
    }

    void FixedUpdate()
    {
        float yatay = CrossPlatformInputManager.GetAxis("Horizontal");
        float dikey = CrossPlatformInputManager.GetAxis("Vertical");

        Vector3 vec = new Vector3(yatay, 0, dikey);

        fizik.velocity = vec * hiz; //addforce kullanmadım çünkü nesneye kuvvet uygulamak değil sadece hareket ettirmek istiyorum.

        fizik.position = new Vector3( // Uzay gemimizin ekranın dışına çıkmaması için sınır koordinatlarını belirliyoruz.
            Mathf.Clamp(fizik.position.x, minX, maxX),
            0.0f,
            Mathf.Clamp(fizik.position.z, minZ, maxZ)
            );

        fizik.rotation = Quaternion.Euler(0, 0, fizik.velocity.x * -egim); //gemi sağa veya sola giderken eğim kazandırıyoruz.
    }

    //public void OnPointerClick(PointerEventData pointerEventData)
    //{
    //    Debug.Log("uzay gemisine tıklandı");
    //}

    void OnMouseUpAsButton() // uzay gemisine tıklandığından çalışır
    {
        if (Time.time > atesSuresi) // mouse'a tıklandığında ateş edilmesini sağlar. (Edit->Project Settings->Input)
        {
            atesSuresi = Time.time + ateslemeSuresi; // sürekli ateş edilememesini sağladık. (ateslemeGecenSure'ye göre bir ates edecek.)
            //Debug.Log("ateş edildi");
            Instantiate(lazer, lazerCikisYeri.position, Quaternion.identity); //bu metot ile ateş etme işlemi sırasında oluşacak obje, posizyonu ve rotasyonu belirlenir.       
            audioS.Play(); //lazer ateşleme sesi
        }
    }

}
