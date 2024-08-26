using UnityEngine;
using System.Collections.Generic;

public class VehicleController : MonoBehaviour
{
    [System.Serializable]
    public class Wheel
    {
        public Transform wheelTransform;
        public WheelCollider wheelCollider;
        public bool isPowered;
        public bool canSteer;
    }

    public bool CanControl;
    public List<Wheel> wheels;

    public float motorTorque = 1500f;
    public float maxSteerAngle = 30f;
    public float brakeTorque = 3000f;
    public float steerSmoothTime = 0.1f;
    public Vector3 CenterOfMass;
    public Transform CameraPoint;

    public Transform steeringWheel;
    public float maxSteeringWheelRotation = 600f;

    private float inputVertical;
    private float inputHorizontal;
    private float currentSteerAngle;
    private float steerVelocity;

    private Quaternion initialRotation;

    private void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.centerOfMass = CenterOfMass;

        if (steeringWheel != null)
        {
            initialRotation = steeringWheel.localRotation;
        }
    }

    private void FixedUpdate()
    {
        if (CanControl)
        {
            inputVertical = InputsManager.Instance.Controls.Vehicle.Drive.ReadValue<Vector2>().y;
            inputHorizontal = InputsManager.Instance.Controls.Vehicle.Drive.ReadValue<Vector2>().x;
        }

        foreach (Wheel wheel in wheels)
        {
            if (wheel.isPowered)
            {
                wheel.wheelCollider.motorTorque = inputVertical * motorTorque;
            }

            if (wheel.canSteer)
            {
                currentSteerAngle = Mathf.SmoothDamp(currentSteerAngle, inputHorizontal * maxSteerAngle, ref steerVelocity, steerSmoothTime);
                wheel.wheelCollider.steerAngle = currentSteerAngle;
            }

            UpdateWheelPose(wheel);
        }

        RotateSteeringWheel(currentSteerAngle);
    }

    private void UpdateWheelPose(Wheel wheel)
    {
        if (wheel.wheelCollider == null || wheel.wheelTransform == null)
            return;

        Vector3 position;
        Quaternion rotation;
        wheel.wheelCollider.GetWorldPose(out position, out rotation);

        wheel.wheelTransform.position = position;
        wheel.wheelTransform.rotation = rotation;
    }

    private void RotateSteeringWheel(float steerAngle)
    {
        if (steeringWheel != null)
        {
            float targetSteeringRotation = steerAngle / maxSteerAngle * maxSteeringWheelRotation;

            Quaternion targetRotation = initialRotation * Quaternion.Euler(0, targetSteeringRotation, 0);

            steeringWheel.localRotation = targetRotation;
        }
    }
}
