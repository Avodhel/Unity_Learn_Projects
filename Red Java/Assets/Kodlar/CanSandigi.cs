using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanSandigi : MonoBehaviour {

    public Sprite[] sandikAnimasyon;
    SpriteRenderer spriteRenderer;
    float zaman = 0f;
    int animasyonSpritelerSayaci; //sandik spritelarını sırayla renderlamak için

	void Start ()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	void Update ()
    {
        zaman += Time.deltaTime;
        if (zaman > 0.1f) //her 0.1 saniyede bu koşula gir
        {
            spriteRenderer.sprite = sandikAnimasyon[animasyonSpritelerSayaci++]; //sırayla sandik spritelarını renderla
            if (sandikAnimasyon.Length == animasyonSpritelerSayaci) //spriteların sonuna gelindiğinde
            {
                animasyonSpritelerSayaci = sandikAnimasyon.Length - 1; //sprite renderlamayı durdur
            }
            zaman = 0; //0.1 saniyede bir koşula girmesi için (sıfırlamazsak sadece 1 kere girer)
        }
	}
}
