using System;
using UnityEngine;

public class InputSource : MonoBehaviour
{
    [SerializeField] private SwipeDetection _swipeDetection;
    public bool Left  { get; private set; }
    public bool Right { get; private set; }
    public bool Up    { get; private set; }
    public bool Down  { get; private set; }
    
    private bool _swipeLeft;
    private bool _swipeRight;
    private bool _swipeUp;
    private bool _swipeDown;

    private void OnEnable()
    {
        SwipeDetection.SwipeEvent += RegisterSwipe;
    }

    private void Update()
    {
        Left = false;
        Right = false;
        Up = false;
        Down = false;
        
        ReadKeyboardInput();
        ReadSwipeInput();
    }
    
    private void OnDisable()
    {
        SwipeDetection.SwipeEvent -= RegisterSwipe;
    }
    
    public void ReadKeyboardInput()
    {
        Left  |= Input.GetKeyDown(KeyCode.LeftArrow)  || Input.GetKeyDown(KeyCode.A);
        Right |= Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D);
        Up    |= Input.GetKeyDown(KeyCode.UpArrow)    || Input.GetKeyDown(KeyCode.W);
        Down  |= Input.GetKeyDown(KeyCode.DownArrow)  || Input.GetKeyDown(KeyCode.S);
    }

    private void ReadSwipeInput()
    {
        Left |= _swipeLeft;
        Right |= _swipeRight;
        Up |= _swipeUp;
        Down |= _swipeDown;
        
        _swipeLeft = false;
        _swipeRight = false;
        _swipeUp = false;
        _swipeDown = false;
    }
    
    public void RegisterSwipe(Vector2 direction)
    {
        Debug.Log($"Swipe registered: {direction}");
        if (direction.x < 0f) _swipeLeft = true;
        if (direction.x > 0f) _swipeRight = true;
        if (direction.y > 0f) _swipeUp = true;
        if (direction.y < 0f) _swipeDown = true;
    }
}
