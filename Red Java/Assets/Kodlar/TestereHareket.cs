using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR // sadece editörde(unity arayüzde) çalışır oyun build edildiğinde inaktif olur.
using UnityEditor; //Editör kodu için paket
#endif

public class TestereHareket : MonoBehaviour {

    public GameObject[] hareketNoktalari; //birden fazla gizmos var
    bool aradakiMesafe1KereHesapla = true; // testere ile gizmo arasındaki mesafenin sadece 1 kere hesaplanması için.
    int aradakiMesafeSayaci = 0;
    bool ileriGeriKontrol = true; // testere ileri mi gidiyor geri mi gidiyor.
    public float testereDonmeHizi = 5f;
    public float testereHareketHizi = 0.5f;
    Vector3 aradakiMesafe;

	void Start ()
    {
        hareketNoktalari = new GameObject[transform.childCount]; // dizinin genişliği oluşturulan alt obje kadar

        for (int i = 0; i < hareketNoktalari.Length; i++)
        {
            hareketNoktalari[i] = transform.GetChild(0).gameObject; //gizmolarimi hareket noktaları dizime ekliyorum.
            hareketNoktalari[i].transform.SetParent(transform.parent); // gizmolarımı dışarı çıkararak testere objesinin alt objesi olmaktan kurtarıyoruz.
            /*testerenin alt objesi olan gizmoları ayırıyorum çünkü testere ile birlikte dönüyorlardı*/
        }
	}
	
	void FixedUpdate ()
    {
        transform.Rotate(0, 0, testereDonmeHizi); //testerenin dönmesi
        noktalaraGit();
	}

    void noktalaraGit() // testerenin oluşturulan gizmolara göre hareket etmesi.
    {
        if (aradakiMesafe1KereHesapla)
        {
            aradakiMesafe = ((hareketNoktalari[aradakiMesafeSayaci].transform.position - transform.position).normalized) * testereHareketHizi; // testerenin gizmonun bulunduğu pozisyona gitmesi için.
            /*.normalized diyerek testerenin sabit bir hızda ilerlemesini sağlıyoruz (diğer türlü mesafe kısaldıkça hızda azalıyordu)*/
            aradakiMesafe1KereHesapla = false;
        }
        float mesafe = Vector3.Distance(transform.position, hareketNoktalari[aradakiMesafeSayaci].transform.position); // testere ile gizmo arasındaki uzaklık.
        transform.position += aradakiMesafe * Time.deltaTime * 10;
        /*time.deltatime ile çarpıyoruz çünkü testerenin gizmos konumuna birden değil yavaş yavaş gitmesini istiyoruz. (her framede bir kere döndür gibi)*/

        if (mesafe < 0.5f) //testere ile gizmo arasındaki fark 0.5f'den küçük olduğunda
        {
            aradakiMesafe1KereHesapla = true; // mesafenin tekrar hesaplanması için.

            if (aradakiMesafeSayaci == hareketNoktalari.Length - 1) //testere son gizmoya ulaştığında
            {
                ileriGeriKontrol = false; //testere geri gitsin
            }
            else if (aradakiMesafeSayaci == 0) // testere tekrar başa döndüğünde
            {
                ileriGeriKontrol = true; // testere ileri gitsin.
            }

            if (ileriGeriKontrol) // testere ileri gidiyorsa
            {
                aradakiMesafeSayaci++; // bir sonraki gizmoya git
            }
            else // testere geri gidiyorsa
            {
                aradakiMesafeSayaci--; // bir önceki gizmoya git
            }
        }
    }

#if UNITY_EDITOR // sadece editörde(unity arayüzde) çalışır oyun build edildiğinde inaktif olur.
    void OnDrawGizmos()
    {
        for (int i = 0; i < transform.childCount; i++) //transfrom.childcount kadar (testere nesnesini altındaki alt obje kadar) dön.
        {
            Gizmos.color = Color.blue; //gizmosun rengi
            Gizmos.DrawWireSphere(transform.GetChild(i).transform.position, 1); //belli bir noktada belli bir yarıçapta gizmos oluşturma.
            /*transform.GetChild ile oluşacak alt nesnelere özellik atıyoruz (pozisyon bilgisi gibi)*/
        }

        for (int i = 0; i < transform.childCount - 1; i++) //transfrom.childcount kadar (testere nesnesini altındaki alt obje kadar) dön.
            /*trans.chilCount - 1 çünkü son objeden sonra çizgi çizmemize gerek yok*/
        {
            Gizmos.color = Color.grey; //gizmosun rengi
            Gizmos.DrawLine(transform.GetChild(i).transform.position, transform.GetChild(i+1).transform.position); //bir gizmodan diğerine çizgi çiz
        }
    }
#endif

}

#if UNITY_EDITOR //build edildiğinde kullanılmaması için editör kodları sadece dizayn aşamasında kullanılacak.
[CustomEditor(typeof(TestereHareket))] //yukarıdaki class ile bağlantı için - 1
[System.Serializable] //yukarıdaki class ile bağlantı için - 2

class testereEditor : Editor //Editör kodu
{
    public override void OnInspectorGUI() // unity arayüzdeki inspector paneline erişim
    {
        TestereHareket script = (TestereHareket)target; //yukarıdaki class ile bağlantı için - 3

        if (GUILayout.Button("Gizmo Olustur", GUILayout.MinWidth(100), GUILayout.Width(100))) // arayüzde testerehareket scriptinin altında buton oluşturma
        {
            GameObject yeniObje = new GameObject(); //Üret butonuna basıldığında obje oluşması
            yeniObje.transform.parent = script.transform; // alt obje olarak oluşturma
            yeniObje.transform.position = script.transform.position; // yeni objelerim script'in (testere'nin) bulunduğu pozisyonda oluşacak.
            yeniObje.name = script.transform.childCount.ToString();// yeni oluşacak objelerin adları (testere objesinin altında 1, 2, 3... şeklinde)
        }

        GUILayout.Label("Testere dönme hizi:");
        script.testereDonmeHizi = GUILayout.HorizontalScrollbar(script.testereDonmeHizi, 1, 0, 20); //testere dönme hizi ayari için scripte scrollbar ekleme.
        GUILayout.Label("Testere hareket hizi:");
        script.testereHareketHizi = GUILayout.HorizontalScrollbar(script.testereHareketHizi, 0.5f, 0, 5f); //testere hareket hizi ayari için scripte scrollbar ekleme.
    }
}
#endif
