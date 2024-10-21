using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : MonoBehaviour
{
    [SerializeField] bool bLookCenterAround;

    // Start is called before the first frame update
    void Start()
    {
        if(bLookCenterAround) LookCenterAround();        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LookCenterAround()
    {
        float AroundRadius = 1;
        Vector2 lookAt = Vector2.zero + Random.insideUnitCircle * AroundRadius;
        Vector2 dir = lookAt - (Vector2)transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }
}
