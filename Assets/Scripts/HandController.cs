using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
   public KeyCode moveLeftKey = KeyCode.A;
    public KeyCode moveBackwardKey = KeyCode.S;
    public KeyCode moveForwardKey = KeyCode.W;
    public KeyCode moveRightKey = KeyCode.D;
    public KeyCode actionKey = KeyCode.Mouse0; // Left mouse button
    public float handSpeed = 5f;
    public float moveSpeed = 0.1f;

    private Animator animator;
    private Vector3 initialPosition;

    void Start()
    {
        animator = GetComponent<Animator>();
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        if (Input.GetKey(moveLeftKey))
        {
            MoveHand(Vector3.left);
        }
        if (Input.GetKey(moveBackwardKey))
        {
            MoveHand(Vector3.back);
        }
        if (Input.GetKey(moveForwardKey))
        {
            MoveHand(Vector3.forward);
        }
        if (Input.GetKey(moveRightKey))
        {
            MoveHand(Vector3.right);
        }
        if (Input.GetKeyDown(actionKey))
        {
            PerformAction();
        }
    }

    void MoveHand(Vector3 direction)
    {
        transform.localPosition += direction * moveSpeed * Time.deltaTime;
    }

    void PerformAction()
    {
        // Example action: closing and opening hand
        if (animator.GetFloat("Grip") < 0.5f)
        {
            CloseHand();
        }
        else
        {
            OpenHand();
        }
    }

    void OpenHand()
    {
        animator.SetFloat("Grip", Mathf.Lerp(animator.GetFloat("Grip"), 0f, Time.deltaTime * handSpeed));
    }

    void CloseHand()
    {
        animator.SetFloat("Grip", Mathf.Lerp(animator.GetFloat("Grip"), 1f, Time.deltaTime * handSpeed));
    }
}
