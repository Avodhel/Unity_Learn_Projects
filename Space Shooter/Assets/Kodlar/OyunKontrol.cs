using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OyunKontrol : MonoBehaviour {

    public GameObject[] asteroidler; //3 farklı asteroid tipimiz olduğundan array olarak tanımladık.
    public Vector3 randomPos;
    public float baslangicBeklemeSure;
    public float asteroidOlusturmaSure;
    public float asteroidGrupOlusturmaSure;
    public int minAsteroidSayisi;
    public int maxAsteroidSayisi;
    int score;
    public Text scoreText;
    public Text oyunBittiText;
    public Text yenidenBaslaText;
    public Text levelTamamlandiText;
    bool oyunBittiKontrol = false; // bu kontrol ile oyun bittikten sonra asteroid oluşumunu bitiriyoruz.
    bool yenidenBaslaKontrol = false; // bu kontrol ile oyunun yeniden başlatılmasını sağlıyoruz.
    AudioSource audioS;
    public int yeniLevelSkorSiniri;
    string mevcutLevelSahnesi;
    int mevcutLevelSahnesiInt;

	void Start ()
    {
        audioS = GetComponent<AudioSource>();
        if (SceneManager.GetActiveScene().name == "1") // ilk leveldan başlandığında skor
        {
            score = 0;
        }
        else // ilk leveldan başlanmadığında skor 
        {
            score = PlayerPrefs.GetInt("mevcutSkor"); // önceki levellardan elde edilen skorları da ekliyorum.
        }
        scoreText.text = "score: " + score;
        StartCoroutine(randomOlustur()); //metodu çağırdım.
        mevcutLevelSahnesi = SceneManager.GetActiveScene().name;
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && yenidenBaslaKontrol) //yeniden başlatmak için R tuşuna basılacak ve bu metot çalışacak.
        {
            SceneManager.LoadScene("1"); //Buraya sahnemizin adını veriyoruz ki yeniden başlatılsın.
        }
    }

    IEnumerator randomOlustur() //IEnumerator metot değil interface
    {
        yield return new WaitForSeconds(baslangicBeklemeSure); // asteroidleri oluşturmadan önce 2 saniye bekle.
        while (true) // true koşulu ile while sonsuza kadar döner
        {
            for (int i = 0; i < Random.Range(minAsteroidSayisi, maxAsteroidSayisi); i++)
            {
                GameObject asteroid = asteroidler[Random.Range(0, asteroidler.Length)]; // 3 asteroid tipi arasından random seçim.
                Vector3 vec = new Vector3(Random.Range(-randomPos.x, randomPos.x), 0, randomPos.z); //iki sayı arasında ramdom bir değer üretmek için "Random.Range" kullanılır.
                Instantiate(asteroid, vec, Quaternion.identity);
                yield return new WaitForSeconds(asteroidOlusturmaSure); //for döngüsünü 0.7 saniye aralıkla dönüyor. (her asteroidi 0.7 saniye aralıkla oluşturuyor bu sayede asteroidlerin birbirini patlatmasının önüne geçtik.)
            }

            if (oyunBittiKontrol)
            {
                yenidenBaslaText.text = "Yeniden baslamak icin 'R' tusuna basiniz.";
                yenidenBaslaKontrol = true; //yeniden başlaması için true yapıyoruz ve update metodundaki "yenidenbaslakontrol" şartını sağlıyoruz.
                break; //while döngüsünü kırıyoruz ki taşlar yeniden çıkmasın.
            }

            yield return new WaitForSeconds(asteroidGrupOlusturmaSure); // her 10 luk asteroid grubunu oluştururken 2 saniye bekle
        }
    }

    public void scoreArttir(int gelenScore) //public diyoruz çünkü AsteroidYokEtme sinifindan bu metoda erişmemiz gerekecek.
    {
        score += gelenScore;
        scoreText.text = "score: " + score; //skoru yazdırıyoruz.
        if (yeniLevelSkorSiniri == score)
        {
            StartCoroutine(yeniLevelaGecis()); //yeni levela gec.
        }
    }

    public void oyunBitti() //uzay gemisi patladığında çalışacak.
    {
        oyunBittiText.text = "Oyun Bitti";
        oyunBittiKontrol = true; //yenidenBaslaText'in görünmesi ve yenidenBaslaKontrol'un true olması için yaptık.
        yenidenBaslaKontrol = false; //oyun bitmeden R'ye basıldığında yeniden başlamaması için yaptık.
        audioS.mute = true; // uzay gemisi patladığından oyun müziği duruyor.
    }

    IEnumerator yeniLevelaGecis() //yeni level icin gereken skor saglandiginde calisacak.
    {
        levelTamamlandiText.text = "Level " + mevcutLevelSahnesi + " Tamamlandi"; // level tamamlandi yazisi.
        mevcutLevelSahnesiInt = int.Parse(mevcutLevelSahnesi);
        yield return new WaitForSeconds(2); // yeni levela geçmeden önce 2 saniye bekletiyorum.
        SceneManager.LoadScene(mevcutLevelSahnesiInt + 1);
        PlayerPrefs.SetInt("mevcutSkor", score); // önceki leveldan elde edilen skoru "mevcutSkor" kaydi ile saklıyorum.
    }
	
}
