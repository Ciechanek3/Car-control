using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateMachineManager : MonoBehaviour
{
    private Dictionary<Type, CarBaseState> availableStates;

    [SerializeField]
    private CarBaseState currentState;

    public CarBaseState CurrentState { get => currentState; set => currentState = value; }
    public Dictionary<Type, CarBaseState> AvailableStates { get => availableStates; set => availableStates = value; }

    public event Action<CarBaseState> OnStateChanged;

    public void SetStates(Dictionary<Type, CarBaseState> states)
    {
        AvailableStates = states;
    }

    private void OnEnable()
    {
        CurrentState = AvailableStates.Values.First();
    }

    private void FixedUpdate()
    {
        var nextState = CurrentState?.Tick();
        if (nextState != null &&
            nextState != CurrentState?.GetType())
        {
            SwitchToNewState(nextState);
        }
    }

    public void SwitchToNewState(Type nextState)
    {
        CurrentState = AvailableStates[nextState];
        OnStateChanged?.Invoke(CurrentState);
    }
}
