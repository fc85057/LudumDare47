using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    // Singleton pattern

    [SerializeField] GameObject pauseMenu;

    public static event Action<int> OnScoreChanged = delegate { };

    int score;
    bool isPaused;

    void Start()
    {
        PlayerController.OnHealthChanged += HealthCheck;
        LevelManager.OnFinalPortalEntered += GameWin;
        Enemy.OnEnemyDie += AddEnemyPoints;
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            ChangeScore(200);
        }
    }

    void HealthCheck(int currentHealth, int maxHealth)
    {
        if (currentHealth <= 0)
        {
            StartCoroutine(GameOver());
        }
    }

    IEnumerator GameOver()
    {
        Debug.Log("Game over.");
        yield return new WaitForSeconds(3f);
        // SceneManager.LoadScene("MainMenu");
    }

    void GameWin()
    {
        Debug.Log("Game won.");
        SceneManager.LoadScene("Outro");
    }

    void AddEnemyPoints(GameObject enemy)
    {
        ChangeScore(enemy.GetComponent<Enemy>().Points);
    }

    void ChangeScore(int points)
    {
        score += points;
        OnScoreChanged(score);
    }

    void Pause()
    {
        if (!isPaused)
        {
            isPaused = true;
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            isPaused = false;
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }
    }

}
