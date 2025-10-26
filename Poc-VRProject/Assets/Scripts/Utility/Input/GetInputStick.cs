using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using Oculus.Interaction.Input;
using UnityEngine;
using UnityEngine.Events;

public class GetInputStick : MonoBehaviour
{
    public UnityEvent<Vector2> OnStickUpdated;

    [SerializeField]
    private OVRInput.Axis2D targetAxis;
    [SerializeField]
    private OVRInput.Controller targetController;

    [SerializeField]
    private HandGrabInteractable grabbable;

    private bool isGrabbed = false;
    private OVRInput.Controller currentController = OVRInput.Controller.None;

    private Vector2 currentValue = Vector2.zero;

    private void Awake()
    {
        if (grabbable == null) return;

        grabbable.WhenSelectingInteractorAdded.Action += OnGrabBegin;
        grabbable.WhenSelectingInteractorRemoved.Action += OnGrabEnd;
    }

    private void OnDestroy()
    {
        if (grabbable == null) return;

        grabbable.WhenSelectingInteractorAdded.Action -= OnGrabBegin;
        grabbable.WhenSelectingInteractorRemoved.Action -= OnGrabEnd;
    }

    private void OnGrabBegin(IInteractor interactor)
    {
        OVRInput.Controller controller = OVRInput.Controller.None;

        // Check if it’s a hand-driven grab
        if (interactor is HandGrabInteractor handInteractor)
        {
            Handedness handedness = handInteractor.Hand.Handedness; // Left or Right
            controller = handedness == Handedness.Left ? OVRInput.Controller.LTouch : OVRInput.Controller.RTouch;
        }

        currentController = controller;
        isGrabbed = true;
    }

    private void OnGrabEnd(IInteractor interactor)
    {
        isGrabbed = false;
    }

    void Update()
    {
        if (!isGrabbed) return; // Only check inputs when grabbed

        Vector2 stickValue = OVRInput.Get(targetAxis, targetController);
        if (stickValue != currentValue)
        {
            currentValue = stickValue;
            OnStickUpdated?.Invoke(currentValue);
        }
    }
}
