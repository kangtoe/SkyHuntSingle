using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 추적 발사체
[RequireComponent(typeof(RotateToTarget))]
public class BulletChase : BulletBase
{
    public Transform target;

    override protected void Start()
    {
        base.Start();
        target = GetComponent<RotateToTarget>().Target;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!target)
        {
            rbody.AddForce(transform.up * movePower);
        }
        else
        {
            float powerMult = 1 - (GetAngleToTarget() / 180);
            //rbody.velocity = (transform.up * movePower * powerMult);
            rbody.AddForce(transform.up * movePower * powerMult);
        }                
    }

    // 0 ~ 180
    float GetAngleToTarget()
    {
        Vector2 vec = target.position - transform.position;
        float currentUpAngle = transform.rotation.eulerAngles.z + 90;
        float angle = Mathf.Abs(currentUpAngle - Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg);
        if(angle > 180) angle = 360 - angle;

        return angle;
    }
}
