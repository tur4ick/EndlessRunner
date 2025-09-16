using System;
using Services.AudioService;
using UnityEngine;
using Zenject;

namespace Character
{
    public class CharacterVisuals : MonoBehaviour
    {
        [Inject] private AudioService _audioService;

        [field: SerializeField] private Animator _animator;
        
        private CharacterController _controller;
        private bool _initialized;
        
        public void Initialize(CharacterController controller)
        {
            if (_initialized)
            {
                Unsubscribe();
            }

            _controller = controller;
            Subscribe();

            _initialized = true;
        }
        
        private void Update()
        {
            if (_animator == null || _controller == null) return;

            _animator.SetBool("IsGrounded", _controller.Parameters.IsGrounded);
        }

        private void Subscribe()
        {
            if (_controller == null) return;

            _controller.OnStateChanged += OnStateChanged;
            _controller.OnJumpStarted += OnJumpStarted;
            _controller.OnRollStarted += OnRollStarted;
            _controller.OnDead += OnDied;
        }

        private void Unsubscribe()
        {
            if (_controller == null) return;

            _controller.OnStateChanged += OnStateChanged;
            _controller.OnJumpStarted -= OnJumpStarted;
            _controller.OnRollStarted -= OnRollStarted;
            _controller.OnDead -= OnDied;
        }


        private void OnStateChanged(CharacterStateType prev, CharacterStateType next)
        {
            if (next == CharacterStateType.Idle)
            {
                _animator.SetTrigger("Idle");
            }
        }

        private void OnJumpStarted()
        {
            _animator.SetTrigger("Jump");
        }


        private void OnRollStarted()
        {
            _animator.SetTrigger("Roll");
        }

        
        private void OnDied()
        {
            _animator.SetTrigger("Dead");
            _audioService.PlayDeath();
        }

        public void Dispose()
        {
            Unsubscribe();
            _controller = null;
            _initialized = false;
        }
    }
}