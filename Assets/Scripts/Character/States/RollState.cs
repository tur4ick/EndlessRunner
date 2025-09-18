using UnityEngine;

namespace Character.States
{
    public class RollState : CharacterState
    {
        private float _startTime;
        public RollState(CharacterController controller) : base(controller)
        {
        }

        public override void OnEnter()
        {
            _startTime = Time.time;
            _controller.RaiseStateChanged(CharacterStateType.Roll);
            _controller.RaiseRollStarted();
        }

        public override void Update()
        {
            var input = _controller.Input;
            
            if (input.Up)
            {
                _controller.StateMachine.ChangeState(CharacterStateType.Jump);
                return;
            }
        
            float duration = _controller.Config.RollDuration;
            if (Time.time - _startTime >= duration)
            {
                _controller.StateMachine.ChangeState(CharacterStateType.Idle);
            }
        }
    }
}
