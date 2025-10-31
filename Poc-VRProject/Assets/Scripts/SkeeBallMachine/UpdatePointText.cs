using TMPro;
using UnityEngine;

public class UpdatePointText : MonoBehaviour
{
    [SerializeField] TMP_Text pointText;

    public void UpdateText(int pTotalPoints)
    {
        pointText.text = pTotalPoints.ToString();
    }
}
