using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HMDManager : MonoBehaviour
{
    [SerializeField] GameObject xrPlayer;
    [SerializeField] GameObject fpsPlayer;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Using HMD:" + XRSettings.loadedDeviceName);
        if (XRSettings.isDeviceActive || XRSettings.loadedDeviceName == "Open XR Display")
        {
            Debug.Log("Using Device XR Player with HMD:" + XRSettings.loadedDeviceName);
            fpsPlayer.SetActive(false);
            xrPlayer.SetActive(true);
        }
        else
        {
            Debug.Log("No HMD Detected, using FPS player");
            fpsPlayer.SetActive(true);
            xrPlayer.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
