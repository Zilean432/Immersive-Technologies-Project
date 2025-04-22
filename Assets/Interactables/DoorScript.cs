using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public Vector3 localOpenOffset = new Vector3(0, 0, 2f); // Local space offset
    public float slideSpeed = 3f;

    private Vector3 closedPosition;
    private Vector3 openPosition;
    private bool isOpen = false;
    private bool isMoving = false;

    void Start()
    {
        closedPosition = transform.position;
        openPosition = closedPosition + transform.TransformDirection(localOpenOffset);
    }

    void Update()
    {
        if (isMoving)
        {
            Vector3 target = isOpen ? openPosition : closedPosition;
            transform.position = Vector3.MoveTowards(transform.position, target, slideSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, target) < 0.01f)
            {
                transform.position = target;
                isMoving = false;
            }
        }
    }

    public void ToggleDoor()
    {
        isOpen = !isOpen;
        isMoving = true;
    }
}
