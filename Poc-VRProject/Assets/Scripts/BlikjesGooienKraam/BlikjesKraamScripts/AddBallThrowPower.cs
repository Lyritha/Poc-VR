using Oculus.Interaction.HandGrab;
using Oculus.Interaction;
using UnityEngine;
using System.Collections;

public class AddBallThrowPower : MonoBehaviour
{
    [SerializeField]
    private HandGrabInteractable grabbableString;
    [SerializeField] Rigidbody rb;
    private bool isGrabbed = false;
    [SerializeField] float power;

    private void Awake()
    {
        if (grabbableString == null) return;

        grabbableString.WhenSelectingInteractorAdded.Action += OnGrabBegin;
        grabbableString.WhenSelectingInteractorRemoved.Action += OnGrabEnd;
    }

    private void OnDestroy()
    {
        if (grabbableString == null) return;

        grabbableString.WhenSelectingInteractorAdded.Action -= OnGrabBegin;
        grabbableString.WhenSelectingInteractorRemoved.Action -= OnGrabEnd;
    }

    private void OnGrabBegin(IInteractor interactor) => isGrabbed = true;
    private void OnGrabEnd(IInteractor interactor)
    {
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        rb.linearVelocity *= power;
    }
}
