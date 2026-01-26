using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    Animator ani; // get animator component
    public GameObject[] bullet;
    public Transform pos = null;
    public int power = 0;
    
    [SerializeField]
    GameObject powerup;
    
    public GameObject bomb;

    void Start()
    {
        ani = GetComponent<Animator>();
    }

    void Update()
    {
        // move
        // float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        // float moveY = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        // transform.Translate(moveX, moveY, 0);

        // diagonal
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector2 input = new Vector2(moveX, moveY);
        if (input.sqrMagnitude > 1f) input.Normalize(); // 대각선 이동 보정
        Vector3 move = new Vector3(input.x, input.y, 0f) * moveSpeed * Time.deltaTime;

        ani.SetBool("left", moveX <= -0.5f);
        ani.SetBool("right", moveX >= 0.5f);
        ani.SetBool("up", moveY >= 0.5f);
        
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            //폭탄생성
            Instantiate(bomb, Vector3.zero, Quaternion.identity);
        }

        // fire the missile
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bullet[power], pos.position, Quaternion.identity);
        }

        transform.Translate(move, Space.World);


        // 화면 밖으로 나가지 않도록 월드 좌표를 뷰포트 좌표로 변환합니다.
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
        // 뷰포트 x 값을 0~1 범위로 클램프합니다.
        viewPos.x = Mathf.Clamp01(viewPos.x);
        // 뷰포트 y 값을 0~1 범위로 클램프합니다.
        viewPos.y = Mathf.Clamp01(viewPos.y);
        // 클램프된 뷰포트 좌표를 다시 월드 좌표로 변환하여 적용합니다.
        Vector3 worldPos = Camera.main.ViewportToWorldPoint(viewPos);
        transform.position = worldPos; // 위치 갱신
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Item"))
        {
            if (power < 3)
            {
                power += 1;
                GameObject go = Instantiate(powerup, transform.position, Quaternion.identity);
                Destroy(go, 1);
            }

            Destroy(collider.gameObject);
        }
    }
}
