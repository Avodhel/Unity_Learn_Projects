using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarakterKontrol : MonoBehaviour {

    float horizontal, vertical;
    Vector3 vec;
    GameObject kafa;
    float kafaRot = 0;
    RaycastHit hit;
    public GameObject nisangah;
    public GameObject kursun;
    Animator animasyonlar;
    bool atesAnimKontrol = false;
    float atesZaman = 1;

	void Start ()
    {
        vec = new Vector3(); //vectoru bir kere boş olarak tanımlıyoruz ve farlı yerlerde kullanıyoruz.
        kafa = transform.Find("Kafa").gameObject;
        animasyonlar = GetComponent<Animator>();
	}
	
	void Update ()
    {
        karakterHareket();
        rayCizdir();
        atesEt();
        animasyonKontrol();
	}

    void atesEt()
    {
        if (Input.GetMouseButton(0)) //mouse'a tıklandığında
        {
            atesZaman += Time.deltaTime;
            if (atesZaman > 0.5f)
            {
                Instantiate(kursun, nisangah.transform.position, Quaternion.identity);
                atesZaman = 0;
            }
            atesAnimKontrol = true;
        }
        else if (Input.GetMouseButtonUp(0)) // mouse'a tıklanmadığında
        {
            atesAnimKontrol = false;
            atesZaman = 1;
        }
    }

    void karakterHareket()
    {
        /*karakter pozisyonu*/
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        vec.Set(horizontal, 0, vertical); //vec = new Vector3() ile aynı şey
        transform.Translate(vec * Time.deltaTime * 10); //yavaşlatmak için time.deltatime ile çarptık

        /*karakter yonu*/
        transform.Rotate(0, Input.GetAxis("Mouse X") * Time.deltaTime * 150, 0); //karakteri y ekseninde donduruyoruz
        //mouse x ile mouseun hareketi alglanıyor ve ona gore hareket ediyor
        kafaRot += Input.GetAxis("Mouse Y") * Time.deltaTime * 150 * -1; //mouse'a zıt hareket etmemesi için -1 ile çarptık
        kafaRot = Mathf.Clamp(kafaRot, -75, 75); //kafa hareketi için sınır belirliyoruz.
        kafa.transform.eulerAngles = new Vector3(kafaRot, transform.eulerAngles.y, 0);
        //transform.eulerAngles.y yukarı aşağı ile birlikte sağa sola da hareket etmesini sağlıyoruz
    }

    void rayCizdir()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); //kameranın tam orta noktasına bir ray cizdir

        if (Physics.Raycast(ray, out hit)) //ray herhangi bir obje ile temas ettiyse
        {
            //Debug.Log("temas var");
        }
        else //temas yoksa
        {
            //Debug.Log("temas yok");
        }

        Debug.DrawRay(ray.origin, ray.GetPoint(10)); //ray'in nasıl göründüğünü görmek için debug (bir başlangıç noktasından bir yöne doğru)
        Debug.DrawLine(nisangah.transform.position, hit.point); //bir başlangıç ve bitiş noktası arasına çizer
    }

    public Vector3 pozisyonAl() // vector 3 bir fonksiyon (vector 3 deger döndürür)
    {
        return (hit.point - nisangah.transform.position).normalized;
    }

    void animasyonKontrol()
    {
        if (atesAnimKontrol)
        {
            animasyonlar.SetBool("ates_bool", true); //ates etme animasyonunu baslat
        }
        else
        {
            if (horizontal != 0 || vertical != 0) //karakter hareket halindeyse
            {
                animasyonlar.SetBool("yurume_bool", true); //yürüme animasyonuna geçiş yap ve oynat
            }
            else if (horizontal == 0 && vertical == 0)
            {
                animasyonlar.SetBool("yurume_bool", false); //yürüme animasyonunu durdur
            }

            animasyonlar.SetBool("ates_bool", false); //ates etme animasyonunu durdur
        }

        //if (Input.GetKeyDown(KeyCode.Z)) // z'ye basıldığında
        //{
        //    animasyonlar.SetBool("yurume_bool", true); //yürüme animasyonuna geçiş yap ve oynat
        //}

        //if (Input.GetKeyUp(KeyCode.Z)) // z tuşu serbest kaldığında
        //{
        //    animasyonlar.SetBool("yurume_bool", false); //yürüme animasyonunu durdur
        //}
    }
}
