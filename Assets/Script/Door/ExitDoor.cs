using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractText : MonoBehaviour
{
    public GameObject interactableObject;
    private InteractableItem interactableItem;
    public string initialMessage = "Text before interaction...";
    public string interactMessage = "Text after interaction!";
    private bool canInteract = true;

    void Start()
    {
        interactableItem = interactableObject.GetComponent<InteractableItem>();
        interactableItem.message = initialMessage;
    }

    public void OnTriggerMessage()
    {
        if (canInteract)
        {
            StartCoroutine(DisplayMessage(5));
        }
    }

    IEnumerator DisplayMessage(int duration)
    {
        interactableItem.message = interactMessage; // Show message
        canInteract = false;
        yield return new WaitForSeconds(duration); // Wait for x seconds
        interactableItem.message = initialMessage; // Reset message
        canInteract = true;
    }
}
