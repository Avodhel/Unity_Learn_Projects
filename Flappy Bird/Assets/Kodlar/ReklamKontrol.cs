using GoogleMobileAds.Api; // reklam için kütüphane
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReklamKontrol : MonoBehaviour
{

    InterstitialAd interstitial;
    static ReklamKontrol reklamKontrol;
    /*static olarak tanımlarsam nesnesini oluşturmadan ve istediğim yerde kullanabilirim.*/

    void Start()
    {
        if (reklamKontrol == null)
        {
            DontDestroyOnLoad(gameObject); // sahne geçişlerinde objelerin silinmemesi için (çünkü reklamı her sahnede kullanmak istiyorum)
            reklamKontrol = this; //this diyerek "ReklamKontrol" classımızı "reklamKontrol"ün içine atadık.
            /*üstteki kod satırı sayesinde artık reklamKontrol = null olmayacak ve reklamlar nesnesi bir kere oluşacak*/

            /*1.asama (platform kodunun eklenmesi)*/
            #if UNITY_ANDROID //buildde android seçili ise
                        string appId = "ca-app-pub-9747030605482572~3169654676"; //admob'da uygulamama tanımlanan uygulama kimliği
            #elif UNITY_IPHONE //buildde ios seçili ise
                        string appId = "ca-app-pub-3940256099942544~1458002511";
            #else
                        string appId = "unexpected_platform";
            #endif

            MobileAds.Initialize(appId);

            /*2.asama (geçiş reklamı kodunun eklenmesi)*/
            #if UNITY_ANDROID
                        string adUnitId = "ca-app-pub-3940256099942544/1033173712"; //admob'da uygulamama tanımlanan reklam kimliği(şimdilik test reklamını kullanıyoruz.)
            #elif UNITY_IPHONE
                        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
            #else
                        string adUnitId = "unexpected_platform";
            #endif

            interstitial = new InterstitialAd(adUnitId);

            /*3.asama(test reklamlarının yüklenmesi)*/
            AdRequest request = new AdRequest.Builder()
                  .AddTestDevice("2077ef9a63d2b398840261c8221a0c9b")
                  .Build();

            interstitial.LoadAd(request);
        }
        else
        {
            Destroy(gameObject); //2.sefer reklam objesi oluştuğunda yok et.
        }
    }

    public void reklamiGoster()
    {
        /*4.asama (test reklamlarının gösterilmesi)*/
        if (interstitial.IsLoaded()) // reklam yüklendiyse
        {
            interstitial.Show(); // reklamı göster
        }

        reklamKontrol = null; // reklamı tekrar gösterebilmek için.
        Destroy(gameObject); // reklamı tekrar gösterebilmek için.
    }
}
