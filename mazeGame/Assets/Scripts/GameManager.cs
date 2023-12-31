using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public GameObject titleScreen;
    public Button restartButton;
    public GameObject buttons;


    public List<GameObject> targetPrefabs;

    private int score;
    private float spawnRate = 1.5f;
    public bool isGameActive = false;

    private float spaceBetweenSquares = 2.5f;
    private float minValueX = -3.75f; //  x value of the center of the left-most square
    private float minValueY = -3.75f; //  y value of the center of the bottom-most square


    [Header("GamePlay")]
    public PlayerScript playerControllerScript;

    public GameObject spawnPos1;
    public GameObject spawnPos2;
    public GameObject spawnPos3;

    public GameObject level1;
    public GameObject level2;
    public GameObject level3;

    public bool title;
    public GameObject titleSprite;
    

    // Start the game, remove title screen, reset score, and adjust spawnRate based on difficulty button clicked
    public void StartGame(int difficulty)
    {
        spawnRate /= difficulty;
        isGameActive = true;
        StartCoroutine(SpawnTarget());
        score = 0;
        UpdateScore(0);
        titleScreen.SetActive(false);
    }

    // While game is active spawn a random target
    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targetPrefabs.Count);

            if (isGameActive)
            {
                Instantiate(targetPrefabs[index], RandomSpawnPosition(), targetPrefabs[index].transform.rotation);
            }

        }
    }

    // Generate a random spawn position based on a random index from 0 to 3
    Vector3 RandomSpawnPosition()
    {
        float spawnPosX = minValueX + (RandomSquareIndex() * spaceBetweenSquares);
        float spawnPosY = minValueY + (RandomSquareIndex() * spaceBetweenSquares);

        Vector3 spawnPosition = new Vector3(spawnPosX, spawnPosY, 0);
        return spawnPosition;

    }

    // Generates random square index from 0 to 3, which determines which square the target will appear in
    int RandomSquareIndex()
    {
        return Random.Range(0, 4);
    }

    // Update score with value from target clicked
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score; " + score;
    }
    

    private void Update()
    {
        //GameOver
        if (playerControllerScript.isDead == true)
        {
            gameOverText.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
            isGameActive = false;
        }
        else if (playerControllerScript.isDead == false)
        {
            gameOverText.gameObject.SetActive(false);
            restartButton.gameObject.SetActive(false);
            isGameActive = true;
        }

        //levels
        if (playerControllerScript.end1 == true)
        {
            level1.SetActive(false);
            level2.SetActive(true);
            level3.SetActive(false);
        }
        if (playerControllerScript.end2 == true && playerControllerScript.end1 == false)
        {
            level1.SetActive(false);
            level2.SetActive(false);
            level3.SetActive(true);
        }
        if (playerControllerScript.end3 == true && playerControllerScript.end2 == false)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }




}
