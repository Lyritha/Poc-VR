using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using UnityEngine;

public class SkeeBall : MonoBehaviour
{
    [SerializeField] HandGrabInteractable grabbableString;
    SkeeBallMachine skeeBallMachine;
    [SerializeField] Rigidbody rb;

    private void Awake()
    {
        skeeBallMachine = FindFirstObjectByType<SkeeBallMachine>();
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
        rb.linearVelocity *= 25f;

        skeeBallMachine.RemoveBall();
        Destroy(gameObject, 10);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Points>(out Points points))
        {
            skeeBallMachine.AddTotalPoints(points.GetPoints());
            print(points.GetPoints());
        }
        else if (other.GetComponent<RampDetector>())
        {
            rb.linearVelocity *= 3;
        }
    }
}
