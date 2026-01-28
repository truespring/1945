using System.Collections;
using UnityEngine;

public class Spwan : MonoBehaviour
{
    public float ss = -2f;
    public float es = 2f;
    public float StartTime = 1;
    public float SpwanStop = 10;
    public GameObject monster1;
    public GameObject monster2;
    public GameObject boss;

    bool swi = true;
    bool swi2 = true;

    [SerializeField] private GameObject textBossWarning;

    private void Awake()
    {
        textBossWarning.SetActive(false);
    }

    void Start()
    {
        
        // Vector3 pos = new Vector3(0, 3f, 0);
        // Instantiate(boss, pos, Quaternion.identity); 
        StartCoroutine("RandomSpwan");
        Invoke("Stop", SpwanStop);
    }

    IEnumerator RandomSpwan()
    {
        while(swi)
        {
            yield return new WaitForSeconds(StartTime);
            float x = Random.Range(ss, es);
            Vector2 r = new Vector2(x, transform.position.y);
            Instantiate(monster1, r, Quaternion.identity);
        }
    }

    IEnumerator RandomSpwan2()
    {
        while(swi2)
        {
            yield return new WaitForSeconds(StartTime + 2);
            float x = Random.Range(ss, es);
            Vector2 r = new Vector2(x, transform.position.y);
            Instantiate(monster2, r, Quaternion.identity);
        }
    }


    void Stop()
    {
        swi = false;
        StopCoroutine("RandomSpwan");

        StartCoroutine("RandomSpwan2");
        Invoke("Stop2", SpwanStop + 5);
    }

    void Stop2()
    {
        swi2 = false;
        StopCoroutine("RandomSpwan2");
        
        // boss
        textBossWarning.SetActive(true);

        Vector3 pos = new Vector3(0, 3f, 0);
        Instantiate(boss, pos, Quaternion.identity); 
        StartCoroutine("Shake");
    }

    IEnumerator Shake()
    {
        int shakeCnt = 30;
        while (shakeCnt > 0)
        {
            CameraImpulse.Instance.CameraShakeShow();
            yield return new WaitForSeconds(0.1f);
            shakeCnt--;
        }
    }

}
