using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMover : MonoBehaviour {

    [SerializeField]
    private float speed = 5f;

    private void Update ()
    {
        var movement = Input.GetAxis("Horizontal");
        transform.position += Vector3.right * movement * speed;
	}
}
