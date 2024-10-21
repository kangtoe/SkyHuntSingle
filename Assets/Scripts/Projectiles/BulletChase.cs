using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 추적 발사체
[RequireComponent(typeof(RotateToTarget))]
public class BulletChase : BulletBase
{
    FindTarget findTarget;
    Transform Target => findTarget?.Target;

    override protected void Start()
    {
        findTarget = GetComponent<FindTarget>();
        base.Start();        
    }

    // Update is called once per frame
    void FixedUpdate()
    {        
        float powerMult = 1;
        if (Target) powerMult -= (GetAngleToTarget(Target) / 180);
        rbody.AddForce(transform.up * movePower * powerMult);
    }

    // 0 ~ 180
    float GetAngleToTarget(Transform target)
    {
        Vector2 vec = target.position - transform.position;
        float currentUpAngle = transform.rotation.eulerAngles.z + 90;
        float angle = Mathf.Abs(currentUpAngle - Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg);
        if(angle > 180) angle = 360 - angle;

        return angle;
    }
}
