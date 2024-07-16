using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Torchlight : MonoBehaviour
{
    public GameObject CameraObject;
    public GameObject PlayerObject;
    private FirstPersonController FPSController;
    public float smooth = 1.0f;
    public float bobSpeed = 10.0f; // Speed of the bobbing
    public float bobAmount = 0.75f; // Amount of bobbing

    void Start()
    {
        transform.position = CameraObject.transform.position;
        transform.rotation = CameraObject.transform.rotation;
        FPSController = PlayerObject.GetComponent<FirstPersonController>();
    }

    void Update()
    {
        transform.position = CameraObject.transform.position;

        if (FPSController.m_IsWalking)
        {
            // If the player is walking, the torchlight will move slower
            transform.rotation = Quaternion.Slerp(transform.rotation, CameraObject.transform.rotation, Time.deltaTime * 5 * smooth);
        }
        else
        {
            // If the player is running, the torchlight will move faster and bob up and down
            transform.rotation = Quaternion.Slerp(transform.rotation, CameraObject.transform.rotation, Time.deltaTime * 20 * smooth);

            
            // Enable bobbing effect
            if (FPSController.m_CharacterController.velocity.magnitude != 0)
            {
                float bob = Mathf.Sin(Time.time * bobSpeed) * bobAmount;

                Quaternion bobbingRotation = Quaternion.Euler(bob, 0, 0);
                transform.rotation *= bobbingRotation;
            }
        }
    }
}
