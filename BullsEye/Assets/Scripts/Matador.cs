﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matador : MonoBehaviour {

    private float angle;
    private Vector2 center;

    public float speed = 5f;
    public float radius = 1f;

    // Use this for initialization
    void Start () {
        center = new Vector2(0f, 0f);
	}
	
	// Update is called once per frame
	void Update () {
        angle += speed * Time.deltaTime;
        var offset = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * radius;
        transform.position = center + offset;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Bull")
        {
            Destroy(gameObject);
        }
    }
}

