using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour
{
    public TMP_Text scoreText;

    private void Start()
    {
        GameManager.Instance.OnScoreChanged += UpdateScore;
        UpdateScore(GameManager.Instance.score);
    }

    private void UpdateScore(int score)
    {
        scoreText.text = "Score: " + score;
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnScoreChanged -= UpdateScore;
        }
    }
}