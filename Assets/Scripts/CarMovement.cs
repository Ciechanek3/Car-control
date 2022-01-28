using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [SerializeField]
    private List<WheelCollider> wheelColliders;
    [SerializeField]
    private List<Transform> wheels;
    [SerializeField]
    private Transform centerOfMass;

    [Header("Speed variables")]
    [SerializeField]
    private float motorForce;
    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    private float maxReverseSpeed;
    [SerializeField]
    private float brakeForce;
    [SerializeField]
    private float turnRadius;
    [SerializeField]
    private float downForce;
    [SerializeField]
    private CarEffects carEffects;

    public event Action StartGame;

    private float horizontalInput;
    private float verticalInput;
    private bool isBraking;
    private Rigidbody rb;
    private bool gameStarted = false;

    public bool IsBraking { get => isBraking; set => isBraking = value; }
    public float HorizontalInput { get => horizontalInput; set => horizontalInput = value; }
    public float VerticalInput { get => verticalInput; set => verticalInput = value; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfMass.localPosition;
    }
    private void FixedUpdate()
    {
        GetInput();
        if (gameStarted)
        {
            HandleMovement();
            UpdateWheels();
            AddDownforce();
            ManageCarEffects();
        }
        else
        {
            StartGameIfButtonPressed();
        }
    }

    private void GetInput()
    {
        HorizontalInput = Input.GetAxis("Horizontal");
        VerticalInput = Input.GetAxis("Vertical");
        IsBraking = (Input.GetAxis("Brake") != 0)? true : false;
    }

    private void HandleMovement()
    {
        HandleVerticalMovement();
        HandleHorizontalMovement();
        HandleBraking();
    }

    private void HandleVerticalMovement()
    {
        Debug.LogError(wheelColliders[1].motorTorque);
        for (int i = 0; i < wheelColliders.Count; i++)
        {
            Debug.Log(wheelColliders[i].motorTorque);
            if (VerticalInput > 0 && wheelColliders[i].motorTorque < maxSpeed || VerticalInput < 0 && wheelColliders[i].motorTorque > maxReverseSpeed)
            {
                wheelColliders[i].motorTorque += VerticalInput * motorForce;
            }
            else
            {
                GapToMaxSpeed(wheelColliders[i]);
                StopIfNotPressing(wheelColliders[i]);
            }  
        }
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

    private void StopIfNotPressing(WheelCollider wheelCollider)
    {
        if (wheelCollider.motorTorque > 0)
        {
            wheelCollider.motorTorque -= motorForce * 2;
            if (wheelCollider.motorTorque < 0)
            {
                wheelCollider.motorTorque = 0;
            }
        }
        else if (wheelCollider.motorTorque < 0)
        {
            wheelCollider.motorTorque += motorForce * 2;
            if (wheelCollider.motorTorque > 0)
            {
                wheelCollider.motorTorque = 0;
            }
        }
    }

    private void HandleHorizontalMovement()
    {
        if (HorizontalInput > 0)
        {
            wheelColliders[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / turnRadius - (1.5f / 2)) * HorizontalInput;
            wheelColliders[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / turnRadius + (1.5f / 2)) * HorizontalInput;
        }
        else if (HorizontalInput < 0)
        {
            wheelColliders[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / turnRadius + (1.5f / 2)) * HorizontalInput;
            wheelColliders[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / turnRadius - (1.5f / 2)) * HorizontalInput;
        }
        else
        {
            wheelColliders[0].steerAngle = 0;
            wheelColliders[1].steerAngle = 0;
            rb.angularVelocity = Vector3.zero;
        }
    }
    private void HandleBraking()
    {
        if(IsBraking)
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

    private void UpdateWheels()
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
    private void AddDownforce()
    {
        rb.AddForce(-transform.up * downForce * rb.velocity.magnitude);
    }

    private void ManageCarEffects()
    {
        carEffects.CheckIfBraking(!IsBraking);
        carEffects.CheckIfSmoking(Input.GetAxis("Vertical") != 0);
        carEffects.SwitchBrakeLights(VerticalInput < 0 || IsBraking);
    }

    private void StartGameIfButtonPressed()
    {
        if(IsBraking)
        {
            gameStarted = true;
            StartGame.Invoke();
        }
    }

    public void FlipCar()
    {
        rb.transform.rotation = new Quaternion(0, 0, 0, 0);
    }
}
