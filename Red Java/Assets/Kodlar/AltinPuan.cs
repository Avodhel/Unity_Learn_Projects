using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltinPuan : MonoBehaviour
{

    public Sprite[] altinAnimasyon;
    SpriteRenderer spriteRenderer;
    float zaman = 0f;
    int animasyonSpritelerSayaci; //altin spritelarını sırayla renderlamak için

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        zaman += Time.deltaTime;
        if (zaman > 0.3f) //her 0.1 saniyede bu koşula gir
        {
            spriteRenderer.sprite = altinAnimasyon[animasyonSpritelerSayaci++]; //sırayla altin spritelarını renderla
            if (altinAnimasyon.Length == animasyonSpritelerSayaci) //spriteların sonuna gelindiğinde
            {
                animasyonSpritelerSayaci = 0; //ilk sprite'a dön (bu sayede sonsuz bir döngü oluşuyor)
            }
            zaman = 0; //0.1 saniyede bir koşula girmesi için (sıfırlamazsak sadece 1 kere girer)
        }
    }
}
