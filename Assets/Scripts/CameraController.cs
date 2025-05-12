using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Variables
    public Transform player;
    public Camera cam;
    public MovementController movementController;

    public float mouseSensitivity = 2f;
    float cameraVerticalRotation = 0f;
    public float maxYCameraRotation = 70f;
    // Start is called before the first frame update
    void Start()
    {
        cam.fieldOfView = 60.0f;
        movementController = GetComponent<MovementController>();
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
        if (movementController != null && !movementController.sliding)
        {
            player.Rotate(Vector3.up * inputX);
        }
        cameraEffect();
    }

    void cameraEffect()
    {
        if (movementController.isBoosting)
        {
            if (cam.fieldOfView < 90)
            {
                cam.fieldOfView += 1.2f;
            }
        }

        if (cam.fieldOfView > 60 && !movementController.isBoosting)
        {
            cam.fieldOfView -= 10f;
        }

        if (cam.fieldOfView < 60)
        {
            cam.fieldOfView = 60f;
        }

    }
}

