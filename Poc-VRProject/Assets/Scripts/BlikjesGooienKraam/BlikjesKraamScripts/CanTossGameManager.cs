using MyBox;
using System.Collections;
using UnityEngine;

public class CanTossGameManager : MonoBehaviour
{
    [SerializeField] CanSpawner spawnCansScript;
    [SerializeField] TextManager textManagerScript;
    [SerializeField] BalSpawner balSpawnerScript;
    [SerializeField] float gameDuration;
    public static CanTossGameManager Instance;
    public bool gameIsActive = false;

    private int score = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    private void Start()
    {
        StartGame();
    }
    public void StartGame()
    {
        //End the game thats already in progress
        if(gameIsActive == true)
        {
            EndGame();
        }
        gameIsActive = true;
        spawnCansScript.RespawnCans();
        balSpawnerScript.RespawnBalls();
        textManagerScript.SetScore();
        StartCoroutine(GameInProgress(gameDuration));
    }
    private void EndGame()
    {
        TicketManager.Instance.AddTicket(score / 100);
        ResetScore();
        balSpawnerScript.DeleteExistingBalls();
        spawnCansScript.DestroyExistingCans();
        gameIsActive = false;
    }
 
    private IEnumerator GameInProgress(float duration)
    {
        float remaining = duration;
        while (remaining > 0)
        {
            remaining -= Time.deltaTime;
            int seconds =  remaining.RoundToInt();
            textManagerScript.SetTimer(seconds);
            yield return null;
        }
        EndGame();
    }

    public int AddScore(int newScore) => score += newScore;

    public int GetCurrentScore() => score;

    public void ResetScore() => score = 0;
}
