using System;
using System.Collections.Generic;
using UnityEngine;

public class DriveState : CarBaseState
{
    private float maxSpeed;
    private float maxReverseSpeed;

    protected override void Awake()
    {
        base.Awake();
        maxSpeed = carSpecification.MaxSpeed;
        maxReverseSpeed = carSpecification.MaxReverseSpeed;
        
    }
    public override Type Tick()
    {
        HandleBraking();
        UpdateWheels();
        AddDownforce();
        carEffects.CheckIfSmoking(Input.GetAxis("Vertical") != 0);
        carEffects.SwitchBrakeLights(verticalInput < 0);
        for (int i = 0; i < wheelColliders.Count; i++)
        {
            if (verticalInput > 0 && wheelColliders[i].motorTorque < maxSpeed || verticalInput < 0 && wheelColliders[i].motorTorque > maxReverseSpeed)
            {
                wheelColliders[i].motorTorque += verticalInput * motorForce;
            }
            else
            {
                GapToMaxSpeed(wheelColliders[i]);
                if(verticalInput == 0)
                {
                    return typeof(StopState);
                }              
            }
        }
        HandleHorizontalMovement();
        return null;
    }

    private void GapToMaxSpeed(WheelCollider wheelCollider)
    {
        if (wheelCollider.motorTorque > maxSpeed)
        {
            wheelCollider.motorTorque = maxSpeed;
        }

        if (wheelCollider.motorTorque < maxReverseSpeed)
        {
            wheelCollider.motorTorque = maxReverseSpeed;
        }
    }

    
}
