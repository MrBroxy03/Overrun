using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Variables
    public Transform player;

    public float mouseSensitivity = 2f;
    float cameraVerticalRotation = 0f;
    public float maxYCameraRotation = 70f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float inputY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        cameraVerticalRotation -= inputY;
        if (cameraVerticalRotation > maxYCameraRotation || cameraVerticalRotation < -maxYCameraRotation)
        {
            if (cameraVerticalRotation < 0f) 
            {
                cameraVerticalRotation = -maxYCameraRotation;
            }
            else
            {
                cameraVerticalRotation = maxYCameraRotation;
            }
        } 
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -90f, 90f);
        Camera.main.transform.localEulerAngles = Vector3.right * cameraVerticalRotation;
        player.Rotate(Vector3.up * inputX);
    }
}

