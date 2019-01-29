using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bull : MonoBehaviour
{

    private Vector2 centerPos;                                                  //center position, where the bull should stay
    private Vector2 mousePos;                                                   //mouse position
    private Vector2 bullPos;                                                    //bull position
    private Vector2 attackPos;                                                  //attack position, where the bull will go
    private LineRenderer line;                                                  //line between center and bull
    private float bullDistance;                                                 //distance from center to bull
    private bool dragging;                                                      //can drag a line or not
    public bool attack;                                                         //bull attacking or not
    private bool retrieve;                                                      //bull retrieving or not
    private bool stunned;                                                       //is bull stunned or not
    public float speed;                                                         //speed of the bull

    private bool shakeLeft;


    // Use this for initialization
    void Start()
    {
        centerPos = transform.position;                                         //Save the starting position of the bull (center)
        line = this.gameObject.AddComponent<LineRenderer>();                    // Add a Line Renderer to the GameObject
        line.startWidth = 0.1f;                                                 // Set the width of the line
        line.endWidth = 0.1f;
        line.material.color = Color.blue;
        line.positionCount = 2;
        line.SetPosition(0, new Vector3(centerPos.x, centerPos.y, -0.5f));        // Set the begginning of the line
        line.useWorldSpace = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (stunned)
        {
            if (shakeLeft)
            {
                transform.position =  new Vector2((transform.position.x + 0.2f), transform.position.y);
                shakeLeft = false;
            }
            else
            {
                transform.position = new Vector2((transform.position.x - 0.2f), transform.position.y);
                shakeLeft = true;
            }
        }

        if (dragging)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);     //if the mouse is dragging we wanna know it's position
            line.SetPosition(1, new Vector3(mousePos.x, mousePos.y, -0.5f));      //create a line between the center and mouse position
            //transform.position = new Vector2(mousePos.x, mousePos.y);         //move the bull where the mouse is;
        }

        if (attack && !stunned)
        {
            bullPos = transform.position;                                       //get bulls position
            float step = speed * Time.deltaTime;                                //have a constant speed
            transform.position = Vector2.MoveTowards(bullPos, attackPos, step); //move the bull towards the attack position
            bullDistance = Vector2.Distance(centerPos, bullPos);                //find out the distance of the bull and center              
        }

        if (bullPos == attackPos && attack || bullDistance > 4.7f)              //if the bull is at same location as attack position    
        {                                                                       // or it gone further than 4.7f from center
            attack = false;                                                     //bull should not attack anymore
            retrieve = true;                                                    //begin retreiving
        }

        if (retrieve && !stunned)
        {
            bullPos = transform.position;                                       //get bull location
            float step = speed * Time.deltaTime;                                //constant speed
            transform.position = Vector2.MoveTowards(bullPos, centerPos, step); //move the bull towards the center again
        }

        if (bullPos == centerPos && !attack)                                    //if bull is in the center and not attacking
        {
            retrieve = false;                                                   //he's done retrieving
            line.positionCount = 2;                                             //and we can draw a new line to attack again    

        }
    }

    private void OnEnable()
    {
        stunned = false;
        attack = false;
        retrieve = false;
        transform.position = new Vector2(0f, 0f); 
        line.positionCount = 2;
    }

    void OnMouseDown()                                                          //when bull is mouse clicked
    {
        if (!attack && !retrieve)                                               //and bull is not attacking nor retrieving
        {
            stunned = false;
            dragging = true;                                                    //we can drag a line
        }
    }

    void OnMouseUp()                                                            //when mouse button has gone up
    {
        if (!attack && !retrieve)                                               //and bull is not attacking nor retrieving
        {
            dragging = false;                                                   //we stop dragging
            attack = true;                                                      //bull starts to attack
            attackPos = new Vector2(-(mousePos.x), -(mousePos.y));              //create the attack position in opposite direction of mouseposition
            line.positionCount = 1;                                             //erase the line
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Matador")                                    //when bull hits a matador
        {
            attack = false;                                                     //bull stops attacking
            retrieve = true;                                                    //and retrieves back
        }
        if (col.gameObject.tag == "Picador" && attack)                          //if bull hits a picador
        {
            float stun = col.GetComponent<Pikador>().stun;
            StartCoroutine(Stunned(stun));
        }
    }

    private IEnumerator Stunned(float stun)
    {
        attack = false;
        stunned = true;
        retrieve = true;
        yield return new WaitForSeconds(stun);
        stunned = false;
    }
}
