using UnityEngine;

public class BodyPart : MonoBehaviour, IDamageable
{
    public Vector2 position => transform.position;

    private UnityEngine.Events.UnityAction attackedCallback = null;

    public void Init(UnityEngine.Events.UnityAction attackedCallback, Color color)
    {
        this.attackedCallback = attackedCallback;
        GetComponent<Renderer>().material.SetColor("_MaskColor", color);
    }

    public void SetPositon(Vector2 position)
    {
        transform.position = position;
    }

    void IDamageable.OnAttacked(int damage)
    {
        Debug.Log("You attacked a body part");
        attackedCallback.Invoke();
    }

    private void OnDestroy()
    {
        Destroy(gameObject);
    }
}