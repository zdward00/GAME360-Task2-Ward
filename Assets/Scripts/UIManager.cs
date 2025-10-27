using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_Text scoreText;
    public TMP_Text timerText;
    public TMP_Text stateText;
    //public TMP_Text coinText;
    public GameObject gameOverPanel;
    public GameObject gameStartPanel;


    void Start()
    {
        Debug.Log("✅ UIManager subscribing to events...");

        // Subscribe to events (OBSERVER PATTERN)
        EventManager.Subscribe("OnScoreChanged", UpdateScore);
        EventManager.Subscribe("OnPlayerStateChanged", UpdateStateDisplay);
        EventManager.Subscribe("OnGameOver", ShowGameOver); // GAME OVER
        EventManager.Subscribe("OnGameStart", ShowStart); // Game Start

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
        EventManager.Unsubscribe("OnScoreChanged", UpdateScore);
        EventManager.Unsubscribe("OnPlayerStateChanged", UpdateStateDisplay);
        EventManager.Unsubscribe("OnGameOver", ShowGameOver);
    }

    void UpdateScore(object scoreData)
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + scoreData.ToString();
        }
    }

    void UpdateStateDisplay(object stateData)
    {
        if (stateText != null)
        {
            stateText.text = "State: " + stateData.ToString();
        }
    }

    /*void UpdateCoinCount(object coinValue)
    {
        coinCount++;
        if (coinText != null)
        {
            coinText.text = "Coins: " + coinCount;
        }
    }*/

    void ShowGameOver(object finalScore)
    {
        Debug.Log("🔴 SHOWING GAME OVER PANEL");

        // Show game over panel
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            Debug.Log("Game Over Panel activated");
        }
        else
        {
            Debug.LogError("❌ Game Over Panel is NULL!");
        }
    }

    void ShowStart()
    {
        Debug.Log("🔴 SHOWING START PANEL");

        // Show start panel
        if (gameStartPanel != null)
        {
            gameStartPanel.SetActive(true);
            Debug.Log("Game Start Panel activated");
            Time.timeScale = 0f; // Pause the game
        }
        else
        {
            Debug.LogError("❌ Game Start Panel is NULL!");
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