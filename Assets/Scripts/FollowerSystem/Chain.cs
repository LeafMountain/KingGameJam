using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chain : MonoBehaviour
{
    private const int MAXLINKS = 1000;
    public GameObject linkPrefab;
    public float linkSpacing = 1;

    public IntVariable numberOfLinks;
    private int currentNumberOfLinks = 0;

    private GameObject[] links      = new GameObject[MAXLINKS];
    private Vector2[] movePosition  = new Vector2[MAXLINKS];

    void Start()
    {
        links[0] = gameObject;
    }

    public void Update()
    {
        if(currentNumberOfLinks != numberOfLinks.GetValue()) AddLink();

        movePosition[0] = links[0].transform.position;
        for (int i = 1; i < MAXLINKS; i++)
        {
            if(links[i] == null)
                break;

            Vector2 vectorToParentLink = movePosition[i - 1] - movePosition[i];
            if(vectorToParentLink.magnitude > linkSpacing)
            {
                Vector2 newPosition = movePosition[i] + vectorToParentLink * Time.deltaTime * 10;
                links[i].transform.position = newPosition;
                movePosition[i] = newPosition;
                links[i].transform.right = vectorToParentLink.normalized;
            }
        }
    }

    public void AddLink()
    {
        for (int i = 1; i < MAXLINKS; i++)
        {
            if(links[i] != null) continue;

            Vector2 spawnPosition = movePosition[i - 1];
            links[i] = Instantiate(linkPrefab, spawnPosition, Quaternion.identity);
            movePosition[i] = spawnPosition;
            currentNumberOfLinks++;
            break;
        }
    }

    public void RemoveLink()
    {
        for (int i = MAXLINKS - 1; i >= 0 ; i--)
        {
            if(links[i] != null) links[i] = null;
        }
    }
}
