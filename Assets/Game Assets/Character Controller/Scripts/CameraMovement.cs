using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public float gamepadSensitivity = 300f;
    public Transform playerBody;

    private float xRotation = 0f;
    public bool AutoCenter = false;

    [HideInInspector] public bool DriveMode;

    private InputAction freeLookAction;
    private InputAction DriveModefreeLookAction;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        freeLookAction = InputsManager.Instance.Controls.Player.FreeLook;
        DriveModefreeLookAction = InputsManager.Instance.Controls.Vehicle.FreeLook;
    }

    void Update()
    {
        Vector2 inputVector;

        if (DriveMode)
        {
            inputVector = DriveModefreeLookAction.ReadValue<Vector2>();
        }
        else
        {
            inputVector = freeLookAction.ReadValue<Vector2>();
        }

        float sensitivity = GetCurrentSensitivity(DriveMode ? DriveModefreeLookAction : freeLookAction);

        float mouseX = inputVector.x * sensitivity * Time.deltaTime;
        float mouseY = inputVector.y * sensitivity * Time.deltaTime;

        if (!DriveMode)
        {
            playerBody.Rotate(Vector3.up * mouseX);

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }
        else
        {
            if (mouseX == 0 && AutoCenter)
            {
                xRotation = Mathf.Lerp(xRotation, 0f, Time.deltaTime * 2f);
            }
            else
            {
                xRotation += mouseX;
            }

            xRotation = Mathf.Clamp(xRotation, -30f, 30f);

            transform.localRotation = Quaternion.Euler(0f, xRotation, 0f);
        }

        int layerMask = LayerMask.GetMask("Player");

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, layerMask))
        {
            // Handle raycast hit
        }
    }

    private float GetCurrentSensitivity(InputAction inputAction)
    {
        if (inputAction.activeControl != null)
        {
            InputDevice activeDevice = inputAction.activeControl.device;

            if (activeDevice is Mouse)
            {
                return mouseSensitivity;
            }
            else if (activeDevice is Gamepad)
            {
                return gamepadSensitivity;
            }
        }

        return mouseSensitivity;
    }
}
