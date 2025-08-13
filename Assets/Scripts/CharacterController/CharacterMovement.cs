using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private CharacterConfig _config;
    [SerializeField] private CharacterController _controller;
    [SerializeField] private float _laneChangeDuration = 0.15f;
    [SerializeField] private AnimationCurve _laneChangeCurve = null;
    
    private int _currentLine = 1;
    private int _queuedDirection = 0;

    private void Awake()
    {
        if (_laneChangeCurve == null || _laneChangeCurve.length == 0)
        {
            _laneChangeCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
        }
    }

    public void Move()
    {
        if (_controller.Parameters.IsChangingLine) return;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            QueueLaneChange(-1);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            QueueLaneChange(1);
        }
    }


    private void QueueLaneChange(int direction)
    {
        if (_controller.Parameters.IsChangingLine)
        {
            int targetIndex = _currentLine + direction;
            if (targetIndex >= 0 && targetIndex <= 2)
            {
                _queuedDirection = direction;
            }
            return;
        }
        StartLaneChange(direction);
    }

    private void StartLaneChange(int direction)
    {
        int targetIndex = _currentLine + direction;
        if (targetIndex < 0 || targetIndex > 2) return;
        
        _currentLine = targetIndex;
        StartCoroutine(ChangeLine(direction));
    }
    
    private IEnumerator ChangeLine(int direction)
    {
        _controller.Parameters.IsChangingLine = true;
        
        Vector3 start = transform.position;
        float targetX = start.x + direction * _config.LaneDistance;
        
        float t = 0f;
        while (t < _laneChangeDuration)
        {
            t += Time.deltaTime;
            float k = Mathf.Clamp01(t / _laneChangeDuration);
            float eased = _laneChangeCurve.Evaluate(k);
            
            float newX = Mathf.LerpUnclamped(start.x, targetX, eased);
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
            
            yield return null;
        }
        
        transform.position = new Vector3(targetX, transform.position.y, transform.position.z);
        _controller.Parameters.IsChangingLine = false;

        if (_queuedDirection != 0)
        {
            int dir = _queuedDirection;
            _queuedDirection = 0;
            StartLaneChange(dir);
        }
    }
}