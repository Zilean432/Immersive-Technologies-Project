using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit;

public class XRGunScript1 : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    private GunScript gun;

    void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        gun = GetComponent<GunScript>();

        // Subscribe
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
        grabInteractable.activated.AddListener(OnActivate);
    }

    void OnDestroy()
    {
        // Unsubscribe
        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnRelease);
        grabInteractable.activated.RemoveListener(OnActivate);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        // Optional: any “pick up” logic goes here
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        // Optional: any “drop” logic goes here
    }

    private void OnActivate(ActivateEventArgs args)
    {
        // This fires when the user pulls the trigger while holding the gun
        if (gun != null)
            gun.Shoot();
    }
}
