using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickupScript : MonoBehaviour
{
    public float pickupRange = 3f;
    public Transform holdPoint;
    private GunScript heldGun;

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && heldGun == null)
        {
            TryPickup();
        }
        else if (Input.GetMouseButtonDown(1) && heldGun != null)
        {
            DropGun();
        }

        // Keep gun locked to hold point if held
        if (heldGun != null)
        {
            heldGun.transform.position = holdPoint.position;
            heldGun.transform.rotation = holdPoint.rotation;
        }
    }

    void TryPickup()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, pickupRange))
        {
            GunScript gun = hit.collider.GetComponentInParent<GunScript>();
            if (gun != null)
            {
                heldGun = gun;
                heldGun.OnPickUp();
                heldGun.transform.SetParent(holdPoint);
            }
        }
    }

    void DropGun()
    {
        heldGun.transform.SetParent(null);
        heldGun.OnDrop();
        heldGun = null;
    }
}
