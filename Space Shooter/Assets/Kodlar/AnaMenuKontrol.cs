using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnaMenuKontrol : MonoBehaviour {

    public void oyunaGiris() //oyuna basla butonuna basıldığında çalışan metot.
    {
        SceneManager.LoadScene("1"); // level 1 sahnesinin yüklenmesi ve oyunun başlaması
    }

    public void oyundanCikis() // oyundan çıkış butonuna basıldığında çalışan metot.
    {
        Application.Quit(); //oyundan cikis yapar ancak oyun build edildikten sonra çalışır.
    }

}
