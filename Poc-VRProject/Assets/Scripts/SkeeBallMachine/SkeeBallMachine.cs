using System.Collections;
using UnityEngine;

public class SkeeBallMachine : MonoBehaviour
{
    [SerializeField] UpdatePointText updatePointText;
    [SerializeField] GameObject ballObject;
    [SerializeField] Transform ballSpawnPoint;

    bool canStartGame = true;

    int totalPoints = 0;

    int ballCount = 0;
    int maxBallCount = 6;

    public void AddTotalPoints(int pPoints)
    {
        totalPoints += pPoints;
        updatePointText.UpdateText(totalPoints);
    }

    public void ResetTotalPoints()
    {
        totalPoints = 0;
        updatePointText.UpdateText(totalPoints);
    }
    
    public void ResetBallCount() => ballCount = maxBallCount;

    void SpawnAllBalls()
    {
        ResetTotalPoints();
        SkeeBall[] allActiveBalls = FindObjectsByType<SkeeBall>(FindObjectsSortMode.None);
        foreach (var ball in allActiveBalls)
        {
            Destroy(ball.gameObject);
        }
        for (int i = 0; i < maxBallCount; i++)
        {
            Invoke("SpawnBall", 0.5f * i);
        }
    }

    void SpawnBall()
    {
        GameObject newBallObject = Instantiate(ballObject);
        newBallObject.transform.position = ballSpawnPoint.position;
    }

    public void StartGame()
    {
        if (!canStartGame) return;
        canStartGame = false;
        StartCoroutine(StartGameTimer());
        SpawnAllBalls();
        ResetBallCount();
        ResetTotalPoints();
    }

    IEnumerator StartGameTimer()
    {
        yield return new WaitForSeconds(5f);
        canStartGame = true;
    }
}
