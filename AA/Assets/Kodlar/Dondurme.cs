﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dondurme : MonoBehaviour {

    public float hiz;
	
	void Update ()
    {
        transform.Rotate(0, 0, Time.deltaTime * hiz); //çember z eksenine göre döndüğü işlemimizi z ekseninde yaptık.
	}
}
