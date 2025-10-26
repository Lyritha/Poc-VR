using TMPro;
using UnityEngine;

public class PurchasableItem : MonoBehaviour
{
    [SerializeField]
    private int ticketCost = 1;
    [SerializeField]
    private GameObject purchasedItemPrefab;
    [SerializeField]
    private TMP_Text price;

    
    public int TicketCost => ticketCost;
    public GameObject PurchasedItemPrefab => purchasedItemPrefab;


    private TicketShop shop;

    public void Initialize(TicketShop ticketShop)
    {
        shop = ticketShop;
        if (price != null)
        {
            price.text = $"{ticketCost}";
        }
    }

    public void Purchase()
    {
        if (shop != null) shop.PurchaseItem(this);
    }
}
