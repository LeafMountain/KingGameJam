using UnityEngine;

public class FloaterScript : AIBase , IEdible
{

    public RuntimeAnimatorController[] animController;

    private Animator myAnim;

    public AudioClip eaten;
    

    void Start()
    {
        SetUp();
        
        myAnim = GetComponent<Animator>();
        GetRandomAnimatorController();
    }
    private void GetRandomAnimatorController()
    {
        if (animController.Length > 0)
        {
            int rand = Random.Range(0, animController.Length);

            myAnim.runtimeAnimatorController = animController[rand];
        }
    }

    void Update()
    {
        ParentUpdate();
    }

    public void OnEaten()
    {
        Died();
    }

    private void Died()
    {
        base.Die();

        Vector2 myPosition = transform.position;

        Instantiate(enemyManagerRef.bloodSplatPrefab, myPosition, Quaternion.identity);

        gameManagerRef.audioManagerRef.sfxAudio.pitch = Random.Range(0.7f, 0.8f);
        gameManagerRef.audioManagerRef.sfxAudio.PlayOneShot(eaten);

        Destroy(gameObject);

    }


  
}
