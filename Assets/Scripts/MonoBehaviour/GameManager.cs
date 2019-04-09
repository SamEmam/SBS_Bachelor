using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {


    public bool GameIsWon;
    public bool GameIsLost;

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
        GameIsWon = true;
        gameOverUI.SetActive(true);
    }

    public void WinLevel ()
    {
        GameIsWon = true;
        CompleteLevelUI.SetActive(true);
    }
}
