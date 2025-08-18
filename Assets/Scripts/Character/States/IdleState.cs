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
        
            _animator.SetTrigger("Idle");
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

// Использовать параметры. Ограничинить перекаты во время смены линии. ИзГроундед убрать метод. 
// Аниматор и анимации. Вынести мув чарактер в отдельный компонент типа чарактермувмента.
// Базовый класс для препядствий, монеток, бафов. КОторый будет просто проверять, что игрок в тригере. У него будут наследники, которые реализуют один онПлеерЭнтер или типа того.
// Препядствия. Должен быть метод типа демеджа, который наносит урон. Внутри игрока логика со здоровьем игрока.