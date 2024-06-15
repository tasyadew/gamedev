using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSwitcher : MonoBehaviour
{
    public CinemachineVirtualCamera thirdPersonCamera;
    public CinemachineVirtualCamera firstPersonCamera;
    public KeyCode switchKey = KeyCode.Y; // Using 'Y' key

    private bool isThirdPersonActive = true;

    // Start is called before the first frame update
    void Start()
    {
        // Ensure the third person camera is active at the start
        thirdPersonCamera.Priority = 10;
        firstPersonCamera.Priority = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(switchKey))
        {
            SwitchCamera();
        }
    }

    void SwitchCamera()
    {
        if (isThirdPersonActive)
        {
            thirdPersonCamera.Priority = 0;
            firstPersonCamera.Priority = 10;
        }
        else
        {
            thirdPersonCamera.Priority = 10;
            firstPersonCamera.Priority = 0;
        }
        isThirdPersonActive = !isThirdPersonActive;
    }
}
