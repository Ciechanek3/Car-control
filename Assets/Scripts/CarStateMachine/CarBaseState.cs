using UnityEngine;
using System;
using System.Collections.Generic;

public abstract class CarBaseState : MonoBehaviour
{
    [SerializeField]
    protected List<WheelCollider> wheelColliders;
    [SerializeField]
    protected PlayerInput playerInput;
    [SerializeField]
    protected CarEffects carEffects;
    [SerializeField]
    protected Rigidbody rb;
    [SerializeField]
    protected CarSpecification carSpecification;
    [SerializeField]
    private List<Transform> wheels;

    protected float brakeForce;
    protected float downForce;
    protected float motorForce;
    protected float turnRadius;
    protected float stopMultiplier;

    protected virtual void Awake()
    {
        brakeForce = carSpecification.BrakeForce;
        downForce = carSpecification.DownForce;
        motorForce = carSpecification.MotorForce;
        turnRadius = carSpecification.TurnRadius;
        stopMultiplier = carSpecification.StopMultiplier;
    }

    public abstract Type Tick();

    protected void HandleBraking()
    {
        carEffects.CheckIfBraking(!playerInput.IsBraking);
        carEffects.SwitchBrakeLights(playerInput.IsBraking);
        if (playerInput.IsBraking)
        {
            wheelColliders[2].brakeTorque = brakeForce;
            wheelColliders[3].brakeTorque = brakeForce;
        }
        else
        {
            wheelColliders[2].brakeTorque = 0;
            wheelColliders[3].brakeTorque = 0;
        }
    }

    protected void HandleHorizontalMovement()
    {
        if (playerInput.HorizontalInput > 0)
        {
            wheelColliders[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / turnRadius - (1.5f / 2)) * playerInput.HorizontalInput;
            wheelColliders[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / turnRadius + (1.5f / 2)) * playerInput.HorizontalInput;
        }
        else if (playerInput.HorizontalInput < 0)
        {
            wheelColliders[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / turnRadius + (1.5f / 2)) * playerInput.HorizontalInput;
            wheelColliders[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / turnRadius - (1.5f / 2)) * playerInput.HorizontalInput;
        }
        else
        {
            wheelColliders[0].steerAngle = 0;
            wheelColliders[1].steerAngle = 0;
            rb.angularVelocity = Vector3.zero;
        }
    }

    protected void AddDownforce()
    {
        rb.AddForce(-transform.up * downForce * rb.velocity.magnitude);
    }

    protected void UpdateWheels()
    {
        for (int i = 0; i < wheels.Count; i++)
        {
            Vector3 pos;
            Quaternion rot;
            wheelColliders[i].GetWorldPose(out pos, out rot);
            wheels[i].rotation = rot;
            wheels[i].position = pos;
        }
    }
}
