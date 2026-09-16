using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text timerText;
    public TMP_Text livesText;

    private void Start()
    {
        GameManager.Instance.OnScoreChanged += UpdateScore;
        GameManager.Instance.OnTimeChanged += UpdateTimer;
        GameManager.Instance.OnLivesChanged += UpdateLives;

        UpdateScore(GameManager.Instance.score);
        UpdateTimer(GameManager.Instance.timeRemaining);
        UpdateLives(GameManager.Instance.lives);
    }

    private void UpdateScore(int score)
    {
        scoreText.text = "Score: " + score;
    }

    private void UpdateTimer(float time)
    {
        timerText.text = "Time: " + Mathf.CeilToInt(time);
    }

    private void UpdateLives(int lives)
    {
        livesText.text = "Lives: " + lives;
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnScoreChanged -= UpdateScore;
            GameManager.Instance.OnTimeChanged -= UpdateTimer;
            GameManager.Instance.OnLivesChanged -= UpdateLives;
        }
    }
}