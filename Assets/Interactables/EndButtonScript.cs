using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndButtonScript : MonoBehaviour
{
    public UnityEvent onPress;
    public float pressDepth = 0.1f;
    public float pressSpeed = 5f;
    public float interactionRange = 2f;

    public GameObject[] requiredDestroyedObjects = new GameObject[6];
    public MeshRenderer buttonRenderer;
    public Material lockedMaterial;
    public Material unlockedMaterial;
    public AudioClip buttonSound;

    private Vector3 originalLocalPosition;
    private bool isPressed = false;

    private void Start()
    {
        originalLocalPosition = transform.localPosition;
        UpdateMaterial();
    }

    private void Update()
    {
        UpdateMaterial(); // update each frame in case targets are being destroyed

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, interactionRange))
            {
                if (hit.collider == GetComponent<Collider>() && CanPress())
                {
                    ToggleButton();
                }
            }
        }

        Vector3 targetLocalPos = isPressed
            ? originalLocalPosition + (-transform.InverseTransformDirection(transform.up) * pressDepth)
            : originalLocalPosition;

        transform.localPosition = Vector3.Lerp(transform.localPosition, targetLocalPos, pressSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Player") || other.CompareTag("Hand")) && CanPress())
        {
            ToggleButton();
        }
    }

    private bool CanPress()
    {
        foreach (GameObject obj in requiredDestroyedObjects)
        {
            if (obj != null)
                return false;
        }
        return true;
    }

    private void ToggleButton()
    {
        isPressed = !isPressed;
        onPress.Invoke();
        AudioSource.PlayClipAtPoint(buttonSound, transform.position);
    }

    private void UpdateMaterial()
    {
        if (buttonRenderer != null && lockedMaterial != null && unlockedMaterial != null)
        {
            buttonRenderer.material = CanPress() ? unlockedMaterial : lockedMaterial;
        }
    }
}
