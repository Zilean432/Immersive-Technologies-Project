using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LeverScript : MonoBehaviour
{
    public float rotationAngle = 45f;
    public float snapThreshold = 10f;
    public float rotateSpeed = 200f;

    public UnityEvent onLeverUp;
    public UnityEvent onLeverDown;

    private float currentRotation = 0f;
    private bool isDragging = false;
    private bool wasUp = false;
    private Quaternion initialRotation;

    void Start()
    {
        initialRotation = transform.localRotation;
    }

    void Update()
    {
        HandleMouseInput();
    }

    void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider == GetComponent<Collider>())
                {
                    isDragging = true;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            SnapToNearestState();
        }

        if (isDragging)
        {
            float mouseY = Input.GetAxis("Mouse Y");
            currentRotation -= mouseY * Time.deltaTime * rotateSpeed;
            currentRotation = Mathf.Clamp(currentRotation, -rotationAngle, rotationAngle);

            transform.localRotation = initialRotation * Quaternion.Euler(currentRotation, 0f, 0f);
        }
    }

    void SnapToNearestState()
    {
        bool nowUp = currentRotation > 0;
        float targetAngle = nowUp ? rotationAngle : -rotationAngle;

        currentRotation = targetAngle;
        transform.localRotation = initialRotation * Quaternion.Euler(currentRotation, 0f, 0f);

        if (nowUp != wasUp)
        {
            if (nowUp)
                onLeverUp.Invoke();
            else
                onLeverDown.Invoke();

            wasUp = nowUp;
        }
    }
}
