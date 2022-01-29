using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Car/Specification")]
public class CarSpecification : ScriptableObject
{
    [Header("Max Speed")]
    [Range(0, 10000)]
    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    [Range(0, 10000)]
    private float maxReverseSpeed;
    [Header("Forces")]
    [SerializeField]
    private float motorForce;
    [SerializeField]
    private float brakeForce;
    [SerializeField]
    private float downForce;
    [Header("Turning")]
    [SerializeField]
    private float turnRadius;
    [Header("Stopping")]
    [SerializeField]
    private float stopMultiplier;

    public float TurnRadius { get => turnRadius; set => turnRadius = value; }
    public float MaxSpeed { get => maxSpeed; set => maxSpeed = value; }
    public float MaxReverseSpeed { get => -maxReverseSpeed; set => maxReverseSpeed = -value; }
    public float MotorForce { get => motorForce; set => motorForce = value; }
    public float BrakeForce { get => brakeForce; set => brakeForce = value; }
    public float DownForce { get => downForce; set => downForce = value; }
    public float StopMultiplier { get => stopMultiplier; set => stopMultiplier = value; }
}
