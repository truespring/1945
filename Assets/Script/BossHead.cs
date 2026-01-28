using UnityEngine;

public class BossHead : MonoBehaviour
{
    [SerializeField] //직렬화
    GameObject bossbullet; //보스미사일

    //애니메이션에서 함수사용하기
    public void RightDownLaunch()
    {
        //보스미사일 생성    
        GameObject go = Instantiate(bossbullet, transform.position, Quaternion.identity);
        //보스미사일 이동
        go.GetComponent<BossBullet>().Move(new Vector2(1, -1));
    }



    public void LeftDownLaunch()
    {
        GameObject go = Instantiate(bossbullet, transform.position, Quaternion.identity);

        go.GetComponent<BossBullet>().Move(new Vector2(-1, -1));

    }

    public void DownLaunch()
    {
        GameObject go = Instantiate(bossbullet, transform.position, Quaternion.identity);

        go.GetComponent<BossBullet>().Move(new Vector2(0, -1));

    }
}
