using System.Collections;
using TMPro;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    [SerializeField] TextMeshPro scoreText;
    [SerializeField] TextMeshPro durationText;


    public void AddScore()
    {
        if (!CanTossGameManager.Instance.gameIsActive)
            return;
        CanTossGameManager.Instance.AddScore(100);
        SetScore();
    }
    public void SetScore()
    {
        scoreText.SetText(CanTossGameManager.Instance.GetCurrentScore().ToString());
    }
    
    public void SetTimer(float second)
    {
        durationText.SetText(second.ToString());
    }
}
