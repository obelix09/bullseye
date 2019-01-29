using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnMouseUp()
    {
        LevelManager.startGame = true;                                          //start the game
        gameObject.SetActive(false);                                            //deactivate the button
    }                                                           
}
