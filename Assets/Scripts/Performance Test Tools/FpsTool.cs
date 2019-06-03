using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FpsTool : MonoBehaviour
{
    public SpawnTool spawner;

    float deltaTime = 0f;
    float fps = 0f;
    float cooldown = 5f;
    float runTime = 30f;
    int quantity;

    [SerializeField]
    float currentAvgFPS;
    [SerializeField]
    bool isUpdating = false;

    private void Awake()
    {
        Application.targetFrameRate = 300;
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("ShipCount") > 200)
        {
            for (int i = 0; i <= 200; i += 12)
            {
                Debug.Log("FPS with " + i + " ships: " + PlayerPrefs.GetString("FPS_" + i));
            }
        }
        else
        {
            StartCoroutine(CooldownBeforeStart(cooldown));
            StartCoroutine(TimeUntilStop(runTime + cooldown));
        }
    }

    void Update()
    {
        if (!isUpdating)
        {
            return;
        }
        deltaTime += Time.deltaTime;
        deltaTime /= 2f;
        fps = 1f / deltaTime;

        UpdateCumulativeMovingAverageFPS(fps);

    }

    IEnumerator CooldownBeforeStart(float time)
    {
        yield return new WaitForSeconds(time);
        isUpdating = true;
    }
    IEnumerator TimeUntilStop(float time)
    {
        yield return new WaitForSeconds(time);
        isUpdating = false;
        PlayerPrefs.SetString("FPS_" + spawner.amountOfShips, currentAvgFPS.ToString());
        PlayerPrefs.SetInt("ShipCount", spawner.amountOfShips + 12);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void UpdateCumulativeMovingAverageFPS(float newFPS)
    {
        ++quantity;
        currentAvgFPS += (newFPS - currentAvgFPS) / quantity;
    }
}
