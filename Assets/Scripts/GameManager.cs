using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float initialSpawnInterval = 3.0f;
    public int totalWaves = 5;
    public int zombiesPerWave = 5;

    public Button startButton;
    public Button restartButton;
    public Button tryAgainButton;

    public GameObject titleScreen;
    public GameObject dieScreen;
    public GameObject winScreen;

    public GameObject HUD;

    public TextMeshProUGUI waveText;
    public TextMeshProUGUI zombiesRemainingText;

    public bool isGameStarted = false;
    private GameObject activeScreen;
    private PlayerHealth playerHealth;
    private int currentWave = 0;
    private int zombiesRemaining;
    private float spawnInterval;

    void Start()
    {
        activeScreen = titleScreen;
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        AssignButtonListeners();
    }

    void AssignButtonListeners()
    {
        startButton.onClick.AddListener(StartGame);
        restartButton.onClick.AddListener(StartGame);
        tryAgainButton.onClick.AddListener(StartGame);
    }

    void StartGame()
    {
        playerHealth.RefreshHealth();
        isGameStarted = true;
        SwitchScreen(null);
        InitializeGame();
    }

    void InitializeGame()
    {
        currentWave = 0;
        zombiesRemaining = 0;
        Cursor.lockState = CursorLockMode.Locked;
        HUD.SetActive(true);
        StartNextWave();
    }

    public void Die()
    {
        EndGame(dieScreen);
    }

    void Win()
    {
        EndGame(winScreen);
    }

    void EndGame(GameObject endScreen)
    {
        isGameStarted = false;
        SwitchScreen(endScreen);
        HUD.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        DestroyAllEnemies();
    }

    void SwitchScreen(GameObject newScreen)
    {
        if (activeScreen != null)
            activeScreen.SetActive(false);

        if (newScreen != null)
        {
            newScreen.SetActive(true);
            activeScreen = newScreen;
        }
    }

    void DestroyAllEnemies()
    {
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Zombie"))
        {
            Destroy(enemy);
        }
    }

    private IEnumerator SpawnWave(int numberOfZombies)
    {
        zombiesRemaining = numberOfZombies;
        UpdateZombiesRemainingText();
        for (int i = 0; i < numberOfZombies; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void StartNextWave()
    {
        if (currentWave < totalWaves)
        {
            currentWave++;
            UpdateWaveText();
            spawnInterval = initialSpawnInterval / currentWave;
            StartCoroutine(SpawnWave(zombiesPerWave * currentWave));
        }
        else if (zombiesRemaining == 0)
        {
            Win();
        }
    }

    public void ZombieKilled()
    {
        zombiesRemaining--;
        UpdateZombiesRemainingText();

        if (zombiesRemaining <= 0)
        {
            if (currentWave < totalWaves)
                StartNextWave();
            else
                Win();
        }
    }

    void UpdateWaveText()
    {
        waveText.text = $"Wave: {currentWave}";
    }

    void UpdateZombiesRemainingText()
    {
        zombiesRemainingText.text = $"Zombies Remaining: {zombiesRemaining}";
    }
}