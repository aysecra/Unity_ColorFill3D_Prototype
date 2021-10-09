using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    float _timer = 0;
    int direction = 1;
    void FixedUpdate()
    {
        _timer += Time.deltaTime;

        if (_timer < 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + (direction * new Vector3(0, 0, 3)), Time.deltaTime * 5);
        }
        else
        {
            _timer = 0;
            direction *= -1;
        }
    }
}
