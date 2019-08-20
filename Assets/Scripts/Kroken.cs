using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kroken : MonoBehaviour
{
    public class BodyPart
    {
        public Vector2 position = Vector2.zero;
        public GameObject body = null;
        
        public BodyPart(GameObject bodyPrefab, Vector2 position)
        {
            Instantiate(bodyPrefab, position, Quaternion.identity);
        }
    }

    public float speed = 1;
    public GameObject bodyPrefab = null;

    private List<BodyPart> bodyParts = new List<BodyPart>();

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

        UpdateBody();
    }

    void UpdateBody()
    {
        for (int i = bodyParts.Count; i >= 0; i--)
        {
            bodyParts[i].position = bodyParts[i - 1].position;
        }
        bodyParts[0].position = transform.position;
    }

    public void Grow()
    {
        // bodyParts.Add(new BodyPart(bodyPrefab, bodyParts[bodyParts.Count - 1]))

    }
}
