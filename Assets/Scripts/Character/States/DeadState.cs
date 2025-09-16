using Services.AudioService;
using Zenject;

namespace Character.States
{
    public class DeadState : CharacterState
    {
        [Inject] private AudioService _audioService;
        public DeadState(CharacterController controller) : base(controller)
        {
        }

        public override void OnEnter()
        {
            _controller.RaiseStateChanged(CharacterStateType.Dead);
            _audioService.PlayDeath();
            
            if (_controller.Movement != null)
            {
                _controller.Movement.enabled = false;
            }
        }

        public override void Update()
        {
        
        }
    }
}