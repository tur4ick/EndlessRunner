using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public CharacterStateMachine StateMachine { get; private set; }
    public CharacterParameters Parameters { get; private set; }
    [field: SerializeField] public Rigidbody Rigidbody { get; private set; }
    [field: SerializeField] public CharacterConfig Config { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }  
    [field: SerializeField] public CharacterMovement Movement { get; private set; }

    [SerializeField] private CharacterStateType _currentState;
    [SerializeField] public bool IsGrounded;
    
    private void Start()
    {
        Parameters = new CharacterParameters();
        StateMachine = new CharacterStateMachine(this);
        StateMachine.ChangeState(CharacterStateType.Idle);
    }

    private void Update()
    {
        StateMachine.Update();
        _currentState = StateMachine.CurrentStateType;
        IsGrounded = Parameters.IsGrounded;
    }
    
    public void Die()
    {
        StateMachine.ChangeState(CharacterStateType.Dead);
    }
    
    
    private void FixedUpdate()
    {
        var origin = transform.position + Vector3.up * Config.GroundOffset;
        Parameters.IsGrounded = Physics.Raycast(origin, Vector3.down, Config.GroundCheckDistance);
        Debug.DrawLine(origin, transform.position+Vector3.down*Config.GroundCheckDistance, Parameters.IsGrounded? Color.green : Color.red);
    }
}
