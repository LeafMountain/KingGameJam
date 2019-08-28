using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloaterScript : AIBase , IEdible
{

    public RuntimeAnimatorController[] animController;

    private Animator myAnim;

    public AudioClip eaten;
    

    void Start()
    {
        GetReferences();
        myAnim = GetComponent<Animator>();
        GetRandomAnimatorController();
       

        direction = GetRandomDirection();
      
    }
    private void GetRandomAnimatorController()
    {
        int rand = Random.Range(0, animController.Length);

        myAnim.runtimeAnimatorController = animController[rand];

    }

    void Update()
    {
        Move();
        CheckBounderies();

    }

    public void OnEaten()
    {
        Died();
    }


    public override void Die()
    {

    }

    private void Died()

    {

        base.Die();

        Vector2 myPosition = transform.position;

        Instantiate(enemyManagerRef.bloodSplatPrefab, myPosition, Quaternion.identity);

        gameManagerRef.audioManagerRef.sfxAudio.PlayOneShot(eaten);

        Destroy(gameObject);

    }

    public override void Move()
    {
        base.Move();
    }

  
}
