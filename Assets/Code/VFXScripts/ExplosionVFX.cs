using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionVFX : MonoBehaviour
{
    public AudioClip[] explosionVariations;


    void Awake()
    {
        int randomIndex = Random.Range(0, explosionVariations.Length);

        GetComponent<AudioSource>().clip = explosionVariations[randomIndex];
    }

    void Start()
    {
        GetComponent<AudioSource>().Play();

        Destroy(gameObject, 2.0f);
    }

    
}
