using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private SpawnManager spawnManager;
    private PlayerController player;

    public GameObject titleScreen;
    public GameObject pauseScreen;
    public GameObject restartScreen;
    public GameObject gameScreen;

    public TextMeshProUGUI scoreText;
    private int score;

    private Slider sliderHealth;

    public bool isGameActive = false;
    public float difficultyRate = 1.0f;

    public float healthPlayer;

    private void Start()
    {
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();

        healthPlayer = player.health;

        СhooseDifficulty();
    }

    public void СhooseDifficulty()
    {
        pauseScreen.SetActive(false);
        restartScreen.SetActive(false);
        gameScreen.SetActive(false);

        titleScreen.SetActive(true);

        isGameActive = false;

        ClearScene();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void difficultyEasy()
    {
        difficultyRate = 0.5f;
        GameObject.Find("ButtonEasy").GetComponent<Button>().interactable = false;
        GameObject.Find("ButtonMedium").GetComponent<Button>().interactable = true;
        GameObject.Find("ButtonHard").GetComponent<Button>().interactable = true;
    }
    public void difficultyMedium()
    {
        difficultyRate = 1.0f;
        GameObject.Find("ButtonEasy").GetComponent<Button>().interactable = true;
        GameObject.Find("ButtonMedium").GetComponent<Button>().interactable = false;
        GameObject.Find("ButtonHard").GetComponent<Button>().interactable = true;
    }
    public void difficultyHard()
    {
        difficultyRate = 2.0f;
        GameObject.Find("ButtonEasy").GetComponent<Button>().interactable = true;
        GameObject.Find("ButtonMedium").GetComponent<Button>().interactable = true;
        GameObject.Find("ButtonHard").GetComponent<Button>().interactable = false;
    }

    public void StartGame()
    {
        //Очищаем сцену
        ClearScene();

        //устанавливаем значения
        player.health = healthPlayer;
        score = 0;
        UpdateScore(0);
        isGameActive = true;

        gameScreen.gameObject.SetActive(true);
        titleScreen.gameObject.SetActive(false);
        restartScreen.SetActive(false);

        //обновляем хилсбар
        sliderHealth = GameObject.Find("SliderHealth").GetComponent<Slider>();
        sliderHealth.maxValue = player.health;
        UpdateSliderHealth();

        //запускаем спавн
        spawnManager.StartSpawn();
    }

    public void UpdateSliderHealth()
    {
        sliderHealth.value = player.health;
    }

    public void GameOver()
    {
        isGameActive = false;
        Time.timeScale = 0;

        gameScreen.gameObject.SetActive(false);
        restartScreen.gameObject.SetActive(true);

        //останавливаем спавн
        spawnManager.StopSpawn();
    }

    public void PauseGame()
    {
        if (isGameActive)
        {
            pauseScreen.SetActive(true);
            isGameActive = false;
            Time.timeScale = 0;
        }
        else
        {
            pauseScreen.SetActive(false);
            isGameActive = true;
            Time.timeScale = 1;
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    private void ClearScene()
    {
        Time.timeScale = 1;

        GameObject[] arrayOfObjects = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject objectAr in arrayOfObjects)
        {
            Destroy(objectAr);
        }
        arrayOfObjects = GameObject.FindGameObjectsWithTag("Projectile");
        foreach (GameObject objectAr in arrayOfObjects)
        {
            objectAr.SetActive(false);
        }
        arrayOfObjects = GameObject.FindGameObjectsWithTag("ProjectileEnemy");
        foreach (GameObject objectAr in arrayOfObjects)
        {
            objectAr.SetActive(false);
        }
    }
}
