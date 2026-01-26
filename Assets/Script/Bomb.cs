using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float explosionRadius = 1.5f;
    [SerializeField] private int damage = 50;
    [SerializeField] private LayerMask hitLayers = Physics2D.DefaultRaycastLayers;

    private void Start()
    {
        // 트리거에 진입하면 폭발 반경 내 모든 콜라이더를 검사하여 몬스터에게 데미지를 줍니다.
        // OverlapCircleAll을 사용하여 폭발 반경 내의 모든 콜라이더를 가져옵니다.
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius, hitLayers);


        // 중복된 몬스터에 대한 데미지 적용을 방지하기 위해 HashSet 사용
        var damaged = new HashSet<Monster>();

        // 충돌한 모든 콜라이더를 순회
        foreach (var hit in hits)
        {
            // null 체크
            if (hit == null) continue;
            // 몬스터 컴포넌트를 가져와서 데미지 적용
            Monster monster = hit.GetComponent<Monster>();
            // 몬스터가 존재하고 아직 데미지를 받지 않은 경우에만 데미지 적용
            if (monster != null && !damaged.Contains(monster))
            {
                // 몬스터에게 데미지 적용
                monster.Damage(damage);
                // 데미지를 받은 몬스터를 HashSet에 추가
                damaged.Add(monster);
            }
        }

        // 폭탄 오브젝트 제거 (원하면 폭발 이펙트 재생 후 제거)
        Destroy(gameObject, 2);
    }

    private void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
    }

    // 폭발 반경을 시각화하기 위한 Gizmos 그리기
    private void OnDrawGizmosSelected()
    {
        // 폭발 반경을 반투명 오렌지색 구체로 표시
        Gizmos.color = new Color(1f, 0.5f, 0f, 0.5f);
        //기즈모 구형태로 그리기  포지션, 반지름
        Gizmos.DrawSphere(transform.position, explosionRadius);
    }
}