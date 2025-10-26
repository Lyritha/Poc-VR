using System;
using TMPro;
using UnityEngine;

public class DisplayTickets : MonoBehaviour
{
    [SerializeField]
    private TMP_Text text;

    private void Start()
    {
        text.text = $"{TicketManager.Instance.TicketCount}";
        TicketManager.Instance.OnTicketCountChanged += UpdateTicketDisplay;
    }

    private void OnDisable()
    {
        if (TicketManager.Instance != null)
            TicketManager.Instance.OnTicketCountChanged -= UpdateTicketDisplay;
    }

    private void UpdateTicketDisplay(int count)
    {
        text.text = $"Tickets: {count}";
    }
}
