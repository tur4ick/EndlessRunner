namespace Character.States
{
    public class DeadState : CharacterState
    {
        public DeadState(CharacterController controller) : base(controller)
        {
        }

        public override void OnEnter()
        {
            _animator.SetTrigger("Dead");
            _controller.Movement.enabled = false;
        }

        public override void Update()
        {
        
        }
    }
}