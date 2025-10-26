using UnityEngine;

public class TicketShop : MonoBehaviour
{
    [SerializeField]
    private Transform purchasedItemSpawnpoint;
    [SerializeField]
    private GameObject itemsContainer;

    private void Start()
    {
        PurchasableItem[] items = itemsContainer.GetComponentsInChildren<PurchasableItem>();
        foreach (PurchasableItem item in items)
        {
            item.Initialize(this);
        }
    }

    public void PurchaseItem(PurchasableItem item)
    {
        if (TicketManager.Instance.UseTickets(item.TicketCost))
        {
            GameObject obj = Instantiate(item.PurchasedItemPrefab, purchasedItemSpawnpoint.position, Quaternion.identity);
        }
    }
}
