using TMPro;
using UnityEngine;

public class HoopTrigger : MonoBehaviour
{
    public TicketManager ticketManager;
    public ParticleSystem coinBoom;
    public TextMeshProUGUI amountText;

    private int hitAmount = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            hitAmount++;
            ticketManager.AddTicket(5);
            coinBoom.Play();

            Debug.Log($"HIT total hits: {hitAmount}");
        }
    }

    private void Update()
    {
      amountText.text = $"{hitAmount}";
    }
}
