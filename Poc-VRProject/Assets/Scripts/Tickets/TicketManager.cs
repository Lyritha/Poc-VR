using System;
using UnityEngine;

public class TicketManager : UnitySingleton<TicketManager>
{
    public event Action<int> OnTicketCountChanged;

    [SerializeField]
    private int startingTickets = 0;

    private int ticketCount = 0;
    public int TicketCount => ticketCount;


    private void Start()
    {
        ticketCount = startingTickets;
        OnTicketCountChanged?.Invoke(ticketCount);
    }

    public void AddTicket(int count)
    {
        ticketCount += count;
        OnTicketCountChanged?.Invoke(ticketCount);
    }
    public bool UseTickets(int count)
    {
        if (ticketCount < count) return false;

        ticketCount -= count;
        OnTicketCountChanged?.Invoke(ticketCount);

        return true;
    }
}
