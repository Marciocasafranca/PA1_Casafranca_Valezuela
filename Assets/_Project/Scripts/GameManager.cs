using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int score = 0;
    public int lives = 3;
    public float timeRemaining = 60f;

    public event Action<int> OnScoreChanged;
    public event Action<int> OnLivesChanged;
    public event Action<float> OnTimeChanged;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;

            if (timeRemaining < 0)
            {
                timeRemaining = 0;
            }

            OnTimeChanged?.Invoke(timeRemaining);
        }
    }

    public void AddScore(int points)
    {
        score += points;
        OnScoreChanged?.Invoke(score);
    }

    public void LoseLife()
    {
        if (lives > 0)
        {
            lives--;
            OnLivesChanged?.Invoke(lives);
        }
    }
}