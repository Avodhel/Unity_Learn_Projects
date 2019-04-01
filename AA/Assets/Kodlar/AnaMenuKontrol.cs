using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnaMenuKontrol : MonoBehaviour {

    void Start()
    {
        //PlayerPrefs.DeleteAll(); // kayıtlı level bilgilerinin silinmesi (!!!build ederken yorum satırı olarak kalmalı)
    }

    public void oyunaGiris()
    {
        int kayitliLevel = PlayerPrefs.GetInt("kayit"); // OyunYonetici sinifindaki kalinan level kaydi

        if (kayitliLevel == 0) // ilk kez oynuyorsak veya yeniden başladıysak.
        {
            SceneManager.LoadScene(kayitliLevel + 1); //level 1 sahnesinin getirilmesi.
        }
        else
        {
            SceneManager.LoadScene(kayitliLevel); //kayitli levelin getirilmesi
        }
    }

    public void oyundanCik()
    {
        Application.Quit(); //oyundan cikis yapar ancak oyun build edildikten sonra çalışır.
    }

}
