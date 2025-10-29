using UnityEngine;

public class SkeeBallMachine : MonoBehaviour
{
    [SerializeField] UpdatePointText updatePointText;
    [SerializeField] GameObject ballObject;
    [SerializeField] Transform ballSpawnPoint;

    int totalPoints = 0;

    int ballCount = 0;
    int maxBallCount = 6;

    void Start()
    {
        SpawnAllBalls();
    }

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

    public void RemoveBall()
    {
        ballCount--;
        if (ballCount <= 0)
        {
            GiveTickets();
        }
    }
    
    public void ResetBallCount() => ballCount = maxBallCount;

    void SpawnAllBalls()
    {
        ResetTotalPoints();
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
    
    void GiveTickets()
    {
        TicketManager.Instance.AddTicket(totalPoints);
    }
}
