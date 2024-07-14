using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerInteract : MonoBehaviour
{
    public GameObject player;
    private float interactRange = 10f;
    private int LayerNumber;
    private FirstPersonController firstPersonController;
    InteractableItem currentInteractableItem;

    void Start()
    {
        LayerNumber = LayerMask.NameToLayer("interactLayer");
        firstPersonController = player.GetComponent<FirstPersonController>();
    }

    void Update()
    {
        CheckInteraction();
        CameraControl();

        if (Input.GetKeyDown(KeyCode.E) && currentInteractableItem != null)
        {
            currentInteractableItem.Interact();
        }
    }

    void CheckInteraction()
    {
        int layerMask = 1 << LayerMask.NameToLayer("Player");
        layerMask = ~layerMask;

        RaycastHit hit;
        Ray rayMouse = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(rayMouse, out hit, interactRange))
        {
            if (hit.collider.tag == "Interactable")
            {
                InteractableItem newInteractable = hit.collider.GetComponent<InteractableItem>();

                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    if (hit.transform.GetComponent<DoorScript.Door>())
                    {
                        hit.transform.GetComponent<DoorScript.Door>().OpenDoor();
                    }

                    if (hit.transform.GetComponent<InteractableItem>())
                    {
                        hit.transform.GetComponent<InteractableItem>().Interact();
                    }

                    // if (hit.transform.GetComponent<AnswerScript>())
                    // {
                    //     hit.transform.GetComponent<AnswerScript>().Answer();
                    // }
                }

                if (currentInteractableItem && newInteractable != currentInteractableItem)
                {
                    currentInteractableItem.DisableOutline();
                }

                if (newInteractable.enabled)
                {
                    setNewCurrentInteractableItem(newInteractable);
                }
                else
                {
                    DisableCurrentInteractableItem();
                }
            }
            else
            {
                DisableCurrentInteractableItem();
            }
        }
        else
        {
            DisableCurrentInteractableItem();
        }
    }

    void setNewCurrentInteractableItem(InteractableItem newInteractable)
    {
        currentInteractableItem = newInteractable;
        currentInteractableItem.EnableOutline();
        HUDController.instance.EnableInteractionText(currentInteractableItem.message);
    }

    void DisableCurrentInteractableItem()
    {
        HUDController.instance.DisableInteractionText();
        if (currentInteractableItem)
        {
            currentInteractableItem.DisableOutline();
            currentInteractableItem = null;
        }
    }

    void CameraControl()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            firstPersonController.EnableCameraControl();
            firstPersonController.m_MouseLook.XSensitivity = 2f;
            firstPersonController.m_MouseLook.YSensitivity = 2f;
        }
        else
        {
            firstPersonController.DisableCameraControl();
            firstPersonController.m_MouseLook.XSensitivity = 0f;
            firstPersonController.m_MouseLook.YSensitivity = 0f;
        }
    }
}