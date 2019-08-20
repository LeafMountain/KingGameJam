using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kroken : MonoBehaviour, IEdible
{
    public class BodyPart
    {
        public Vector2 position = Vector2.zero;
        public GameObject body = null;
        
        public BodyPart(GameObject bodyPrefab, Vector2 position, Transform parent)
        {
            this.position = position;
            body = GameObject.Instantiate(bodyPrefab, position - new Vector2(parent.right.x, parent.right.y), parent.rotation);
        }

        public void SetPositon(Vector2 position)
        {
            this.position = position;
            body.transform.position = position;
        }
    }

    public float speed = 1;
    public GameObject bodyPrefab = null;
    public float bodySpaceing = 1;
    public float attackRange = 1;

    private List<BodyPart> bodyParts = new List<BodyPart>();
    private Vector2[] paintPositions = new Vector2[MAX_LENGTH];

    const int MAX_LENGTH = 100;

    public void Grow()
    {
        Debug.Log("Trying to grow");

        if(bodyParts.Count >= MAX_LENGTH)
        {
            Debug.Log("Kroken too long");
            return;
        }
        
        Vector2 spawnPositon = transform.position;

        if(bodyParts.Count > 0)
        {
            BodyPart parentBody = bodyParts[bodyParts.Count - 1];
            spawnPositon = parentBody.position - new Vector2(parentBody.body.transform.right.x, parentBody.body.transform.right.y);
        }

        bodyParts.Add(new BodyPart(bodyPrefab, spawnPositon, transform));
    }

    public void OnAttacked(int damage)
    {
        Debug.Log("You attacked me :(");
    }

    public void Attack()
    {
        Debug.Log("Trying to attack");
        Ray2D attackRay = new Ray2D(transform.position, transform.right);
        RaycastHit2D hit;
        if(hit = Physics2D.Raycast(attackRay.origin, attackRay.direction, attackRange))
        {
            IEdible damageable = hit.transform.GetComponent<IEdible>();
            if(damageable != null)
            {
                Debug.Log("Attacked: " + hit.transform.name);
                damageable.OnAttacked(1);
            }
        }
    }

    private void Update()
    {
        Vector2 input = Vector2.zero;
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        Move(input.normalized);

        if(Input.GetKeyDown(KeyCode.T)) {
            Grow();
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            Attack();
        }
    }

    private void Paint()
    {
        paintPositions[0] = transform.position;

        float delta = Vector3.Distance(paintPositions[0], paintPositions[1]);
        
        // Move last point
        Vector2 dirEnd = paintPositions[paintPositions.Length - 1] - paintPositions[paintPositions.Length - 2];
        paintPositions[paintPositions.Length - 1] = paintPositions[paintPositions.Length - 2] + dirEnd.normalized * (bodySpaceing - delta);

        if(delta > bodySpaceing)
        {            
            // shift array
            for (int i = paintPositions.Length - 1; i > 0 ; i--)
            {
                paintPositions[i] = paintPositions[i - 1];
            }

            paintPositions[1] = paintPositions[0];
        }

        for (int i = 0; i < paintPositions.Length - 1; i++)
        {
            Debug.DrawLine(paintPositions[i], paintPositions[i + 1], Color.red);
        }

        UpdateBody();
    }

    private void Move(Vector2 direction)
    {
        if(direction != Vector2.zero)
        {
            Vector2 lastPosition = transform.position;
            transform.position += new Vector3(direction.x, direction.y, 0) * speed * Time.deltaTime;
            transform.right = direction;
        }

        Paint();
    }

    private void UpdateBody()
    {
        if(bodyParts.Count == 0) {
            return;
        }

        float delta = Vector3.Distance(paintPositions[0], paintPositions[1]);

        for (int i = 0; i < bodyParts.Count; i++) {
            
            if(i > 0) {

            Vector2 position = Vector2.Lerp(paintPositions[i + 1], paintPositions[i], delta);
            bodyParts[i].SetPositon(position);

            
                bodyParts[i].body.transform.right = bodyParts[i].position - paintPositions[i - 1];
            }
        }
    }
}
