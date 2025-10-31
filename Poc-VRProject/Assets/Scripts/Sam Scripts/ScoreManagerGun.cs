using UnityEngine;
using TMPro; // assuming you’re using TextMeshPro for your UI text

public class ScoreManagerGun : MonoBehaviour
{
    public static ScoreManagerGun Instance;

    [SerializeField] public TMP_Text scoreText;
    [SerializeField] private GameObject shotGun;
    [SerializeField] private Transform shotGunSpawnPoint;
    [SerializeField] public GunTargetSpawner GunTargetSpawner;
    private GunShot gunShot;
    public int score = 0;
    public bool isGameRunning = false;

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
        if (GunTargetSpawner.spawnCount == -1)
        {
            return;
        }
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }

    public void StartGame()
    {
        if (isGameRunning)
        {
            return;
        }
        isGameRunning = true;
        scoreText.text = "Ready, Set,";
        gunShot = FindAnyObjectByType<GunShot>();
        GunTargetSpawner.gameStarted = true;
        gunShot.SelfDestruct();
        Instantiate(shotGun, shotGunSpawnPoint);
    }

    public void EndGame()
    {
        isGameRunning = false;
        TicketManager.Instance.AddTicket(score);
    }
}
