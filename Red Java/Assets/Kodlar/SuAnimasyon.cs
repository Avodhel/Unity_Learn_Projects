using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuAnimasyon : MonoBehaviour
{

    public Sprite[] suAnimasyon;
    SpriteRenderer spriteRenderer;
    float zaman = 0f;
    int animasyonSpritelerSayaci; //su spritelarını sırayla renderlamak için

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        zaman += Time.deltaTime;
        if (zaman > 0.1f) //her 0.1 saniyede bu koşula gir
        {
            spriteRenderer.sprite = suAnimasyon[animasyonSpritelerSayaci++]; //sırayla su spritelarını renderla
            if (suAnimasyon.Length == animasyonSpritelerSayaci) //spriteların sonuna gelindiğinde
            {
                animasyonSpritelerSayaci = 0; //ilk sprite'a dön (bu sayede sonsuz bir döngü oluşuyor)
            }
            zaman = 0; //0.1 saniyede bir koşula girmesi için (sıfırlamazsak sadece 1 kere girer)
        }
    }
}
