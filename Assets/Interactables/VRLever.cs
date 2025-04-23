using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class VRLever : MonoBehaviour
{
    public float rotationAngle = 45f;
    public float snapThreshold = 10f;
    public float rotateSpeed = 200f;

    public UnityEvent onLeverUp;
    public UnityEvent onLeverDown;

    private Quaternion initialRotation;
    private float currentRotation = 0f;
    private bool wasUp = false;

    private Transform interactor;
    private bool isBeingHeld = false;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;

    void Start()
    {
        initialRotation = transform.localRotation;
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    void OnDestroy()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnRelease);
    }

    void Update()
    {
        if (isBeingHeld && interactor != null)
        {
            Vector3 localInteractorPos = transform.InverseTransformPoint(interactor.position);
            float targetAngle = Mathf.Clamp(localInteractorPos.y * rotateSpeed, -rotationAngle, rotationAngle);

            currentRotation = targetAngle;
            transform.localRotation = initialRotation * Quaternion.Euler(currentRotation, 0f, 0f);
        }
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        interactor = args.interactorObject.transform;
        isBeingHeld = true;
    }

    void OnRelease(SelectExitEventArgs args)
    {
        isBeingHeld = false;
        interactor = null;
        SnapToNearestState();
    }

    void SnapToNearestState()
    {
        bool nowUp = currentRotation > 0;
        float targetAngle = nowUp ? rotationAngle : -rotationAngle;

        currentRotation = targetAngle;
        transform.localRotation = initialRotation * Quaternion.Euler(currentRotation, 0f, 0f);

        if (nowUp != wasUp)
        {
            if (nowUp)
                onLeverUp.Invoke();
            else
                onLeverDown.Invoke();

            wasUp = nowUp;
        }
    }
}
