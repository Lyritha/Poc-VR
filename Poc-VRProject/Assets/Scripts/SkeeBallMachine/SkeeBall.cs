using System.Collections;
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
        StartCoroutine(AddForceToBall());
        Destroy(gameObject, 10);
    }

    IEnumerator AddForceToBall()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        rb.linearVelocity *= 5;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Points>(out Points points))
        {
            TicketManager.Instance.AddTicket(points.GetPoints());
            skeeBallMachine.AddTotalPoints(points.GetPoints());
            Destroy(gameObject, 0.5f);
        }
        else if (other.TryGetComponent<RampDetector>(out RampDetector rampDetector))
        {
            rb.linearVelocity *= rampDetector.addSpeed;
        }
    }
}
