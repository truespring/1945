using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float Speed = 3f;

    private Vector2 vec2 = Vector2.down;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(vec2 * (Speed * Time.deltaTime));
    }

    public void Move(Vector2 vec)
    {
        vec2 = vec;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}