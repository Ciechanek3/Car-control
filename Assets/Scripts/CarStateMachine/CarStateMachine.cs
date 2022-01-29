using System.Collections.Generic;
using UnityEngine;
using System;

public class CarStateMachine : MonoBehaviour
{
    private void Awake()
    {
        InitializeStateMachine();
    }

    private void InitializeStateMachine()
    {
        var states = new Dictionary<Type, CarBaseState>()
        {
            { typeof(StopState), GetComponent<StopState>() },
            { typeof(DriveState), GetComponent<DriveState>() }
        };
        StateMachineManager stateMachineManager = GetComponent<StateMachineManager>();
        stateMachineManager.SetStates(states);
    }
}
