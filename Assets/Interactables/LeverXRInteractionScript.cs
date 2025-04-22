using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class LeverXRInteractionScript : MonoBehaviour
{
    public float upAngleThreshold = 30f;
    public float downAngleThreshold = -30f;
    public UnityEvent onLeverUp;
    public UnityEvent onLeverDown;

    private bool wasUp = false;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;

    void Start()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
    }

    void Update()
    {
        if (grabInteractable.isSelected)
        {
            float angle = transform.localEulerAngles.x;
            angle = (angle > 180) ? angle - 360 : angle; // Normalize to -180 to 180

            bool nowUp = angle >= upAngleThreshold;
            bool nowDown = angle <= downAngleThreshold;

            if (nowUp && !wasUp)
            {
                onLeverUp.Invoke();
                wasUp = true;
            }
            else if (nowDown && wasUp)
            {
                onLeverDown.Invoke();
                wasUp = false;
            }
        }
    }
}
