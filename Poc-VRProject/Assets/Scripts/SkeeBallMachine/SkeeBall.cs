using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using UnityEngine;

public class SkeeBall : MonoBehaviour
{
    [SerializeField] HandGrabInteractable grabbableString;
    [SerializeField] Rigidbody rb;
 
    private void Awake()
    {
        if (grabbableString == null) return;
 
        grabbableString.WhenSelectingInteractorRemoved.Action += OnGrabEnd;
    }
 
    private void OnDestroy()
    {
        if (grabbableString == null) return;
 
        grabbableString.WhenSelectingInteractorRemoved.Action -= OnGrabEnd;
    }
 
    void OnGrabEnd(IInteractor interactor)
    {
        SkeeBallMachine.Instance.RemoveBall();
        Rigidbody handRb = ((MonoBehaviour)interactor).GetComponent<Rigidbody>();

        float velocityMultiplier = 2f;
        rb.linearVelocity = handRb.linearVelocity * velocityMultiplier;
        rb.angularVelocity = handRb.angularVelocity * velocityMultiplier;

        Destroy(gameObject, 10);
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Points>(out Points points))
        {
            SkeeBallMachine.Instance.AddTotalPoints(points.GetPoints());
            print(points.GetPoints());
        }
    }
}
