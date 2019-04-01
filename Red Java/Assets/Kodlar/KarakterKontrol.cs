using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput; //mobil platform kontrolleri için kütüphane (Assets->Import Package->CrossPlatformInput)

public class KarakterKontrol : MonoBehaviour {

    public Sprite[] beklemeAnim; // bekleme animasyonu için birden fazla texture var.
    public Sprite[] ziplamaAnim; // ziplama animasyonu için birden fazla texture var.
    public Sprite[] yurumeAnim; // yurume animasyonu için birden fazla texture var.
    public int ziplamaGucu;
    SpriteRenderer spriteRenderer; //sprite'ları değiştirmek için
    Rigidbody2D fizik;
    GameObject kamera;
    Vector3 vec;
    Vector3 kameraIlkPoz;
    Vector3 kameraSonPoz;
    float horizontal = 0f;
    float beklemeAnimZaman = 0f;
    float yurumeAnimZaman = 0f;
    bool ziplamaKontrol = true; // amaç karakterin  aynı anda birden fazla zıplamasını önlemek.
    int beklemeAnimSayac;
    int yurumeAnimSayac;
    public Text canText;
    public Text altinPuanText;
    public Image kirmiziEkranGecis; //karakter öldükten sonra ekranın yavaşça kızarması için
    float kirmiziTonuSayaci; //kirmizinin tonunu gittikçe arttırmak için.
    int can = 100;
    int altinPuan = 0;
    float anaMenuyeDonusZaman = 0f;

	void Start ()
    {
        Time.timeScale = 1; //ölüm sonrası tekrar başlandığında  oyunun normal hızına dönmesi için.
        kirmiziEkranGecis.gameObject.SetActive(false); //mobile joystick'in hareketini engellediği için kapatıyoruz
        spriteRenderer = GetComponent<SpriteRenderer>();
        fizik = GetComponent<Rigidbody2D>();

        kamera = GameObject.FindGameObjectWithTag("MainCamera");
        kameraIlkPoz = kamera.transform.position - transform.position; //karakter ile kamera arasındaki uzaklık

        canText.text = "Can:" + can; //karakterin canı
        altinPuanText.text = "Puan:" + altinPuan; // toplanan puan

        if (SceneManager.GetActiveScene().buildIndex > PlayerPrefs.GetInt("kacinciLevel")) // kaydin tekrar başa dönmemesi için koşul
        {
            PlayerPrefs.SetInt("kacinciLevel", SceneManager.GetActiveScene().buildIndex); // kaçıncı levelda olduğumuzun kaydı
        }
	}

