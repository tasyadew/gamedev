using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    [SerializeField] bool isText;
    bool isEnabled;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnEnable()
    {
        isEnabled = true;
    }

    void OnDisable()
    {
        isEnabled = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(isEnabled == true)
        {
            if(isText == true)
            {
                transform.LookAt(Camera.main.transform);
                transform.RotateAround(transform.position, transform.up, 180f);
            }

            else
            {
                Vector3 lookPos = Camera.main.transform.position - transform.position;
                transform.rotation = Quaternion.LookRotation(new Vector3(lookPos.x, 0, lookPos.z));
            }
        }
    }
}
