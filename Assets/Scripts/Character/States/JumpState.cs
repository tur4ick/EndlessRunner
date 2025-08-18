using UnityEngine;

namespace Character.States
{
    public class JumpState : CharacterState
    {
        public JumpState(CharacterController controller) : base(controller)
        {
        }

        public override void OnEnter()
        {
            if (!_controller.Parameters.IsGrounded) return;
            _animator.SetTrigger("Jump");
            float force = _controller.Config.JumpForce;
            _controller.Rigidbody.AddForce(Vector3.up * force, ForceMode.Impulse);
        }
    
        public override void Update()
        {
            _controller.Movement.Move();
        
            if (_controller.Parameters.IsGrounded)
            {
                _controller.StateMachine.ChangeState(CharacterStateType.Idle);
            }
        }
    
        public override void OnExit()
        {
        }
    }
}
