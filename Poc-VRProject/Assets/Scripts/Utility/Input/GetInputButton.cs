using UnityEngine;
using System;
using Oculus.Interaction;
using UnityEngine.Events;
using Oculus.Interaction.HandGrab;
using Oculus.Interaction.Input;

public class GetInputButton : MonoBehaviour
{
    public UnityEvent OnButtonPressed;
    public UnityEvent OnButtonReleased;

    [SerializeField]
    private OVRInput.Button targetButton;
    [SerializeField]
    private OVRInput.Controller targetController;

    [SerializeField]
    private HandGrabInteractable grabbable;

    private bool isGrabbed = false;
    private OVRInput.Controller currentController = OVRInput.Controller.None;

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

    private void Update()
    {
        if (!isGrabbed) return; // Only check inputs when grabbed

        if (OVRInput.GetDown(targetButton, targetController))
            OnButtonPressed?.Invoke();

        if (OVRInput.GetUp(targetButton, targetController))
            OnButtonReleased?.Invoke();
    }
}
