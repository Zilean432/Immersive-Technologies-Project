using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIControl : MonoBehaviour
{
    [SerializeField] private InputActionReference toggleMenu;
    [SerializeField] private GameObject UICanvas;
    private bool UIActive = false;

    // Start is called before the first frame update
    void Start()
    {
        toggleMenu.action.performed += ToggleMenuCanvas;
        UICanvas.SetActive(false);
    }

    private void ToggleMenuCanvas(InputAction.CallbackContext context)
    {
        if (!UIActive)
        {
            UIActive = true;

        }
        else
        {
            UIActive = false;
        }

        UICanvas.SetActive(UIActive);
    }
}
