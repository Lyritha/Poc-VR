using UnityEngine;

public class GameManager : MonoBehaviour
{
    public RingSpawner ringSpawner;
    public ScoreManager scoreManager;

    public void RestartGame()
    {
        scoreManager.ResetScore();

        foreach (Transform child in ringSpawner.transform)
        {
            Destroy(child.gameObject);
        }

        ringSpawner.SpawnRings();
    }
}
