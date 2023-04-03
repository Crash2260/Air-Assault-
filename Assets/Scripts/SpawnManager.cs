using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    //Variables
    public GameObject[] enemyPrefabs;
    public GameObject firstPowerUp;
    public GameObject titleScreen;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreDisplay;
    private int score;
    private float spawnRangeX = 13;
    private float spawnPosZ = 53.0f;
    private float startDelay = 2;
    private float spawnInterval = 2.0f;
    private float tankOffset = 10.0f;
    // Start is called before the first frame update
    public void StartGame(int difficulty)
    {
        spawnInterval /= difficulty;
        score = 0;
        scoreDisplay.gameObject.SetActive(true);
        titleScreen.gameObject.SetActive(false);
        Instantiate(firstPowerUp, new Vector3(0.15f, 2.5f, 7) , firstPowerUp.transform.rotation);
        UpdateScore(0);
        InvokeRepeating("spawnRandomEnemy", startDelay, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void spawnRandomEnemy()
    {
        int enemyIndex = Random.Range(0, enemyPrefabs.Length);

        //Tank spawn behavior

        if (enemyIndex == 2)
        {
            //Random number to spawn on left or right
            float rando = Random.Range(-spawnRangeX, spawnRangeX);
            //Spawn left
            if (rando > 2)
            {
                Vector3 spawnPos = new Vector3(-spawnRangeX + (Random.Range(0, tankOffset)), 0.5f, spawnPosZ);
                Instantiate(enemyPrefabs[enemyIndex], spawnPos, enemyPrefabs[enemyIndex].transform.rotation);
            }
            //Spawn right
            else if (rando < 2)
            {
                Vector3 spawnPos = new Vector3(spawnRangeX - (Random.Range(0, tankOffset)), 0.5f, spawnPosZ);
                Instantiate(enemyPrefabs[enemyIndex], spawnPos, enemyPrefabs[enemyIndex].transform.rotation);
            }
            //Rare chance to do double center spawn
            else
            {
                Vector3 spawnPos = new Vector3(0, 0.5f, spawnPosZ);
                Instantiate(enemyPrefabs[enemyIndex], new Vector3(spawnPos.x + 1, spawnPos.y, spawnPos.z), enemyPrefabs[enemyIndex].transform.rotation);
                Instantiate(enemyPrefabs[enemyIndex], new Vector3(spawnPos.x - 1, spawnPos.y, spawnPos.z), enemyPrefabs[enemyIndex].transform.rotation);
            }
            //Other Enemy Spawns
        }
        else
        {
            Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), Random.Range(1.4f, 2.8f), spawnPosZ);
            Instantiate(enemyPrefabs[enemyIndex], spawnPos, enemyPrefabs[enemyIndex].transform.rotation);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
