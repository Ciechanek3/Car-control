using System;
using UnityEngine;

public class StopState : CarBaseState
{
    public override Type Tick()
    {
        HandleBraking();
        AddDownforce();
        HandleHorizontalMovement();
        UpdateWheels();
        if (verticalInput != 0)
        {
            return typeof(DriveState);
        }
        for (int i = 0; i < wheelColliders.Count; i++)
        {
            StopIfNotPressing(wheelColliders[i]);
        }
        return null;
    }

    private void StopIfNotPressing(WheelCollider wheelCollider)
    {
        if (wheelCollider.motorTorque > 0)
        {
            Debug.LogError(wheelCollider.motorTorque);
            wheelCollider.motorTorque -= motorForce * stopMultiplier;
            if (wheelCollider.motorTorque < 0)
            {
                wheelCollider.motorTorque = 0;
            }
        }
        else if (wheelCollider.motorTorque < 0)
        {
            wheelCollider.motorTorque += motorForce * stopMultiplier;
            if (wheelCollider.motorTorque > 0)
            {
                wheelCollider.motorTorque = 0;
            }
        }
    }
}
