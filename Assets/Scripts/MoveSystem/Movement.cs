using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 1;

    public bool rotateTowardsMoveDirection;

    public void Move(Vector2 input)
    {
        if(input != Vector2.zero)
        {
            transform.position += (Vector3)(input * speed * Time.deltaTime);

            if(rotateTowardsMoveDirection)
            {
                transform.right = input;
            }
        }
    }
}
