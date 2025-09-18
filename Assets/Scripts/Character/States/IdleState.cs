using UnityEngine;

namespace Character.States
{
    public class IdleState : CharacterState
    {
        public IdleState(CharacterController controller) : base(controller)
        {
        }

        public override void OnEnter()
        {
            _controller.RaiseStateChanged(CharacterStateType.Idle);
        }

        public override void Update()
        {
            _controller.Movement.Move();
            var input = _controller.Input;

            if (input.Up)
            {
                if (_controller.Parameters.IsGrounded)
                    _controller.StateMachine.ChangeState(CharacterStateType.Jump);
            }
            else if (input.Down)
            {
                if (!_controller.Parameters.IsChangingLine)
                    _controller.StateMachine.ChangeState(CharacterStateType.Roll);
            }
        }
    }
}