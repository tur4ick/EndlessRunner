using UnityEngine;

public abstract class CharacterState
{
    protected CharacterController _controller;
    protected Animator _animator;

    public CharacterState(CharacterController controller)
    {
        _controller = controller;
        _animator = controller.Animator;
    }

    public virtual void OnEnter()
    {
        
    }

    public virtual void OnExit()
    {
        
    }
    public abstract void Update();
}