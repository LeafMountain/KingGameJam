using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kroken : MonoBehaviour, IDamageable
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

    private List<BodyPart> bodyParts = new List<BodyPart>();

    public void Grow()
    {
        Debug.Log("Trying to grow");

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

    private void Update()
    {
        Vector2 input = Vector2.zero;
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        Move(input.normalized);

        if(Input.GetKeyDown(KeyCode.T)) {
            Grow();
        }
    }

    Vector3 lastPaintPosition = Vector3.zero;
    float currentPaintDelta = 0;
    Vector3[] paintPositions = new Vector3[10];
    int currentPaintIndex = 0;

    void Paint()
    {
        float delta = Vector3.Distance(paintPositions[0], paintPositions[1]);

        if(delta > .5f)
        {
            paintPositions[currentPaintIndex] = transform.position;
            currentPaintDelta = 0;
            currentPaintIndex++;
            currentPaintIndex %= 10;
            lastPaintPosition = transform.position;
        }

        for (int i = 0; i < paintPositions.Length - 1; i++)
        {
            Debug.DrawLine(paintPositions[i], paintPositions[i + 1], Color.red);
        }

        GetComponent<LineRenderer>().positionCount = 10;
        GetComponent<LineRenderer>().SetPositions(paintPositions);
    }

    private void Move(Vector2 direction)
    {
        if(direction != Vector2.zero)
        {
            Vector2 lastPosition = transform.position;
            transform.position += new Vector3(direction.x, direction.y, 0) * speed * Time.deltaTime;
            transform.right = direction;
            UpdateBody(lastPosition);
            Paint();
        }
    }

    private void UpdateBody(Vector2 firstPosition)
    {
        if(bodyParts.Count == 0) {
            return;
        }

        Vector2 parentPosition = bodyParts[0].position;
        bodyParts[0].SetPositon(firstPosition);
        for (int i = 1; i < bodyParts.Count; i++)
        {
            Vector2 tempPosition = parentPosition;
            parentPosition = bodyParts[i].position;
            bodyParts[i].SetPositon(tempPosition);
        }
    }
}
