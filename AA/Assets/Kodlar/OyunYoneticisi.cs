using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OyunYoneticisi : MonoBehaviour {

    GameObject donenCember;
    GameObject anaCember;
    public Animator animator;
    public Text hangiLevelText;
    public Text kalanIgne1;
    public Text kalanIgne2;
    public Text kalanIgne3;
    public int kacTaneIgneOlsun; // her level'da değişecek.
    bool levelKontrol = true; // !!!bu kısım kritik, true vermezsen çalışmıyor.

	void Start ()
    {
        PlayerPrefs.SetInt("kayit", int.Parse(SceneManager.GetActiveScene().name)); //hangi levelda kaldıysak onu hafizaya kayıt ediyor.

        donenCember = GameObject.FindGameObjectWithTag("DonenCemberTag");
        anaCember = GameObject.FindGameObjectWithTag("AnaCemberTag");
        hangiLevelText.text = SceneManager.GetActiveScene().name; // sahne adı 
        /*kalan igne(kucuk cember) sayisi icin kosul*/
        if (kacTaneIgneOlsun < 2)
        {
            kalanIgne1.text = kacTaneIgneOlsun + ""; //kactaneigneolsun + "" kısmı ile kactanegineolsun'u integer'dan string'e çevirdik.
        }
        else if (kacTaneIgneOlsun <3)
        {
            kalanIgne1.text = kacTaneIgneOlsun + "";
            kalanIgne2.text = (kacTaneIgneOlsun - 1) + "";
        }
        else
        {
            kalanIgne1.text = kacTaneIgneOlsun + "";
            kalanIgne2.text = (kacTaneIgneOlsun - 1) + "";
            kalanIgne3.text = (kacTaneIgneOlsun - 2) + "";
        }
      }

    public void kalanIgneSayisi()
    {
        kacTaneIgneOlsun--; // kalanIgneSayisi metodu cagrildiginda, igne sayıları azalacak.

        /*kalan igne(kucuk cember) sayisinin azaltılması icin kosul*/
        if (kacTaneIgneOlsun < 2)
        {
            kalanIgne1.text = kacTaneIgneOlsun + ""; // 1 olacak
            kalanIgne2.text = ""; // eğer bu şekilde temizlemezsek önceki değer olan 1 yazar
            kalanIgne3.text = ""; // eğer bu şekilde temizlemezsek önceki değer olan 1 yazar
        }
        else if (kacTaneIgneOlsun < 3)
        {
            kalanIgne1.text = kacTaneIgneOlsun + "";
            kalanIgne2.text = (kacTaneIgneOlsun - 1) + "";
            kalanIgne3.text = "";
        }
        else
        {
            kalanIgne1.text = kacTaneIgneOlsun + "";
            kalanIgne2.text = (kacTaneIgneOlsun - 1) + "";
            kalanIgne3.text = (kacTaneIgneOlsun - 2) + "";
        }
        /*Yeni levela geçme koşulu*/
        if (kacTaneIgneOlsun == 0)
        {
            StartCoroutine(yeniLevel());
        }
    }

    IEnumerator yeniLevel() // bu metot çağrılarak yeni levela geçilecek.
    {
        donenCember.GetComponent<Dondurme>().enabled = false; // oyun bittiği için dönen çemberimi durduruyorum. (!!!enabled: false olduğunda scriptin arayüzdeki tikini kaldırarak o scripti inaktif ediyor.)
        anaCember.GetComponent<AnaCember>().enabled = false; // oyun bittiği için iğne atmayi da durduruyoruz.

        yield return new WaitForSeconds(1); //0 olduğunda direkt sonraki levela geçmeden 1 saniye bekliyor.

        if (levelKontrol)
        {
            animator.SetTrigger("YeniLevel"); //yeni levela geçişte "YeniLevel" animasyonu çalışacak.
            yield return new WaitForSeconds(1.5f);//oyun bittiğinde yeni levela geömeden önce 1.5 saniye bekletiyoruz ki animasyonumuz da görünsün.
            SceneManager.LoadScene(int.Parse(SceneManager.GetActiveScene().name) + 1); // bir sonraki level ekranını yüklüyoruz.   
        }
    }

    public void oyunBitti()
    {
        StartCoroutine(cagrilanMetot());
    }
	
    IEnumerator cagrilanMetot() //IEnumerator şeklinde tanımlanan metotlar belli bir saniye durdurulabilir.
    {
        donenCember.GetComponent<Dondurme>().enabled = false; // oyun bittiği için dönen çemberimi durduruyorum. (!!!enabled: false olduğunda scriptin arayüzdeki tikini kaldırarak o scripti inaktif ediyor.)
        anaCember.GetComponent<AnaCember>().enabled = false; // oyun bittiği için iğne atmayi da durduruyoruz.
        animator.SetTrigger("OyunBitti"); //Oyun bittiğinde oluşturduğumuz "OyunBitti" animasyonumuz çalışacak.
        levelKontrol = false; // igne sayisi 0 oldu ancak çarparak oldu, yani yeni levela geçmemeli.

        yield return new WaitForSeconds(1.5f); //oyun bittiğinde ana menüye dönmeden önce 1.5 saniye bekletiyoruz ki animasyonumuz da görünsün.

        SceneManager.LoadScene("AA Ana Menu"); //oyun bitince ana menüye dön.
    }
}
