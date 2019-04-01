using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OyunKontrol : MonoBehaviour {

    public GameObject gokyuzu1;
    public GameObject gokyuzu2;
    Rigidbody2D fizik1;
    Rigidbody2D fizik2;
    float gokyuzuUzunluk = 0f;
    public float arkaplanHiz = 1.5f;
    public GameObject engel;
    public int kacAdetEngel;
    GameObject[] engeller;
    float engelDegisimZamani = 0f;
    int sayac = 0;
    bool engelOlusturDurdur = false;

	void Start ()
    {
        fizik1 = gokyuzu1.GetComponent<Rigidbody2D>(); // gokyuzu 1'in rigidbody'si
        fizik2 = gokyuzu2.GetComponent<Rigidbody2D>(); // gokyuzu 2'nin rigidbody'si

        gokyuzuUzunluk = gokyuzu1.GetComponent<BoxCollider2D>().size.x; // gokyuzu1'in collider'ının uzunluğu

        engelOlustur();  
    }

    void Update()
    {
        engelPozisyonBelirle();
    }
	
    void FixedUpdate()
    {
        gokyuzuHareket();   
    }

    void gokyuzuHareket()
    {
        fizik1.velocity = new Vector2(-arkaplanHiz, 0); //x ekseninde geriye doğru 1.5 hızında hareket
        fizik2.velocity = new Vector2(-arkaplanHiz, 0);

        if (gokyuzu1.transform.position.x <= -gokyuzuUzunluk) // gokyuzu 1'in pozisyonu uzunluktan(23,04) küçük veya eşit olduğunda
        {
            gokyuzu1.transform.position += new Vector3(gokyuzuUzunluk * 2, 0); // gokyuzu 1'i uzunluğunun 2 katı kadar ileri at.
            /*amaç gokyuzu 1'in sonuna gelindiğinde gokyuzu 2'nin ilerisine atarak sonsuz bir gokyuzu dongusu oluşturmak*/
        }

        if (gokyuzu2.transform.position.x <= -gokyuzuUzunluk) // gokyuzu 2'nin pozisyonu uzunluktan(23,04) küçük veya eşit olduğunda
        {
            gokyuzu2.transform.position += new Vector3(gokyuzuUzunluk * 2, 0); // gokyuzu 2'yi uzunluğunun 2 katı kadar ileri at.
            /*amaç gokyuzu 2'nin sonuna gelindiğinde gokyuzu 1'in ilerisine atarak sonsuz bir gokyuzu dongusu oluşturmak*/
        }
    }

    void engelOlustur()
    {
        if (!engelOlusturDurdur) //oyun bittiğinde true oluyor ve bu koşula girmiyor.
        {
            engeller = new GameObject[kacAdetEngel]; //kacAdetEngel'e verilen sayı kadar engel objesi

            for (int i = 0; i < engeller.Length; i++) // engeller dizisi kadar obje oluştur
            {
                engeller[i] = Instantiate(engel, new Vector2(-10, -10), Quaternion.identity); // engel objelerinin oluşturulması
                Rigidbody2D fizikEngel = engeller[i].AddComponent<Rigidbody2D>(); //engelimize koddan rigidbody ekledik.
                fizikEngel.gravityScale = 0; //oluşan engellerin düşmemesi için 
                fizikEngel.velocity = new Vector2(-arkaplanHiz, 0); // engellerimizin arkaplan ile birlikte hareket etmesi için.
            }
        }
    }

    void engelPozisyonBelirle() // oluşturulan engellerin random pozisyonlarda oluşması için metot
    {
        if (!engelOlusturDurdur) //oyun bittiğinde true oluyor ve bu koşula girmiyor.
        {
            engelDegisimZamani += Time.deltaTime;

            if (engelDegisimZamani > 2f) // her 2 saniyede bir bu koşula girecek ve engelleri oluşturacak.
            {
                engelDegisimZamani = 0;
                float yEkseniDegisim = Random.Range(-0.7f, 1.3f); // y eksenini random olarak değiştirerek engellerin random bir konumda gelmesini sağlıyoruz.
                engeller[sayac].transform.position = new Vector3(17.3f, yEkseniDegisim); // engellerimizin oluşum alanını belirliyoruz.
                                                                                         /*!!! Vector3 iki tane değer de alabilir.*/
                sayac++;
                if (sayac >= engeller.Length) // dizideki obje sayısına ulaştığımda
                {
                    sayac = 0; // sayacı sıfırla
                }
            }
        }
    }

    public void oyunBitti() // public diyoruz çünkü Kontrol scriptinde bu metoda erişmem lazım.
    {
        for (int i = 0; i < engeller.Length; i++)
        {
            engeller[i].GetComponent<Rigidbody2D>().velocity = Vector2.zero; // oyun bittiği için engellerimi durduruyorum.
            /*Vector2.zero = new Vector2(0, 0)*/
        }

        engelOlusturDurdur = true; // engel oluşturmayı da durduruyorum
        arkaplanHiz = 0; // arkaplanı da durduruyorum
        //Destroy(this); //Ek Bilgi: OyunKontrol Scriptini komple silerek etkisiz hale getirir.
    }
}
