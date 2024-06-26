using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool isGameStarted = false;
    public GameObject titleScreen;
    public GameObject enemyPrefab;
    public float spawnInterval = 3.0f;
    public Button startButton;

    public GameObject HUD;

    void Start()
    {
        startButton.onClick.AddListener(StartGame);
    }

    void StartGame()
    {
        isGameStarted = true;
        titleScreen.SetActive(false);

        StartCoroutine(SpawnTarget());

        Cursor.lockState = CursorLockMode.Locked;

        HUD.SetActive(true);
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
