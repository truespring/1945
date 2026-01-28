using UnityEngine;

public class PBullet : MonoBehaviour
{
    public float Speed = 4.0f;
    public int Attack = 10;
    public GameObject effect;

    void Start()
    {
    }

    void Update()
    {
        // missile move to up 
        transform.Translate(Vector3.up * Speed * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Monster"))
        {
            Monster m = collider.GetComponent<Monster>();
            if (m != null)
            {
                m.Damage(Attack);
            }

            Destroy(gameObject);
        }

        if (collider.CompareTag("Boss"))
        {
            GameObject go = Instantiate(effect, transform.position, Quaternion.identity);
            Destroy(go, 1);
        
            Destroy(gameObject);
        }
    }
}