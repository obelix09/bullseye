using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bull : MonoBehaviour {

    private Vector2 centerPos;                                                  //center position, where the bull should stay
    private Vector2 mousePos;                                                   //mouse position
    private Vector2 bullPos;                                                    //bull position
    private Vector2 attackPos;                                                  //attack position, where the bull will go
    private bool dragging;
    private bool attack;
    private bool retrieve;
    private LineRenderer line;                                                  //line between center and bull
    private float distance;
    public float speed;



    // Use this for initialization
    void Start () 
    {
        centerPos = transform.position;                                         //Save the starting position of the bull (center)
        line = this.gameObject.AddComponent<LineRenderer>();                    // Add a Line Renderer to the GameObject
        line.startWidth = 0.1f;                                                       // Set the width of the line
        line.endWidth = 0.1f;
        line.material = new Material(Shader.Find("Particles/Additive"));
        line.startColor = Color.blue;
        line.endColor = Color.blue;
        line.positionCount = 2;
        line.SetPosition(0, new Vector3(centerPos.x, centerPos.y, -1f));        // Set the begginning of the line
        line.useWorldSpace = true;
    }

// Update is called once per frame
    void Update()
    {
        if (distance > 4.7f)
        {
            dragging = false;
            attack = true;
            attackPos = new Vector2(-(mousePos.x), -(mousePos.y));
            line.positionCount = 1;
        }

        if (dragging)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);     //if the mouse is dragging we wanna know it's position
            //transform.position = new Vector2(mousePos.x, mousePos.y);           //move the bull where the mouse is;
            line.SetPosition(1, new Vector3(mousePos.x, mousePos.y, -1f));      //create a line between the center and bull position
            distance = Vector2.Distance(centerPos, mousePos);                   //calculate the distance
        }   

        if (attack)
        {
            bullPos = transform.position;
            float step = speed * Time.deltaTime;                                // calculate distance to move
            transform.position = Vector2.MoveTowards(bullPos, attackPos, step);

        }

        if (bullPos == attackPos && attack)
        {
            attack = false;
            retrieve = true;
        }

        if (retrieve)
        { 
            bullPos = transform.position;
            float step = speed * Time.deltaTime;                                // calculate distance to move
            transform.position = Vector2.MoveTowards(bullPos, centerPos, step);
        }

        if (bullPos == centerPos && !attack)
        {
            retrieve = false;
            line.positionCount = 2;

        }
    }

    void OnMouseDown()
    {
        if (!attack && !retrieve)
        {
            dragging = true;
        }
    }

    void OnMouseUp()
    {
        if (!attack && !retrieve)
        {
            dragging = false;
            attack = true;
            attackPos = new Vector2(-(mousePos.x), -(mousePos.y));
            line.positionCount = 1;
        }
    }
}
