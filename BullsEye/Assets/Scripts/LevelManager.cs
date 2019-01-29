using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

    private GameObject bull;                                                    //the bull
    private List<GameObject> matadorList;                                       //list of matadors
    public GameObject matadorPrefab;                                            //matador prefab
    public GameObject playAgainButton;
    private int maxNumber;                                                      //max number of matadors
    private int level;                                                          //level of game

    public static int deadMatadors;                                             //how many matadors are dead
    public static bool gameOver;                                                //check if game over
    public static bool startGame;                                               //check if new game

    public GameObject panel;                                                    
    Text infoText;
    Text levelText;

    // Use this for initialization
    void Start()
    {
        level = 1;                                                              //start at level 1
        maxNumber = 1;                                                          //max number starts at 1
        matadorList = new List<GameObject>();                                   //create a matadorList
        levelText = GetComponent<Text>();                                       //level display
        infoText = GameObject.Find("InfoText").GetComponent<Text>();            //info display
        bull = GameObject.FindGameObjectWithTag("Bull");                        //inisialize the bull

        panel.SetActive(false);                                                 //hide panel
        infoText.gameObject.SetActive(false);                                   //hide infotext
        bull.SetActive(false);                                                  //hide bull
    }

    // Update is called once per frame
    void Update()
    {
        if(startGame)                                                           //if game has started
        {
            StartGame();
        }

        if (gameOver)                                                           //if game is over                                                       
        {
            GameOver();
        }                 

        if (deadMatadors == maxNumber)                                          //if all matadors are dead
        {
            NextLevel();                                                        //set the next level
            FillMatadors();                                                     //create new matadors
        }

        levelText.text = "Level: " + level;
    }

    private void StartGame()
    {
        ScoreManager.score = 0;                                                 //reset score
        level = 1;                                                              //reset level 
        maxNumber = 1;                                                          //reset maxNumber
        deadMatadors = 0;                                                       //reset number of dead matadors
        TimeMananger.mainTimer = 10f;                                           //reset timer
        TimeMananger.timeFinished = false;
        StartCoroutine(ShowLevel());                                            //show what level
        matadorList.Clear();                                                    //clear matador list
        FillMatadors();                                                         //create matadors
        startGame = false;                                                      //startgame done
    }

    private void GameOver()
    {
        bull.SetActive(false);                                                  //hide bull    
        GameObject[] matadors = GameObject.FindGameObjectsWithTag("Matador");   //get all matadors
        foreach (GameObject matador in matadors)                                //Destroy all matadors 
        {
            Destroy(matador);
        }
        playAgainButton.SetActive(true);                                        //activate play again button
        gameOver = false;                                                       //gameOver not anymore
    }

    private void FillMatadors()
    {
        for (int i = 0; i < maxNumber; i++)
        {
            GameObject newMatador = Instantiate(matadorPrefab);
            matadorList.Add(newMatador);
        }
    }

    private void NextLevel()
    {
        level += 1;
        if (maxNumber <= 10)
        {
            TimeMananger.mainTimer += 5f;
            maxNumber += 1;
        }
        StartCoroutine(ShowLevel());
        deadMatadors = 0;
        matadorList.Clear();
    }

    private IEnumerator ShowLevel()
    {
        bull.SetActive(false);
        bull.transform.position = new Vector2(0f, 0f);
        infoText.text = "Level " + level;
        infoText.gameObject.SetActive(true);
        panel.SetActive(true);
        TimeMananger.canCount = false;                                          //pause the count
        yield return new WaitForSeconds(2f);                                    //show level for 2sek
        TimeMananger.resetTimer = true;                                         //reset the timer
        infoText.gameObject.SetActive(false);
        panel.SetActive(false);
        bull.SetActive(true);
    }
}
