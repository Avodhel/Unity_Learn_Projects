using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Kontrol : MonoBehaviour {

    public Sprite[] kusSprite; // 3 farklı kus texture'ı var. (amaç kanat çırpma efekti yapmak.)
    SpriteRenderer spriteRenderer;
    bool ileriGeriKanatKontrol = true;
    int kusSayac = 0;
    float kusAnimasyonZaman = 0;
    Rigidbody2D fizik;
    int puan = 0;
    public Text puanText;
    bool oyunBitti = false;
    OyunKontrol oyunKontrol;
    AudioSource []sesler; // puan, kanat ve carpma olmak üzere 3 farklı ses var.
    bool carpmaSesiKontrol = false; // carpma sesinin sadece 1 kere çalması için.
    int enYuksekPuan = 0;
    int reklamSayaci = 0;

    void Start ()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        fizik = GetComponent<Rigidbody2D>();
        oyunKontrol = GameObject.FindGameObjectWithTag("OyunKontrolTag").GetComponent<OyunKontrol>(); // OyunKontrol scriptini aldık.
        sesler = GetComponents<AudioSource>();
        enYuksekPuan = PlayerPrefs.GetInt("enYuksekPuanKayit"); // en yüksek puan bilgimi çekiyorum.
        //PlayerPrefs.DeleteAll(); //skorları temizle (!!!build ederken uygulanmamalı)

        /*reklam gösterme*/
        reklamSayaci = PlayerPrefs.GetInt("reklamSayaciKayit"); // daha önceki kayidi da çekiyorum ki sürekli 0 dan başlamasın önceki kaydın üstüne eklesin.
        reklamSayaci++; // her yeniden baslandığından artacak.
        PlayerPrefs.SetInt("reklamSayaciKayit", reklamSayaci);
	}
	
	void Update ()
    {
        kusAnimasyon();
    }

    void FixedUpdate()
    {
        kusHareket();
    }

    void kusAnimasyon() // kus textureları kullanarak kanat çırpma efekti yaptığımız fonksiyon
    {
        kusAnimasyonZaman += Time.deltaTime;
        /*kanat çırpma hızını düşürmek için koşul*/
        if (kusAnimasyonZaman > 0.2f) // her 0.2 saniyede bir bu koşula girecek.
        {
            kusAnimasyonZaman = 0;

            /*kus kanat çırpma efekti için koşul*/
            if (ileriGeriKanatKontrol)
            {
                spriteRenderer.sprite = kusSprite[kusSayac]; // kusSprite'ın içindeki 3 kus texture'ını sırasıyla oynatıyoruz.
                kusSayac++;
                if (kusSayac == kusSprite.Length) // kus sayacı dizinin sonuna ulaştığında
                {
                    kusSayac--; // iki kere aynı texture'ın üst üste oynatılmaması için
                    ileriGeriKanatKontrol = false; // else'e girmesini sağlıyoruz.
                }
            }
            else
            {
                kusSayac--; //if'den gelen kusSayac 3 olacak ve hata verecek bu hatanın önüne geçiyoruz.
                spriteRenderer.sprite = kusSprite[kusSayac];
                if (kusSayac == 0)
                {
                    kusSayac++; // iki kere aynı texture'ın üst üste oynatılmaması için
                    ileriGeriKanatKontrol = true; //tekrar if'e girmesini sağlıyoruz.
                }
            }
        }
    }

    void kusHareket()
    {
        if (Input.GetMouseButtonDown(0) && !oyunBitti) // mouse'a sol tıklandığında
        {
            fizik.velocity = new Vector2(0, 0); //hizi 0 yaptik (amaç yer çekiminin etkisi ile eksiye düşen hızımızı sıfırlayıp yerçekiminin etkisini azaltmak)
            fizik.AddForce(new Vector2(0, 200)); // y ekseninde 200 lük bir kuvvet uyguluyoruz.
            sesler[0].Play(); // kanat çırpma sesi
        }

        if (fizik.velocity.y > 0) // y ekseninde kuvvet uygulandığında
        {
            /*transform.rotation Quaternion alır.Quaternionlar ise editörde transformun 
           içerisindeki rotation gibi 3 parametre almaz. Bu yüzden eulerAngles kullanıyoruz 
           çünkü vector3 alıyor*/
            transform.eulerAngles = new Vector3(0, 0, 35); // kusun kafasını 35 derece yukarı kaldır.
        }
        else if (fizik.velocity.y < -2.5) // hiz -2,5 altına düştüğünde yani düşüşe geçtiğimizde
        {
            transform.eulerAngles = new Vector3(0, 0, -35); //kusun kafasını 35 derece asağı indir.
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "PuanTag") // puan collider'ına çarptığında 
        {
            puan++;
            puanText.text = "Puan: " + puan;
            sesler[1].Play(); // puan sesi
        }

        if (col.gameObject.tag == "EngelTag") // engellere çarptığında
        {
            oyunBitti = true;

            if (!carpmaSesiKontrol) // carpma sesini sadece 1 kere oynatması için koşul
            {
                sesler[2].Play(); // carpma sesi
                carpmaSesiKontrol = true; // tekrar bu koşula girmemesi için
            }

            oyunKontrol.oyunBitti(); //OyunKontrol scriptindeki oyunBitti metodunu çağırdım.

            if (puan > enYuksekPuan) // en yüksek puan için koşul
            {
                enYuksekPuan = puan;
                PlayerPrefs.SetInt("enYuksekPuanKayit", enYuksekPuan); // en yüksek puanı kayıtlı tutuyoruz.
            }

            Invoke("anaMenuyeDon", 2); // 2 saniye sonra "anaMenuyeDon" metodunu çalıştır.
        }
    }

    void anaMenuyeDon()
    {
        PlayerPrefs.SetInt("puanKayit", puan); // puani kayıtlı tutuyoruz.
        SceneManager.LoadScene("FB Ana Menu"); // ana menu sahnemizi getiriyoruz.
    }
}
