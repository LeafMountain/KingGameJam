using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kroken : MonoBehaviour, IDamageable
{
    public float speed = 1;
    public BodyPart bodyPrefab = null;
    public float bodySpaceing = 1;
    public float attackRange = 1;
    public InputMapping inputMapping = null;
    public Color bodyColor = Color.green;

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
        Quaternion spawnRotation = Quaternion.identity;

        if(bodyParts.Count > 0)
        {
            BodyPart parentBody = bodyParts[bodyParts.Count - 1];
            spawnPositon = parentBody.position - new Vector2(parentBody.transform.right.x, parentBody.transform.right.y);
            spawnRotation = parentBody.transform.rotation;
        }

        BodyPart bodyPart = Instantiate(bodyPrefab, spawnPositon, spawnRotation);
        int damageValue = bodyParts.Count;
        bodyPart.Init(() =>
        {
            OnAttacked(damageValue);
        });

        bodyParts.Add(bodyPart);
    }

    public void OnAttacked(int damage)
    {
        Debug.Log("You attacked me :(");

        if(bodyParts.Count > 0)
        {
            for (int i = bodyParts.Count - 1; i >= damage; i--)
            {
                Destroy(bodyParts[i]);
                bodyParts.RemoveAt(i);
            }
        }
        else
        {
            OnDeath();
        }
    }

    public void Attack()
    {
        GetComponent<Animator>().SetTrigger("Attack");

        Ray2D attackRay = new Ray2D(transform.position + transform.right * (GetComponent<BoxCollider2D>().size.x), transform.right);
        Debug.DrawRay(attackRay.origin, attackRay.direction * attackRange, Color.red);
        RaycastHit2D hit;
        if(hit = Physics2D.Raycast(attackRay.origin, attackRay.direction, attackRange))
        {
            IDamageable damageable = hit.transform.GetComponent<IDamageable>();
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
        Move(inputMapping.GetMovement());

        if(Input.GetKeyDown(KeyCode.KeypadPlus)) {
            Grow();
        }
        if(Input.GetKeyDown(KeyCode.KeypadMinus)) {
            OnAttacked(bodyParts.Count - 1);
        }

        if(inputMapping.GetAttack()) {
            Attack();
        }
    }

    private void OnEnable()
    {
        GameManager.GetInstance().AddPlayer(this);
    }

    private void OnDisable()
    {
        GameManager.GetInstance().RemovePlayer(this);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        IEdible edible = col.transform.GetComponent<IEdible>();
        Debug.Log(edible);
        if(edible != null)
        {
            edible.OnEaten();
            Grow();
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

        // for (int i = 0; i < paintPositions.Length - 1; i++)
        // {
        //     Debug.DrawLine(paintPositions[i], paintPositions[i + 1], Color.red);
        // }

        UpdateBody();
    }

    private void Move(Vector2 direction)
    {
        // Debug.Log(Vector2.Dot(transform.right, direction));
        if(Vector2.Dot(transform.right, direction) > -0.9f)
        {
            float modSpeed = speed;
            GetComponent<Rigidbody2D>().velocity = new Vector3(direction.x, direction.y, 0) * modSpeed;

            if(direction != Vector2.zero)
            {
                Vector2 lastPosition = transform.position;
                transform.right = direction;
            }
        }

        Paint();
    }

    private void UpdateBody()
    {
        if(bodyParts.Count == 0) {
            return;
        }

        float percentageDelta = Mathf.InverseLerp(0, bodySpaceing, Vector3.Distance(paintPositions[0], paintPositions[1]));

        for (int i = 0; i < bodyParts.Count; i++) {
            Vector2 position = Vector2.Lerp(paintPositions[i + 2], paintPositions[i + 1], percentageDelta);
            bodyParts[i].SetPositon(position);
            Vector2 lookDirection = paintPositions[i + 1] - bodyParts[i].position;
            bodyParts[i].transform.right = lookDirection;
        }
    }

    private void OnDeath()
    {
        Destroy(gameObject);
        Debug.Log("I'm dead");
    }
}
