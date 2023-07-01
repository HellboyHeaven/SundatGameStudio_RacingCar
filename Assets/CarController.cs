using System;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private float _currentSteerAngle;

    [Header("Settings")]
    [SerializeField] private float motorForce;
    [SerializeField] private float motorForceBack ;
    [SerializeField] private float breakForce;
    [SerializeField] private float maxSteerAngle;

    [Header("Wheels")]
    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider;
    [SerializeField] private WheelCollider rearRightWheelCollider;
    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform;
    [SerializeField] private Transform rearRightWheelTransform;

    public float HorizontalInput { get; set; }
    public float VerticalInput { get; set; }
    public float AngularVelocity { get; private set; }
    public float AccelerationInput { get; private set; }

    public float Rpm { get; set; }


    private void FixedUpdate() {
        HandleMotor();
        HandleSteering();
        UpdateWheels();
        SetState();
    }

    private void HandleMotor() {
        frontLeftWheelCollider.motorTorque = (VerticalInput > 0 ? motorForce : motorForceBack) * VerticalInput;
        frontRightWheelCollider.motorTorque =  (VerticalInput > 0 ? motorForce : motorForceBack) * VerticalInput;
        AccelerationInput = Mathf.Lerp(AccelerationInput, VerticalInput, Time.fixedDeltaTime / 100);
        ApplyBreaking( AngularVelocity * VerticalInput < 0 ? breakForce : 0);
    }
    
    private void HandleSteering() {
        _currentSteerAngle = maxSteerAngle * HorizontalInput;
        frontLeftWheelCollider.steerAngle = _currentSteerAngle;
        frontRightWheelCollider.steerAngle = _currentSteerAngle;
    }

    private void SetState()
    {
        AngularVelocity = (
            frontLeftWheelCollider.rotationSpeed +
            frontRightWheelCollider.rotationSpeed + 
            rearLeftWheelCollider.rotationSpeed + 
            rearRightWheelCollider.rotationSpeed)/4;
        Rpm = (
            frontLeftWheelCollider.rpm +
            frontRightWheelCollider.rpm + 
            rearLeftWheelCollider.rpm + 
            rearRightWheelCollider.rpm)/4;
    }

    private void ApplyBreaking(float force) {
        frontRightWheelCollider.brakeTorque = force;
        frontLeftWheelCollider.brakeTorque = force;
        rearLeftWheelCollider.brakeTorque = force;
        rearRightWheelCollider.brakeTorque = force;
    }

    private void UpdateWheels() {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform) {
        Vector3 pos;
        Quaternion rot; 
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }
}