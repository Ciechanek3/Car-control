using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private Transform centerOfMass;
    [SerializeField]
    private GameObject carBehavior;
    [SerializeField]
    private GameManager gameManager;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfMass.localPosition;
    }

    private void OnEnable()
    {
        gameManager.StartGame += EnableCar;
    }

    private void OnDisable()
    {
        gameManager.StartGame += EnableCar;
    }

    public void FlipCar()
    {
        rb.transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    private void EnableCar()
    {
        carBehavior.SetActive(true);
    }
}
