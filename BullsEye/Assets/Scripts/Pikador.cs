using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pikador : MonoBehaviour {

    private float angle;
    private Vector2 center;
    private float[] circles = { 1.45f, 2.4f, 3.35f, 4.3f };
    private float[] circleTurn = { -1f, 1f };
    private float[] speeds = { 0.8f, 1.3f, 1.8f, 2.3f, 2.8f, 3.2f, 3.8f, 4.2f, 4.8f };
    private float turn;
    public float speed;
    public float radius;
    public float stun;

    public AudioSource audioSource;


    // Use this for initialization
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        center = new Vector2(0f, 0f);
        speed = speeds[Random.Range(0, speeds.Length)];
        radius = circles[Random.Range(0, circles.Length)];
        turn = circleTurn[Random.Range(0, circleTurn.Length)];

        if (radius == circles[0])
        {
            stun = 0.5f;
        }
        if (radius == circles[1])
        {
            stun = 1f;
        }
        if (radius == circles[2])
        {
            stun = 1.5f;
        }
        if (radius == circles[3])
        {
            stun = 2f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        angle += speed * Time.deltaTime;
        var offset = new Vector2((Mathf.Sin(angle)), turn * (Mathf.Cos(angle))) * radius;
        transform.position = center + offset;

        Vector2 direction = new Vector2(0f - transform.position.x, 0f - transform.position.y);
        transform.up = direction;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Bull")
        {
            if (col.gameObject.GetComponent<Bull>().attack)
            {
                audioSource.Play();
            }
        }
    }
}
