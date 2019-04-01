using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuanObjeleri : MonoBehaviour {

	void Start () {
		
	}

	void Update () {
        transform.Rotate(new Vector3(15, 30, 45)*Time.deltaTime); // dönüşü yavaşlatmak için Time.deltaTime ile çarptık.

        //Debug.Log(Time.deltaTime); //her update'in çağrı zamanı
	}
}
