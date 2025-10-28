using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_Text scoreText;
    public TMP_Text timerText;
    public TMP_Text stateText;
    public TMP_Text livesText;
    public TMP_Text enemiesKilledText;
    //public TMP_Text coinText;
    public GameObject gameOverPanel;


    void Start()
    {
        Debug.Log("✅ UIManager subscribing to events...");

        // Subscribe to events (OBSERVER PATTERN)
        EventManager.Subscribe("OnScoreChanged", UpdateScore);
        EventManager.Subscribe("OnLivesChanged", UpdateLives);
        EventManager.Subscribe("OnEnemiesKilledChanged", UpdateEnemiesKilled);
        EventManager.Subscribe("OnPlayerStateChanged", UpdateStateDisplay);
        EventManager.Subscribe("OnGameOver", ShowGameOver); // GAME OVER

        // Initialize
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
            Debug.Log("Game Over Panel disabled");
        }
    }

    /*void Update()
    {
        // Update timer
        if (timerText && GameManager.Instance != null)
        {
            //float time = GameManager.Instance.GetTimeRemaining();
            int minutes = Mathf.FloorToInt(time / 60);
            int seconds = Mathf.FloorToInt(time % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }*/

    void OnDestroy()
    {
        // Unsubscribe to prevent errors on scene change
        EventManager.Subscribe("OnScoreChanged", UpdateScore);
        EventManager.Subscribe("OnLivesChanged", UpdateLives);
        EventManager.Subscribe("OnEnemiesKilledChanged", UpdateEnemiesKilled);
        EventManager.Subscribe("OnPlayerStateChanged", UpdateStateDisplay);
        EventManager.Subscribe("OnGameOver", ShowGameOver); // GAME OVER
    }

    void UpdateScore(object scoreData)
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + scoreData.ToString();
        }
    }

    void UpdateLives(object livesData)
    {
        if (livesText != null)
        {
            livesText.text = "Lives: " + livesData.ToString();
        }
    }

    void UpdateEnemiesKilled(object enemiesKilledData)
    {
        if (enemiesKilledText != null)
        {
            enemiesKilledText.text = "Enemies Killed: " + enemiesKilledData.ToString();
        }
    }

    void UpdateStateDisplay(object stateData)
    {
        if (stateText != null)
        {
            stateText.text = "State: " + stateData.ToString();
        }
        
    }

    public void ShowGameOver(object panel)
    {
        Debug.Log("🔴 SHOWING GAME OVER PANEL");
        // Show game over panel
        if (gameOverPanel != null)
        {
            GameManager.Instance.GameOver();
        }
        else
        {
            Debug.LogError("❌ Game Over Panel is NULL!");
        }
    }


    public void OnRestartButton()
    {
        if (GameManager.Instance != null)
        { 
            GameManager.Instance.RestartGame();
        }
    }
}