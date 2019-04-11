using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public string missionsScene, hangarScene;

    public SceneFader sceneFader;

    public void GoToMissions()
    {
        sceneFader.FadeTo(missionsScene);
    }

    public void GoToHangar()
    {
        sceneFader.FadeTo(hangarScene);
    }

    public void Quit()
    {
        Debug.Log("Exciting...");
        Application.Quit();
    }
}
