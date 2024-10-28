using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindTarget : MonoBehaviour
{
    [SerializeField] LayerMask targetLayer;
    [SerializeField] float searchRaduius = 20;    
    public bool findAlways = false;
    
    [Header("for debug")]
    [SerializeField] Transform target;
    public Transform Target => target;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(UpdateTargetCr());
    }

    IEnumerator UpdateTargetCr()
    {
        while (true)
        {            
            if(!target || findAlways)
            {
                // 타겟 갱신
                Collider2D coll = GetClosestCollider(transform.position, searchRaduius, targetLayer);
                if (coll) target = coll.attachedRigidbody.transform;
            }            

            // 대기
            yield return new WaitForSeconds(1);
        }        
    }

    Collider2D GetClosestCollider(Vector2 pos, float radius, LayerMask layer)
    {                
        Collider2D[] colls = Physics2D.OverlapCircleAll(pos, radius, layer);
        Collider2D closestColl = null;
        float closestDist = Mathf.Infinity;

        foreach (Collider2D coll in colls)
        {            
            // 플레이어 또는 기준 지점과 각 오브젝트 간의 거리를 계산
            float distance = Vector3.Distance(transform.position, pos);

            // 현재까지 가장 가까운 오브젝트보다 더 가까운 오브젝트를 찾으면 업데이트
            if (distance < closestDist)
            {
                closestDist = distance;
                closestColl = coll;
            }
        }
        return closestColl;        
    }

}
