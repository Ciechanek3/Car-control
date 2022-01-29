using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public event Action StartGame;

    private bool isSpacePressed = false;

    private bool gameStarted = false;

    private void Awake()
    {
        Debug.LogError(isSpacePressed);
    }

    private void Update()
    {
        isSpacePressed = (Input.GetAxis("Brake") != 0) ? true : false;
        Debug.LogError(isSpacePressed);
        StartGameIfButtonPressed();
    }

    private void StartGameIfButtonPressed()
    {
        if (isSpacePressed && gameStarted == false)
        {
            StartGame.Invoke();
            gameStarted = true;
        }
    }
}
