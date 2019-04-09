using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteLevel : MonoBehaviour {

    public string menuSceneName = "MainMenu";
    public string levelSelectSceneName = "LevelSelect";
    public int LevelToUnlock;

    public SceneFader sceneFader;



    public void Continue()
    {
        PlayerPrefs.SetInt("levelReached", LevelToUnlock);
        sceneFader.FadeTo(levelSelectSceneName);
    }

    public void Menu()
    {
        PlayerPrefs.SetInt("levelReached", LevelToUnlock);
        sceneFader.FadeTo(menuSceneName);
    }
}
