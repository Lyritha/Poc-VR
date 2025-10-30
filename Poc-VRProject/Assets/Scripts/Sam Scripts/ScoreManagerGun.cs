using UnityEngine;
using TMPro; // assuming you’re using TextMeshPro for your UI text

public class ScoreManagerGun : MonoBehaviour
{
    public static ScoreManagerGun Instance;

    [SerializeField] private TMP_Text scoreText;

    private int score = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }
}
