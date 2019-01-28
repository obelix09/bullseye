using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour {

    private bool canClick;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseDown()
    {
        canClick = true;
    }

    private void OnMouseExit()
    {
        canClick = false;
    }

    private void OnMouseUp()
    {
        LevelManager.startGame = true;                                          //start the game
        gameObject.SetActive(false);                                            //deactivate the button
        canClick = false;
    }                                                           
}
