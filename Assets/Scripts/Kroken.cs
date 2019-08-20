using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kroken : MonoBehaviour
{
    public float speed = 1;

    private List<Vector2> bodyPositions = new List<Vector2>();

    void Start()
    {
        
    }

    void Update()
    {
        Vector2 input = Vector2.zero;
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        Move(input.normalized);
    }

    void Move(Vector2 direction)
    {
        transform.position += new Vector3(direction.x, direction.y, 0) * speed * Time.deltaTime;
        transform.right = direction;
    }

    void UpdateBody()
    {

    }
}
