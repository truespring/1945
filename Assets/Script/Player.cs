using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Animator _ani; // get animator component
    public GameObject[] bullet;
    public Transform pos = null;
    public int power = 0;

    [SerializeField] GameObject powerup;

    [Header("Lazer")] public GameObject lazer;
    public float gValue = 0;
    public Image Gage;

    // 느리게 할 시간 비율 (0~1). 플레이어는 언스케일드(정상속도)를 유지.
    public float slowTimeScale = 0.2f;
    private float _normalFixedDeltaTime;

    public GameObject bomb;

    public DynamicJoystick dJoystick;

    // 새로 추가된 변수: 레이저 발사 후 재충전 대기시간 관리
    public float cooldownDuration = 3f;
    private bool _canCharge = true;
    private bool _canLazer;

    void Start()
    {
        _ani = GetComponent<Animator>();
        _ani.updateMode = AnimatorUpdateMode.UnscaledTime;
        // 초기 FixedDeltaTime 저장
        _normalFixedDeltaTime = Time.fixedDeltaTime;
    }

    void Update()
    {
        // Left Shift 누르면 전체 시간 느리게 (플레이어 제외)
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Time.timeScale = slowTimeScale;
            Time.fixedDeltaTime = _normalFixedDeltaTime * slowTimeScale;
        }
        else
        {
            Time.timeScale = 1f;
            Time.fixedDeltaTime = _normalFixedDeltaTime;
        }

        // diagonal
        float moveX = dJoystick.Horizontal;
        float moveY = dJoystick.Vertical;

        Vector2 input = new Vector2(moveX, moveY);
        if (input.sqrMagnitude > 1f) input.Normalize(); // 대각선 이동 보정
        Vector3 move = new Vector3(input.x, input.y, 0f) * (moveSpeed * Time.unscaledDeltaTime);

        _ani.SetBool("left", moveX <= -0.5f);
        _ani.SetBool("right", moveX >= 0.5f);
        _ani.SetBool("up", moveY >= 0.5f);

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            //폭탄생성
            Instantiate(bomb, Vector3.zero, Quaternion.identity);
        }

        // fire the missile
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        // Instantiate(bullet[power], pos.position, Quaternion.identity);
        // }
        // else if (Input.GetKey(KeyCode.Space))
        // {
        //     gValue += Time.deltaTime;
        //     Gage.fillAmount = gValue;
        //
        //     if (gValue >= 1)
        //     {
        //         GameObject go = Instantiate(lazer, pos.position, Quaternion.identity);
        //         Destroy(go, 3);
        //         gValue = 0;
        //     }
        // }
        // else
        // {
        //     gValue -= Time.deltaTime;
        //     if (gValue <= 0) gValue = 0;
        //     
        //     // gage UI
        //     Gage.fillAmount = gValue;
        // }

        if (_canLazer)
        {
            // 충전 허용 상태에서만 게이지 증가 (게이지는 타임스케일의 영향을 받음)
            if (_canCharge)
            {
                gValue += Time.deltaTime;
                Gage.fillAmount = gValue;
                if (gValue >= 1)
                {
                    GameObject go = Instantiate(lazer, pos.position, Quaternion.identity);
                    Destroy(go, 3);
                    gValue = 0;
                    // 충전 비활성화 및 쿨다운 코루틴 시작 (쿨다운은 시간 스케일 영향을 받음)
                    StartCoroutine(LazerCooldown());
                }
            }
            else
            {
                // 쿨다운 중이면 게이지는 유지
                Gage.fillAmount = gValue;
            }
        }
        else
        {
            gValue -= Time.deltaTime;

            if (gValue <= 0)
            {
                gValue = 0;
            }

            //게이지바 UI표시
            Gage.fillAmount = gValue;
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

    // 레이저 발사 후 쿨다운 코루틴
    IEnumerator LazerCooldown()
    {
        _canCharge = false;
        yield return new WaitForSeconds(cooldownDuration);
        _canCharge = true;
    }

    public void LazerOn()
    {
        _canLazer = true;
    }

    public void LazerOff()
    {
        _canLazer = false;
    }

    public void Fire()
    {
        Instantiate(bullet[power], pos.position, Quaternion.identity);
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