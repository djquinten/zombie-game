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

    public GameObject hud;

    public TextMeshProUGUI waveText;
    public TextMeshProUGUI zombiesRemainingText;

    public bool isGameStarted;
    private GameObject _activeScreen;
    private PlayerHealth _playerHealth;
    private int _currentWave = 0;
    private int _zombiesRemaining;
    private float _spawnInterval;

    private void Start()
    {
        _activeScreen = titleScreen;
        _playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();

        startButton.onClick.AddListener(StartGame);
        restartButton.onClick.AddListener(StartGame);
        tryAgainButton.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        _playerHealth.RefreshHealth();
        isGameStarted = true;
        SwitchScreen(null);
        InitializeGame();
    }

    private void InitializeGame()
    {
        _currentWave = 0;
        _zombiesRemaining = 0;
        Cursor.lockState = CursorLockMode.Locked;
        hud.SetActive(true);
        StartNextWave();
    }

    public void Die()
    {
        EndGame(dieScreen);
    }

    private void Win()
    {
        EndGame(winScreen);
    }

    private void EndGame(GameObject endScreen)
    {
        isGameStarted = false;
        SwitchScreen(endScreen);
        hud.SetActive(false);
        Cursor.lockState = CursorLockMode.None;

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Zombie"))
        {
            Destroy(enemy);
        }
    }

    private void SwitchScreen(GameObject newScreen)
    {
        if (_activeScreen)
        {
            _activeScreen.SetActive(false);
        }

        if (! newScreen) return;

        newScreen.SetActive(true);
        _activeScreen = newScreen;
    }

    // Coroutine to spawn a wave of zombies with a delay between each spawn
    private IEnumerator SpawnWave(int numberOfZombies)
    {
        _zombiesRemaining = numberOfZombies;
        UpdateZombiesRemainingText();

        for (var i = 0; i < numberOfZombies; i++)
        {
            if (!isGameStarted)
            {
                break;
            }

            Vector3 spawnPosition = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(_spawnInterval);
        }
    }

    private void StartNextWave()
    {
        if (_currentWave < totalWaves && isGameStarted)
        {
            _currentWave++;
            UpdateWaveText();
            _spawnInterval = initialSpawnInterval / _currentWave;
            StartCoroutine(SpawnWave(zombiesPerWave * _currentWave));
        }
        else if (_zombiesRemaining == 0 && isGameStarted)
        {
            Win();
        }
    }

    public void ZombieKilled()
    {
        _zombiesRemaining--;
        UpdateZombiesRemainingText();

        if (_zombiesRemaining > 0) return;

        if (_currentWave < totalWaves)
        {
            StartNextWave();
        }
        else
        {
            Win();
        }
    }

    private void UpdateWaveText()
    {
        waveText.text = $"Wave: {_currentWave}";
    }

    private void UpdateZombiesRemainingText()
    {
        zombiesRemainingText.text = $"Zombies Remaining: {_zombiesRemaining}";
    }
}