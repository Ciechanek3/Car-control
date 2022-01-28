using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTip : MonoBehaviour
{
    [SerializeField]
    private CarMovement carMovement;

    private void OnEnable()
    {
        carMovement.StartGame += DisableToolTip;
    }

    private void OnDisable()
    {
        carMovement.StartGame -= DisableToolTip;
    }

    private void DisableToolTip()
    {
        gameObject.SetActive(false);
    }
}
