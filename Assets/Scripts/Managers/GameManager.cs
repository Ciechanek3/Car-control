using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public event Action StartGame;

    [SerializeField]
    private PlayerInput playerInput;

    private bool gameStarted = false;

    private void Update()
    {
        StartGameIfButtonPressed();
    }

    private void StartGameIfButtonPressed()
    {
        if (playerInput.IsBraking && gameStarted == false)
        {
            StartGame.Invoke();
            gameStarted = true;
        }
    }
}
