using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matador : MonoBehaviour {

    private float angle;
    private Vector2 center;
    private float[] circles = { 1.45f, 2.4f, 3.35f, 4.3f };
    private float[] circleTurn = { -1f, 1f };
    private float minSpeed = 1f;
    private float maxSpeed = 5f;
    private float turn;
    public float speed;
    public float radius;
    public int score;

    private AudioSource audioSource;
    public AudioClip[] audioClips;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        int range = Random.Range(0, audioClips.Length);
        audioSource.clip = audioClips[range];
    }

    void Start() {
        center = new Vector2(0f, 0f);
        speed = Random.Range(minSpeed, maxSpeed);
        radius = circles[Random.Range(0, circles.Length)];
        turn = circleTurn[Random.Range(0, circleTurn.Length)];

        if (radius == circles[0])
        {
            score = 5;
        }
        if (radius == circles[1])
        {
            score = 10;
        }
        if (radius == circles[2])
        {
            score = 15;
        }
        if (radius == circles[3])
        {
            score = 20;
        }
    }
	
	// Update is called once per frame
	void Update () {
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
                gameObject.GetComponent<SpriteRenderer>().sortingOrder = 0;
                Destroy(gameObject, audioSource.clip.length);
                ScoreManager.score += score;
                LevelManager.deadMatadors += 1;
            }
        }
    }
}

