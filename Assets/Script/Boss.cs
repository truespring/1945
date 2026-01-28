using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private int flag = 1;

    private int speed = 2;

    public GameObject mb;

    public GameObject mb2;

    public Transform pos1;
    public Transform pos2;

    private void Awake()
    {
        //CinemachineImpulseSource이타입을 가지고 있는 오브젝트 바로 검색해서 가져와
        CinemachineImpulseSource cine = FindAnyObjectByType<CinemachineImpulseSource>();
        CameraImpulse.Instance.SetImpulseSource(cine);
    }

    private void Start()
    {
        Invoke("Hide", 2);
        StartCoroutine("BossMissile");
        StartCoroutine("CircleFire");
    }
    
    private void Update()
    {
        if (transform.position.x >= 1)
            flag *= -1;
        if (transform.position.x <= -1)
            flag *= -1;


        transform.Translate(flag * speed * Time.deltaTime,0,0);
    }

    private void Hide()
    {
        GameObject.Find("TextBossWarning").SetActive(false);
    }

    IEnumerator BossMissile()
    {
        while (true)
        {
            Instantiate(mb, pos1.position, Quaternion.identity);
            Instantiate(mb, pos2.position, Quaternion.identity);

            yield return new WaitForSeconds(0.5f);
        }
    }
    
    // circle missile
    IEnumerator CircleFire()
    {
        float attackRate = 3;
        int count = 30;
        float intervalAngle = 360 / count;
        float weightAngle = 0f;

        while (true)
        {
            for (int i = 0; i < count; i++)
            {
                GameObject clone = Instantiate(mb2, transform.position, Quaternion.identity);

                float angle = weightAngle + intervalAngle * i;
                float x = Mathf.Cos(angle * Mathf.Deg2Rad);
                float y = Mathf.Sin(angle * Mathf.Deg2Rad);
                
                clone.GetComponent<BossBullet>().Move(new Vector2(x, y));
            }

            weightAngle += 1;

            yield return new WaitForSeconds(attackRate);
        }
    }

}