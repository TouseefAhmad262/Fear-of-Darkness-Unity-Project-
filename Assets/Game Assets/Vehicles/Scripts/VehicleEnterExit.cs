using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InteractableObject))]
public class VehicleEnterExit : MonoBehaviour
{
    public Transform ExitPoint;


    Vector3 PlayerCamPosition;
    Vector3 PlayerCamRotation;
    void Start()
    {
        InteractableObject Intraction = GetComponent<InteractableObject>();
        Intraction.Title = "Drive";
        Intraction.Action = () => OnIntract();
    }

    void OnIntract()
    {
        VehicleController Controller = GetComponent<VehicleController>();
        Controller.CanControl = true;
        Transform PlayerCamera = Camera.main.transform;
        PlayerCamPosition = PlayerCamera.localPosition;
        PlayerCamRotation = PlayerCamera.localEulerAngles;
        GameManager.Instance.Player.SetActive(false);
        PlayerCamera.gameObject.GetComponent<CameraMovement>().DriveMode = true;
        PlayerCamera.SetParent(Controller.CameraPoint);
        PlayerCamera.localEulerAngles = Vector3.zero;
        PlayerCamera.localPosition = Vector3.zero;

        InputsManager.Instance.Controls.Player.Disable();
        InputsManager.Instance.Controls.Vehicle.Enable();

        GameManager.Instance.Player.transform.SetParent(transform);
        InputsManager.Instance.Controls.Vehicle.Exit.performed += Doxfen => ExitTheCar();
    }

    void ExitTheCar()
    {
        VehicleController Controller = GetComponent<VehicleController>();
        Controller.CanControl = false;
        Transform PlayerCamera = Camera.main.transform;
        GameManager.Instance.Player.SetActive(true);
        PlayerCamera.SetParent(GameManager.Instance.Player.transform);
        PlayerCamera.localEulerAngles = PlayerCamRotation;
        PlayerCamera.localPosition = PlayerCamPosition;
        PlayerCamera.gameObject.GetComponent<CameraMovement>().DriveMode = false;

        InputsManager.Instance.Controls.Vehicle.Exit.performed -= Doxfen => ExitTheCar();
        InputsManager.Instance.Controls.Vehicle.Disable();
        InputsManager.Instance.Controls.Player.Enable();

        GameManager.Instance.Player.transform.SetParent(null);
    }
}