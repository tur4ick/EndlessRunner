using System.Collections.Generic;
using UnityEngine;

public class CharacterStateMachine
{
    private readonly Dictionary<CharacterStateType, CharacterState> _states;
    private CharacterState _currentState;
    public CharacterStateType CurrentStateType { get; private set; }

    public CharacterStateMachine(CharacterController controller)
    {
        _states = new()
        {
            { CharacterStateType.Idle, new IdleState(controller) },
            { CharacterStateType.Jump, new JumpState(controller) },
            { CharacterStateType.Roll, new RollState(controller) },
            { CharacterStateType.Dead, new DeadState(controller) }
        };
    }
    
    public void ChangeState(CharacterStateType type)
    {
        _currentState?.OnExit();
        _currentState = _states[type];
        CurrentStateType = type;
        Debug.Log($"StateChanged to {type}");
        _currentState.OnEnter();
    }

    public void Update()
    { 
        _currentState.Update();
    } 
}