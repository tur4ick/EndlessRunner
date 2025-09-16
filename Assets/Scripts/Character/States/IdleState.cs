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

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (_controller.Parameters.IsGrounded)
                    _controller.StateMachine.ChangeState(CharacterStateType.Jump);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (!_controller.Parameters.IsChangingLine)
                    _controller.StateMachine.ChangeState(CharacterStateType.Roll);
            }
        }
    }
}