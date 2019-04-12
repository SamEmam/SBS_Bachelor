using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {


    public bool GameIsWon;
    private bool hasBeenAwarded = false;
    public bool GameIsLost;
    public int missionReward;

    public GameObject gameOverUI;

    public GameObject CompleteLevelUI;

    private void Start()
    {
        GameIsWon = false;
        GameIsLost = false;
    }

    // Update is called once per frame
    void Update () {
        if (GameIsWon)
        {
            WinLevel();
        }

        if (GameIsLost)
        {
            EndGame();
        }
	}

    void EndGame()
    {
        GameIsLost = true;
        gameOverUI.SetActive(true);
    }

    public void WinLevel ()
    {
        if (!hasBeenAwarded)
        {
            hasBeenAwarded = true;
            GameIsWon = true;
            CompleteLevelUI.SetActive(true);
            missionReward += PlayerPrefs.GetInt("Currency", 0);
            PlayerPrefs.SetInt("Currency", missionReward);
        }
        
    }
}
