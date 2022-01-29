using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTip : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;

    private void OnEnable()
    {
        gameManager.StartGame += DisableToolTip;
    }

    private void OnDisable()
    {
        gameManager.StartGame -= DisableToolTip;
    }

    private void DisableToolTip()
    {
        gameObject.SetActive(false);
    }
}
