using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool isGameStarted = false;
    public GameObject enemyPrefab;
    public float spawnInterval = 3.0f;

    public Button startButton;
    public Button restartButton;
    public Button tryAgainButton;

    public GameObject titleScreen;
    public GameObject dieScreen;
    public GameObject winScreen;

    public GameObject HUD;

    private GameObject activeScreen;
    private PlayerHealth playerHealth;

    void Start()
    {
        activeScreen = titleScreen;

        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();

        startButton.onClick.AddListener(StartGame);
        restartButton.onClick.AddListener(StartGame);
        tryAgainButton.onClick.AddListener(StartGame);
    }

    void StartGame()
    {
        playerHealth.RefreshHealth();

        isGameStarted = true;
        activeScreen.SetActive(false);

        StartCoroutine(SpawnTarget());

        Cursor.lockState = CursorLockMode.Locked;

        HUD.SetActive(true);
    }

    public void Die()
    {
        isGameStarted = false;
        dieScreen.SetActive(true);
        HUD.SetActive(false);
        activeScreen = dieScreen;

        Cursor.lockState = CursorLockMode.None;

        // delete all zombies
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Zombie"))
        {
            Destroy(enemy);
        }
    }

    void Win()
    {
        isGameStarted = false;
        winScreen.SetActive(true);
        HUD.SetActive(false);
        activeScreen = winScreen;

        Cursor.lockState = CursorLockMode.None;

        // delete all zombies
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Zombie"))
        {
            Destroy(enemy);
        }
    }

    IEnumerator SpawnTarget()
    {
        while (isGameStarted)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
