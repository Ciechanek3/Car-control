using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private bool isBraking;

    public bool IsBraking { get => isBraking; set => isBraking = value; }
    public float HorizontalInput { get => horizontalInput; set => horizontalInput = value; }
    public float VerticalInput { get => verticalInput; set => verticalInput = value; }

    private void FixedUpdate()
    {
        GetInput();
    }
    private void GetInput()
    {
        HorizontalInput = Input.GetAxis("Horizontal");
        VerticalInput = Input.GetAxis("Vertical");
        IsBraking = (Input.GetAxis("Brake") != 0) ? true : false;
    }
}
