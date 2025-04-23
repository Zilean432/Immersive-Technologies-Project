using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonScriptVariant : MonoBehaviour
{
    public UnityEvent onPress;
    public float pressDepth = 0.1f;
    public float pressSpeed = 5f;
    public float interactionRange = 2f;
    public float pressDuration = 0.2f;
    public AudioClip buttonSound;

    private Vector3 originalLocalPos;
    private Vector3 pressedLocalOffset;
    private bool isAnimating = false;

    void Start()
    {
        originalLocalPos = transform.localPosition;
        // We calculate press offset using local space direction
        pressedLocalOffset = -transform.InverseTransformDirection(transform.up) * pressDepth;
    }

    void Update()
    {
        if (!isAnimating && Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, interactionRange))
            {
                if (hit.collider == GetComponent<Collider>())
                {
                    StartCoroutine(PressAndRelease());
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isAnimating)
        {
            if (other.CompareTag("Player") || other.CompareTag("Hand"))
            {
                StartCoroutine(PressAndRelease());
            }
        }
    }

    private IEnumerator PressAndRelease()
    {
        isAnimating = true;

        Vector3 targetLocalPos = originalLocalPos + pressedLocalOffset;

        // Animate down
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * pressSpeed;
            transform.localPosition = Vector3.Lerp(originalLocalPos, targetLocalPos, t);
            yield return null;
        }

        onPress.Invoke();
        AudioSource.PlayClipAtPoint(buttonSound, transform.position);

        yield return new WaitForSeconds(pressDuration);

        // Animate up
        t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * pressSpeed;
            transform.localPosition = Vector3.Lerp(targetLocalPos, originalLocalPos, t);
            yield return null;
        }

        isAnimating = false;
    }
}
