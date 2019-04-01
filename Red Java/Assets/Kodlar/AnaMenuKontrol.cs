using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AnaMenuKontrol : MonoBehaviour {

    //GameObject level1, level2, level3; (!!! for döngülerini yazınca gerek kalmadı)
    GameObject levellar, kilitler;

	void Start ()
    {
        //level1 = GameObject.Find("level 1"); // level objemizi buluyoruz.(!!!gameobject.find çok obje olduğunda yavaş çalışır)
        //level2 = GameObject.Find("level 2");
        //level3 = GameObject.Find("level 3");

        levellar = GameObject.Find("levellar"); // levellar objesine ulaşıyoruz
        kilitler = GameObject.Find("kilitler"); // kilitler objesine ulaşıyoruz

        for (int i = 0; i < levellar.transform.childCount; i++)
        {
            levellar.transform.GetChild(i).gameObject.SetActive(false); //başlangıçta levellar görünmesin.
        }

        for (int i = 0; i < kilitler.transform.childCount; i++)
        {
            kilitler.transform.GetChild(i).gameObject.SetActive(false); //başlangıçta kilitler görünmesin.
        }

        for (int i = 0; i < PlayerPrefs.GetInt("kacinciLevel"); i++) //bulunduğu level bilgisine göre koşul
        {
            levellar.transform.GetChild(i).GetComponent<Button>().interactable =true; //bulunulan level ve öncekilerin butonunu tıklanabilir yapıyoruz.
        }
    }

    public void butonSec(int gelenButon) //tıklanan butona göre çalışacak metot
    {
        if (gelenButon == 1) //oyuna basla butonu
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("kacinciLevel")); //kacinci levelda kalındıysa oradan basla
        }
        else if (gelenButon == 2) // bolumler butonu
        {
            for (int i = 0; i < levellar.transform.childCount; i++)
            {
                levellar.transform.GetChild(i).gameObject.SetActive(true); //level butonlarını görünür yap
            }

            for (int i = 0; i < kilitler.transform.childCount; i++)
            {
                kilitler.transform.GetChild(i).gameObject.SetActive(true); //kilitleri görünür yap
            }

            for (int i = 0; i < PlayerPrefs.GetInt("kacinciLevel"); i++)
            {
                kilitler.transform.GetChild(i).gameObject.SetActive(false); //bulunulan level ve öncekilerin kilidini aç(sil)
            }
        }
        else if (gelenButon == 3) //oyundan cik butonu
        {
            Application.Quit();
        }
    }

    public void levellarButon(int gelenLevel) // level butonlarına tıklandığında çalışacak metot
    {
        SceneManager.LoadScene(gelenLevel); //tıklanan butona göre levela git
    }
}
