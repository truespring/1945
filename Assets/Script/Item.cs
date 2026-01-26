using UnityEngine;

public class Item : MonoBehaviour
{
    public float ItemVelocity = 100f;
    Rigidbody2D rig = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        rig.AddForce(new Vector3(-ItemVelocity, ItemVelocity, 0f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
