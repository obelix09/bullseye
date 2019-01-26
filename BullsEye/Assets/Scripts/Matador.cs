using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matador : MonoBehaviour {

    private float angle;
    private Vector2 center;
    private float[] circles = { 1.45f, 2.4f, 3.35f, 4.3f };
    private int minSpeed = 1;
    private int maxSpeed = 5;

    public float speed;
    public float radius;

    // Use this for initialization
    void Start() {
        center = new Vector2(0f, 0f);
        speed = Random.Range(minSpeed, maxSpeed);
        radius = circles[Random.Range(0, circles.Length)];
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

