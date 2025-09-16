using System;
using Services.AudioService;
using UnityEngine;
using Zenject;

namespace Character
{
    public class CharacterController : MonoBehaviour
    {
        
        [Inject] private AudioService _audio;

        public event Action OnDead;
        public event Action<CharacterStateType, CharacterStateType> OnStateChanged;
        public event Action OnJumpStarted;
        public event Action OnRollStarted;
        
        public CharacterStateMachine StateMachine { get; private set; }
        public CharacterParameters Parameters { get; private set; }
        [field: SerializeField] public Rigidbody Rigidbody { get; private set; }
        [field: SerializeField] public CharacterConfig Config { get; private set; }
        [field: SerializeField] public CharacterMovement Movement { get; private set; }
        
        private CharacterStateType _lastState;
        private bool _wasGrounded;

    
        private void Start()
        {
            Parameters = new CharacterParameters();
            StateMachine = new CharacterStateMachine(this);
            StateMachine.ChangeState(CharacterStateType.Idle);
            
            _lastState = StateMachine.CurrentStateType;
            _wasGrounded = Parameters.IsGrounded;
        }

        private void Update()
        {
            StateMachine.Update();
            
            CharacterStateType current = StateMachine.CurrentStateType;
            
            if (current != _lastState)
            {
                RaiseStateChanged(current);
                _lastState = current;
            }
            _wasGrounded = Parameters.IsGrounded;
        }
    
        public void Die()
        {
            if (OnDead != null) OnDead();
        }
    
        private void FixedUpdate()
        {
            var origin = transform.position + Vector3.up * Config.GroundOffset; 
            Parameters.IsGrounded = Physics.Raycast(origin, Vector3.down, Config.GroundCheckDistance);
        }
        
        public void RaiseStateChanged(CharacterStateType next)
        {
            
            OnStateChanged?.Invoke(_lastState, next);
            
            if (next == CharacterStateType.Jump)
                OnJumpStarted?.Invoke();
            else if (next == CharacterStateType.Roll)
                OnRollStarted?.Invoke();
            else if (next == CharacterStateType.Dead)
                OnDead?.Invoke();
            
            _lastState = next;
        }
        
        public void RaiseJumpStarted() => OnJumpStarted?.Invoke();
        public void RaiseRollStarted() => OnRollStarted?.Invoke();
    }
}
