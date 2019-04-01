using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour {

	private void Update ()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GetComponent<Renderer>().material.color = GetRandomColor();
        }
	}

    private Color GetRandomColor()
    {
        return new Color(UnityEngine.Random.Range(0, 1f),
                         UnityEngine.Random.Range(0, 1f),
                         UnityEngine.Random.Range(0, 1f));
    }
}
