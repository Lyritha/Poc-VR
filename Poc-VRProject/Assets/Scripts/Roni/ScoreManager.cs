using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;
    public TextMeshProUGUI scoreText;

    void Start()
    {
        UpdateScoreUI();
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreUI();
    }

    public void ResetScore()
    {
        score = 0;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }
}
