using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonScript : MonoBehaviour
{
    public UnityEvent onPress;
    public float pressDepth = 0.25f;
    public float pressSpeed = 5f;
    public float interactionRange = 2f;
    public AudioClip buttonSound;

    private Vector3 originalLocalPosition;
    private bool isPressed = false;

    private void Start()
    {
        originalLocalPosition = transform.localPosition;
    }

    private void Update()
    {
        // For Desktop: Detect Mouse Input
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, interactionRange))
            {
                if (hit.collider == GetComponent<Collider>())
                {
                    ToggleButton();
                }
            }
        }

        // Animate button press using local position and local direction
        Vector3 targetLocalPos = isPressed
            ? originalLocalPosition + (-transform.InverseTransformDirection(transform.up) * pressDepth)
            : originalLocalPosition;

        transform.localPosition = Vector3.Lerp(transform.localPosition, targetLocalPos, pressSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Hand"))
        {
            ToggleButton();
        }
    }

    private void ToggleButton()
    {
        isPressed = !isPressed;
        onPress.Invoke();
        AudioSource.PlayClipAtPoint(buttonSound, transform.position);
    }
}