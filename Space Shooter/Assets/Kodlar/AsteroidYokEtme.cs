using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidYokEtme : MonoBehaviour {

    public GameObject uzayGemisiPatlama;
    public GameObject patlama;
    //AudioSource audioS; //çalışmadı !!!
    GameObject oyunKontrol;
    OyunKontrol kontrolScript;

    void Start()
    {
        oyunKontrol = GameObject.FindGameObjectWithTag ("oyunKontrol"); //oyun kontrol objemize bu şekilde eriştik.
        kontrolScript = oyunKontrol.GetComponent<OyunKontrol>(); //oyun kontrol objesinin içindeki "oyun kontrol" scriptine eriştik.
        //audioS = GetComponent<AudioSource>(); //çalışmadı !!!
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag != "arkaplan")
        {
            if (col.tag == "altSinir") //alt sinira çarpan asteroidlerin yok olması için 
            {
                Destroy(gameObject);
            }
            else
            {
                Destroy(col.gameObject); //asteroride temas eden lazerin yok olması
                Destroy(gameObject); //asteroidin kendisinin yok olması
                Instantiate(patlama, transform.position, transform.rotation); //patlama efekti (!önemli: patlama sesini de bu efektin içine ekleyerek sonuç alabildim.)
                //audioS.Play(); // asteroid patlama sesi (çalışmadı !!!)
                kontrolScript.scoreArttir(10); // her asteroid patladığında 10 puan artacak.
            }
        }

        if (col.tag == "uzayGemisi")
        {
            Instantiate(uzayGemisiPatlama, col.transform.position, col.transform.rotation); // uzay gemisi patlama efekti
            kontrolScript.oyunBitti(); //Oyun kontrol sınıfındaki oyunbitti metodunu çağırdık.
        }
    }

}
