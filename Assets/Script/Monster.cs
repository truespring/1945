using UnityEngine;

public class Monster : MonoBehaviour
{
    public int HP = 100;
    public float Speed = 0.8f;
    public float Delay = 3f;
    public Transform ms1;
    public Transform ms2;
    public GameObject bullet;

    // Get item
    [SerializeField]
    private GameObject Item;
    public GameObject Effect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("CreateBullet", Delay);
    }

    void CreateBullet()
    {
        Instantiate(bullet, ms1.position, Quaternion.identity);
        Instantiate(bullet, ms2.position, Quaternion.identity);

        Invoke("CreateBullet", Delay);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Speed * Time.deltaTime);

        if (transform.position.y < -7.0f)
        {
            Destroy(gameObject);
        }
    }

    public void Damage(int attack)
    {
        HP -= attack;

        GameObject go =   Instantiate(Effect, transform.position, Quaternion.identity);
        Destroy(go, 1f);

        if (HP <= 0)
        {
            // drop item
            ItemDrop();
            Destroy(gameObject);
        }
    }

    public void ItemDrop()
    {
        Instantiate(Item, transform.position, Quaternion.identity);
    }
}
