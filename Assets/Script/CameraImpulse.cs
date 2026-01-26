using Unity.Cinemachine;
using UnityEngine;

public class CameraImpulse : MonoBehaviour
{
    public static CameraImpulse instance;

    [SerializeField] private CinemachineImpulseSource impulse;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    public void CameraShakeShow()
    {
        impulse.GenerateImpulse();
    }
}