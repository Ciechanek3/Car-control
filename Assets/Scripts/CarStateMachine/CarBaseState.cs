using UnityEngine;
using System;
using System.Collections.Generic;

public abstract class CarBaseState : MonoBehaviour
{
    [SerializeField]
    protected List<WheelCollider> wheelColliders;
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
    protected float horizontalInput;
    protected float verticalInput;
    protected bool isBraking;

    protected virtual void Awake()
    {
        brakeForce = carSpecification.BrakeForce;
        downForce = carSpecification.DownForce;
        motorForce = carSpecification.MotorForce;
        turnRadius = carSpecification.TurnRadius;
        stopMultiplier = carSpecification.StopMultiplier;  
    }

    protected void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        isBraking = (Input.GetAxis("Brake") != 0) ? true : false;
    }

    public abstract Type Tick();

    protected void HandleBraking()
    {
        carEffects.CheckIfBraking(!isBraking);
        carEffects.SwitchBrakeLights(isBraking);
        if (isBraking)
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
        if (horizontalInput > 0)
        {
            wheelColliders[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / turnRadius - (1.5f / 2)) * horizontalInput;
            wheelColliders[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / turnRadius + (1.5f / 2)) * horizontalInput;
        }
        else if (horizontalInput < 0)
        {
            wheelColliders[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / turnRadius + (1.5f / 2)) * horizontalInput;
            wheelColliders[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / turnRadius - (1.5f / 2)) * horizontalInput;
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
