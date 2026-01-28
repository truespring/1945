using Unity.Cinemachine;
using UnityEngine;

public class CameraImpulse : MonoBehaviour
{
    //싱글톤 인스턴스
    public static CameraImpulse Instance;

    //임펄스 소스 컴포넌트
    [SerializeField] CinemachineImpulseSource impulse;

    //Awake 메서드 : 싱글톤 패턴 구현
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void SetImpulseSource(CinemachineImpulseSource source)
    {
        impulse = source;
    }


    //카메라 흔들기 메서드
    public void CameraShakeShow()
    {
        impulse.GenerateImpulse();
    }
}