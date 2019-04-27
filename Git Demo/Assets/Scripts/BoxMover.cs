using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMover : MonoBehaviour {

    [SerializeField]
    private float speed = 3f; //branch denemesi

    private void Update ()
    {
        var movement = Input.GetAxis("Horizontal");
        transform.position += Vector3.right * movement * speed;
	}
    /*new branch function*/
    /*new branc function 2*/
}
