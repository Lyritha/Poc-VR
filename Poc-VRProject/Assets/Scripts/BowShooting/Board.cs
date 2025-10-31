using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField]
    private float boardRadius = 0.5f;
    [SerializeField]
    private float innerRadius = 0.3f;
    [SerializeField]
    private float bullseyeRadius = 0.1f;

    [SerializeField]
    private int boardScore = 10;
    [SerializeField]
    private int innerScore = 15;
    [SerializeField]
    private int bullseyeScore = 25;

    private BowShooting parent;
    public void Initialize(BowShooting parent) => this.parent = parent;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Arrow arrow))
        {
            // Find the closest contact point
            ContactPoint contact = collision.GetContact(0);
            Vector3 hitPoint = contact.point;

            // Distance from center of board
            float distance = Vector3.Distance(hitPoint, transform.position);

            int score = 0;
            if (distance <= bullseyeRadius) score = bullseyeScore;
            else if (distance <= innerRadius) score = innerScore;
            else if (distance <= boardRadius) score = boardScore;

            parent.AddScore(score);
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, boardRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, bullseyeRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, innerRadius);
    }
}
