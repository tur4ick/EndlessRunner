using System.Collections.Generic;
using Character.States;
using UnityEngine;

namespace Character
{
    public class CharacterStateMachine
    {
        private readonly Dictionary<CharacterStateType, CharacterState> _states;
        private CharacterState _currentState;
        public CharacterStateType CurrentStateType { get; private set; }
        private readonly CharacterController _controller;

        public CharacterStateMachine(CharacterController controller)
        {
            _controller = controller;
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
            _controller.RaiseStateChanged(type);
            _currentState.OnEnter();
        }

        public void Update()
        { 
            _currentState.Update();
        } 
    }
}