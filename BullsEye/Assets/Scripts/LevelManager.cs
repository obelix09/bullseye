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
    Text text;

    // Use this for initialization
    void Start()
    {
        level = 1;
        maxNumber = 1;
        matadorList = new List<GameObject>();
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Level: " + level;                  

        if (matadorList.Count < maxNumber)
        {
            GameObject newMatador = Instantiate(matadorPrefab);
            matadorList.Add(newMatador);
        }

        if (deadMatadors == maxNumber)
        {
            level += 1;
            if (maxNumber < 20)
            {
                maxNumber += 1;
            }
            deadMatadors = 0;
            matadorList.Clear();
        }
    }
}
