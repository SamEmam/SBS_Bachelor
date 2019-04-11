using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangarScene : MonoBehaviour
{
    public SceneFader sceneFader;
    public string mainMenu;

    public void GoToMainMenu()
    {
        sceneFader.FadeTo(mainMenu);
    }
}
