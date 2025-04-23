using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GunScript : MonoBehaviour
{
    public Transform firePoint;
    public GameObject projectileBlue;
    public GameObject projectileRed;
    public GameObject projectileGreen;
    public AudioClip shootSound;

    private bool isHeld = false;
    private float firingMode = 0;

    void Update()
    {
        // Desktop testing
        if (isHeld && !usingXR && Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        switch (firingMode)
        {
            case 0:
                GameObject projectile0 = Instantiate(projectileBlue, firePoint.position, firePoint.rotation);
                AudioSource.PlayClipAtPoint(shootSound, firePoint.position);
                break;

            case 1:
                GameObject projectile1 = Instantiate(projectileRed, firePoint.position, firePoint.rotation);
                AudioSource.PlayClipAtPoint(shootSound, firePoint.position);
                break;

            case 2:
                GameObject projectile2 = Instantiate(projectileGreen, firePoint.position, firePoint.rotation);
                AudioSource.PlayClipAtPoint(shootSound, firePoint.position);
                break;
        }
    }

    public void changeModeZero()
    {
        firingMode = 0;
    }

    public void changeModeOne()
    {
        firingMode = 1;
    }

    public void changeModeTwo()
    {
        firingMode = 2;
    }

    // Called by desktop or XR
    public void OnPickUp()
    {
        isHeld = true;
    }

    public void OnDrop()
    {
        isHeld = false;
    }

    // Check if using XR or not
    public bool usingXR = false;
}
