using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoSingleton<InputManager>
{
    private Direction _currentDirection;
    private static Vector2 _startPosition, _dist;
    private Vector3 _currentPos;

    private enum Direction
    {
        Nothing,
        Up,
        Down,
        Right,
        Left
    }

    public void Move(Transform transform, float speed)
    {
        _currentPos = transform.position;
        Direction input = MovementDirection();

        switch (input)
        {
            case Direction.Up:
                _currentDirection = input;
                if (transform.position == _currentPos)
                    _currentPos += Vector3.forward;
                transform.position = Vector3.MoveTowards(transform.position, _currentPos, Time.deltaTime * speed);
                break;

            case Direction.Down:
                _currentDirection = input;
                if (transform.position == _currentPos)
                    _currentPos += -Vector3.forward;
                transform.position = Vector3.MoveTowards(transform.position, _currentPos, Time.deltaTime * speed);
                break;

            case Direction.Right:
                _currentDirection = input;
                if (transform.position == _currentPos)
                    _currentPos += Vector3.right;
                transform.position = Vector3.MoveTowards(transform.position, _currentPos, Time.deltaTime * speed);
                break;

            case Direction.Left:
                _currentDirection = input;
                if (transform.position == _currentPos)
                    _currentPos += Vector3.left;
                transform.position = Vector3.MoveTowards(transform.position, _currentPos, Time.deltaTime * speed);
                break;

            case Direction.Nothing:
                break;
        }
    }

    private Direction MovementDirection()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                // take start position
                case TouchPhase.Began:
                    if (GameManager.instance.CurrentGameState == GameManager.GameState.Prepare)
                        GameManager.instance.CurrentGameState = GameManager.GameState.MainGame;
                    _startPosition = touch.position;
                    break;

                // take last position
                case TouchPhase.Moved:
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    _dist = touch.position - _startPosition;
                    break;
            }
        }

        // horizontal
        if (Mathf.Abs(_dist.x) > Mathf.Abs(_dist.y))
        {
            if (_dist.x > 0)
                return Direction.Right;
            return Direction.Left;
        }

        // vertical
        else if (Mathf.Abs(_dist.y) > Mathf.Abs(_dist.x))
        {
            if (_dist.y > 0)
                return Direction.Up;
            return Direction.Down;
        }

        return Direction.Nothing;
    }
}