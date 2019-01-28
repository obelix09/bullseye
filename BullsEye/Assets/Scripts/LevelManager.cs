using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

    private List<GameObject> matadorList;
    public GameObject matadorPrefab;
    private int maxNumber;
    private int level;

    public static int deadMatadors;
    public static bool gameOver;
    public GameObject panel;
    Text infoText;
    Text levelText;

    // Use this for initialization
    void Start()
    {
        level = 1;
        maxNumber = 1;
        matadorList = new List<GameObject>();
        levelText = GetComponent<Text>();
        infoText = GameObject.Find("InfoText").GetComponent<Text>();
        infoText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            GameOver();
        }

        levelText.text = "Level: " + level;                  

        if (matadorList.Count < maxNumber)
        {
            GameObject newMatador = Instantiate(matadorPrefab);
            matadorList.Add(newMatador);
        }

        if (deadMatadors == maxNumber)
        {
            NextLevel();
        }
    }

    private void NextLevel()
    {
        level += 1;
        infoText.text = "Level " + level;
        StartCoroutine(ShowLevel());

        if (maxNumber < 20)
        {
            maxNumber += 1;
        }
        deadMatadors = 0;
        matadorList.Clear();
    }

    private void GameOver()
    {
        infoText.text = "GAME OVER";
        infoText.gameObject.SetActive(true);
        panel.SetActive(true);
    }

    private IEnumerator ShowLevel()
    {
        infoText.gameObject.SetActive(true);
        panel.SetActive(true);
        TimeMananger.canCount = false;
        yield return new WaitForSeconds(3f);
        TimeMananger.resetTimer = true;
        infoText.gameObject.SetActive(false);
        panel.SetActive(false);
    }
}
