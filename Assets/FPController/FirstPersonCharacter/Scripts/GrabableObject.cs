using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GrabableObject : MonoBehaviour
{
    Vector3 mousePosition;
    public Transform player;
    private float dragSpeedMultiplier = 10;
    private float pickupRange = 10f;
    private bool isRotating = false;
    private bool mouseHold = false;
    private float rotationSpeed = 300f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.useGravity = true;
    }

    void Update()
    {
        if (IsWithinPickupRange())
        {
            if (mouseHold == true)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    isRotating = true;
                }

                if (Input.GetKeyUp(KeyCode.R))
                {
                    isRotating = false;
                }

                if (isRotating)
                {
                    RotateObject();
                }
            }
            else
            {
                isRotating = false;
            }
        }

        if (!isRotating)
        {
            rb.isKinematic = false;
        }

        if (mouseHold)
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    public Vector3 GetMousePos()
    {
        return Camera.main.WorldToScreenPoint(transform.position);
    }

    public void OnMouseDown()
    {
        if (IsWithinPickupRange() && !isRotating)
        {
            mousePosition = Input.mousePosition - GetMousePos();
            mouseHold = true;
            rb.useGravity = false;
        }
    }

    public void OnMouseUp()
    {
        mouseHold = false;
        rb.useGravity = true;
    }

    public void OnMouseDrag()
    {
        if (IsWithinPickupRange() && !isRotating)
        {
            Vector3 objectPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePosition);
            rb.velocity = (objectPosition - rb.transform.position) * dragSpeedMultiplier;
        }
    }

    bool IsWithinPickupRange()
    {
        return Vector3.Distance(player.position, transform.position) <= pickupRange;
    }

    void RotateObject()
    {
        Vector3 mouseDelta = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0f);
        rb.isKinematic = true;

        float xRotation = mouseDelta.x * rotationSpeed * Time.deltaTime;
        float yRotation = mouseDelta.y * rotationSpeed * Time.deltaTime;

        transform.Rotate(Vector3.up, -xRotation, Space.World);
        transform.Rotate(Vector3.right, yRotation, Space.World);
    }
}