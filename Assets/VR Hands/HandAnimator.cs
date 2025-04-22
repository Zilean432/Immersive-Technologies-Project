using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class HandAnimator : MonoBehaviour
{
    [SerializeField] private NearFarInteractor nearFarInteractor;
    [SerializeField] private SkinnedMeshRenderer handMesh;

    private void Awake()
    {
        nearFarInteractor.selectEntered.AddListener(OnGrab);
        nearFarInteractor.selectExited.AddListener(OnRelease);
    }



    private void OnGrab(SelectEnterEventArgs args)
    {
        Debug.Log("Selected");
        handMesh.enabled = false;
    }
    private void OnRelease(SelectExitEventArgs args)
    {
        handMesh.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
