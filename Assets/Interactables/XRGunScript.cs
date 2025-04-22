using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRGunScript : MonoBehaviour
{
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;
    private GunScript gun;

    void Awake()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        gun = GetComponent<GunScript>();

        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        if (gun != null)
        {
            gun.usingXR = true;
            gun.OnPickUp();

            // Optional: Bind XR controller trigger here
            args.interactorObject.transform.TryGetComponent(out ActionBasedController controller);
            if (controller != null)
            {
                controller.activateAction.action.performed += ctx => {
                    if (gun.isActiveAndEnabled) gun.Shoot();
                };
            }
        }
    }

    void OnRelease(SelectExitEventArgs args)
    {
        if (gun != null)
        {
            gun.OnDrop();
        }
    }
}
