using UnityEngine;

namespace Character
{
    public abstract class CharacterState
    {
        protected CharacterController _controller;

        public CharacterState(CharacterController controller)
        {
            _controller = controller;
        }

        public virtual void OnEnter()
        {
        
        }

        public virtual void OnExit()
        {
        
        }
        public abstract void Update();
    }
}