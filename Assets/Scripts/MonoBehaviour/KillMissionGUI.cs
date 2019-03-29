using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KillMissionGUI : MonoBehaviour
{
    public TextMeshProUGUI playersAlive, enemiesAlive;
    public GameObject[] players, enemies;

    private void Update()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        playersAlive.text = "Players Alive: " + players.Length;
        enemiesAlive.text = "Enemies Alive: " + enemies.Length;

        if (players.Length <= 0)
        {
            playersAlive.text = "Players lost";
        }
        if (enemies.Length <= 0)
        {
            enemiesAlive.text = "Enemies lost";
        }
    }

}
