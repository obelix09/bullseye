using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

    private GameObject bull;                                                    //the bull
    private List<GameObject> matadorList;                                       //list of matadors
    private List<GameObject> picadorList;                                       //list of picadors
    public GameObject matadorPrefab;                                            //matador prefab
    public GameObject picadorPrefab;                                            //picador prefab
    public GameObject playAgainButton;                                          //play again button
    public GameObject quitButton;                                               //quit button
    private int maxMatadors;                                                    //max number of matadors
    private int maxPicadors;
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
        maxMatadors = 1;                                                        //max matadors starts at 1
        maxPicadors = 0;                                                        //max picadors starts at 0
        matadorList = new List<GameObject>();                                   //create a matador list
        picadorList = new List<GameObject>();                                   //create a picador list 
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

        if (deadMatadors == maxMatadors)                                          //if all matadors are dead
        {
            NextLevel();                                                        //set the next level
            FillMatadors();                                                     //create new matadors
            FillPicadors();
        }

        levelText.text = "Level: " + level;
    }

    private void StartGame()
    {
        quitButton.SetActive(false);                                            //hide quit button
        ScoreManager.score = 0;                                                 //reset score
        level = 1;                                                              //reset level 
        maxMatadors = 1;                                                        //reset maxNumber
        maxPicadors = 0;
        deadMatadors = 0;                                                       //reset number of dead matadors
        TimeMananger.mainTimer = 10f;                                           //reset timer
        TimeMananger.timeFinished = false;
        StartCoroutine(ShowLevel());                                            //show what level
        matadorList.Clear();                                                    //clear matador list
        picadorList.Clear();
        FillMatadors();                                                         //create matadors
        FillPicadors();
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
        GameObject[] picadors = GameObject.FindGameObjectsWithTag("Picador");   //get all picadors
        foreach (GameObject picador in picadors)                                //Destroy all picadors
        {
            Destroy(picador);
        }
        playAgainButton.SetActive(true);                                        //activate play again button
        quitButton.SetActive(true);
        gameOver = false;                                                       //gameOver not anymore
    }


    private void NextLevel()
    {
        level += 1;
        Debug.Log(level % 2);
        if (maxMatadors < 10)
        {
            TimeMananger.mainTimer += 5f;
            maxMatadors++;
        }
        if (maxPicadors < 8 && (level % 2 == 0))
        {
            maxPicadors++;
        }

        StartCoroutine(ShowLevel());
        deadMatadors = 0;
        matadorList.Clear();
        picadorList.Clear();
        GameObject[] picadors = GameObject.FindGameObjectsWithTag("Picador");   //get all picadors
        foreach (GameObject picador in picadors)                                //Destroy all picadors
        {
            Destroy(picador);
        }
    }

    private void FillMatadors()
    {
        for (int i = 0; i < maxMatadors; i++)
        {
            GameObject newMatador = Instantiate(matadorPrefab);
            matadorList.Add(newMatador);
        }
    }

    private void FillPicadors()
    {
        for (int i = 0; i < maxPicadors; i++)
        {
            GameObject newPicador = Instantiate(picadorPrefab);
            picadorList.Add(newPicador);
        }
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
