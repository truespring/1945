using UnityEngine;

public class Homing : MonoBehaviour
{
    public GameObject target; // player
    public float Speed = 3f;
    Vector2 dir;
    Vector2 dirNo;


    void Start()
    {
        // find the Player tag
        target = GameObject.FindGameObjectWithTag("Player");
    
        // A - B -> Player - missile
        dir = target.transform.position - transform.position;

        dirNo = dir.normalized;
    }


    void Update()
    {
        transform.Translate(dirNo * Speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