    void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("Jump")) // space'e basıldığında (mobil)
        {
            if (ziplamaKontrol) // sadece 1 kere zıplaması için kontrol
            {
                fizik.AddForce(new Vector2(0, ziplamaGucu)); // y ekseninde ziplamaHizi kadar kuvvet uygula, yani zıpla.
                ziplamaKontrol = false;
            }
        }
    }
	
	void FixedUpdate ()
    {
        karakterHareket();
        animasyon();

        if (can <= 0) //karakter ölünce ana menüye dönme için koşul
        {
            Time.timeScale = 0.4f; // normalde 1 dir biz düşürerek karakter ölünce oyunun yavaşlamasını sağlıyoruz.
            canText.enabled = false; // can yazısını artık gösterme.
            kirmiziTonuSayaci += 0.01f; //kirmizinin tonunu gittikçe arttırmak için.
            kirmiziEkranGecis.gameObject.SetActive(true); //start metodunda kapatmıştık karakter ölünce geri açıyoruz
            kirmiziEkranGecis.color = new Color(200, 0, 0, kirmiziTonuSayaci); //kirmizinin doygunluğunu ayarlamak için.
            anaMenuyeDonusZaman += Time.deltaTime;
            if (anaMenuyeDonusZaman > 1) // ana menuye donus için koşul
            {
                SceneManager.LoadScene("RJ Ana Menu");
            }
        }
	}

    void LateUpdate()
    {
        kameraKontrol();
    }

    void karakterHareket()
    {
        horizontal = CrossPlatformInputManager.GetAxisRaw("Horizontal"); //mobil platformda hareket
        /*hareket tuşuna basıldığında getaxisraw 0'dan 1 olur getaxis ise 0.1'den 0.2*/
        vec = new Vector3(horizontal * 10, fizik.velocity.y, 0); // sırasıyla parantez içi: x ekseninde 10 hızında koş | y eksenindeki hızım neyse o olsun |
        fizik.velocity = vec;
    }

    void OnCollisionEnter2D(Collision2D col) // geçirgen olmayan bir yüzeye temas edildiğinde çalışır.
    {
        ziplamaKontrol = true; // zıpladıktan sonra karakter yere değdiğinde zıplama tekrar aktif oluyor.
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "kursunTag") //kursun karakterimize değdiyse
        {
            can -= 1; // canı 1 azalt
            canText.text = "Can: " + can;
        }

        if (col.gameObject.tag == "dusmanTag") //dusman karaktere degerse
        {
            can -= 10; // canı 10 azalt
            canText.text = "Can: " + can;
        }

        if (col.gameObject.tag == "testereTag") //testere karaktere degerse
        {
            can -= 10; // canı 10 azalt
            canText.text = "Can: " + can;
        }

        if (col.gameObject.tag == "levelBitisTag") //karakter level bitis portalina ulasinca (degince)
        {
            SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex) + 1); // bir sonraki levela gec
        }

        if (col.gameObject.tag == "canSandigiTag") //karakter can sandigina degince
        {
            can += 10; //karakterin canını 10 arttır
            if (can >=100)
            {
                can = 100;
            }
            canText.text = "Can: " + can;
            col.GetComponent<BoxCollider2D>().enabled = false; // sadece 1 kere 10 can vermesi için colliderı yok ettik.
            col.GetComponent<CanSandigi>().enabled = true; // sandik animasyonu scriptini çalıştır
            //Destroy(col.gameObject, 3); //sandiga temas edilince 3 saniye içinde sandik yok olsun
        }

        if (col.gameObject.tag == "altinTag") //karakter altina degince
        {
            altinPuan++;
            altinPuanText.text = "Puan:" + altinPuan;
            Destroy(col.gameObject); // altini yok et
        }

        if (col.gameObject.tag == "suTag") //karakter suya degince (dusunce)
        {
            can = 0;
        }
    }

    void kameraKontrol()
    {
        kameraSonPoz = kameraIlkPoz + transform.position; // kameranın karakteri takip etmesi için - 1
        kamera.transform.position = Vector3.Lerp(kamera.transform.position, kameraSonPoz, 0.08f); // kameranın karakteri takip etmesi için - 2
        /*Vector.Lerp() kamera takibini yumuşatmak için kullanılır. (kameranın karakter durduktan bir süre sonra durması gibi)*/
    }

    void animasyon()
    {
        if (ziplamaKontrol) // eğer zıplamıyorsa yüreme ve bekleme animasyonlar çalışsın
        {
            if (horizontal == 0) // karakter hareket etmiyorsa
            {
                beklemeAnimZaman += Time.deltaTime;
                if (beklemeAnimZaman > 0.07f) // her 0.07 saniyede bir animasyonu oynat.
                {
                    spriteRenderer.sprite = beklemeAnim[beklemeAnimSayac++];
                    if (beklemeAnimSayac == beklemeAnim.Length) // sayacim texture sayisina eşit olduğunda
                    {
                        beklemeAnimSayac = 0; // sayacımı sıfırla ki bekleme animasyonu devamlı sürsün.
                    }
                    beklemeAnimZaman = 0;
                }
            }
            else if (horizontal > 0) // karakter ilerliyorsa
            {
                yurumeAnimZaman += Time.deltaTime;
                if (yurumeAnimZaman > 0.03f) // her 0.07 saniyede bir animasyonu oynat.
                {
                    spriteRenderer.sprite = yurumeAnim[yurumeAnimSayac++];
                    if (yurumeAnimSayac == yurumeAnim.Length) // sayacim texture sayisina eşit olduğunda
                    {
                        yurumeAnimSayac = 0; // sayacımı sıfırla ki yurume animasyonu devamlı sürsün.
                    }
                    yurumeAnimZaman = 0;
                }
                transform.localScale = new Vector3(1, 1, 1); // x ekseni 1 çünkü ileri gidiyor.
            }
            else if (horizontal < 0) // karakter geri gidiyorsa
            {
                yurumeAnimZaman += Time.deltaTime;
                if (yurumeAnimZaman > 0.03f) // her 0.07 saniyede bir animasyonu oynat.
                {
                    spriteRenderer.sprite = yurumeAnim[yurumeAnimSayac++];
                    if (yurumeAnimSayac == yurumeAnim.Length) // sayacim texture sayisina eşit olduğunda
                    {
                        yurumeAnimSayac = 0; // sayacımı sıfırla ki yurume animasyonu devamlı sürsün.
                    }
                    yurumeAnimZaman = 0;
                }
                transform.localScale = new Vector3(-1, 1, 1); // x ekseni -1 çünkü geri geri giderken karakterin ters istikamete dönmesi lazım.
            }
        }
        else // zıplıyorsa zıplama animasyonu çalışsın
        {
            if (fizik.velocity.y > 0) // karakter zıplamış havadaysa
            {
                spriteRenderer.sprite = ziplamaAnim[0]; // ilk texture'ı oynat
            }
            else // karakter zıplamış ancak artık düşüyorsa
            {
                spriteRenderer.sprite = ziplamaAnim[1]; //ikinci texture'ı oynat
            }

            if (horizontal > 0) //karakter ilerlerken zıplıyorsa
            {
                transform.localScale = new Vector3(1, 1, 1); // x ekseni 1 çünkü ileri gidiyor.
            }
            else if (horizontal < 0) // karakter geri geri koşarken zıplıyorsa
            {
                transform.localScale = new Vector3(-1, 1, 1); // x ekseni -1 çünkü geri geri giderken karakterin ters istikamete dönmesi lazım.
            }
        }   
    }
}
