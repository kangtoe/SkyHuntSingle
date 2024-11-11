using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 추적 발사체
[RequireComponent(typeof(RotateToTarget))]
public class BulletChase : BulletBase
{
    FindTarget findTarget;
    Transform Target => findTarget.Target;

    Pulse pulse;
    Pulse Pulse {
        get
        {
            if (!pulse) pulse = hitEffect.GetComponent<Pulse>();
            return pulse;
        }
    }                

    override protected void Start()
    {
        findTarget = GetComponent<FindTarget>();
        findTarget.targetLayer = targetLayer;
        base.Start();        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Target && Pulse)
        {
            float checkRange = Pulse.ExpansionRadius / 2;
            bool check = Physics2D.OverlapCircle(transform.position, checkRange, targetLayer);      
            if (check) OnDestroyBullet();
        }

        float powerMult = 1;
        if (Target) powerMult -= (GetAngleToTarget(Target) / 180);
        RBody.AddForce(transform.up * movePower * powerMult);
    }

    float GetAngleToTarget(Transform target)
    {
        Vector2 directionToTarget = target.position - transform.position;
        Vector2 currentDirection = transform.up;

        return Vector2.Angle(currentDirection, directionToTarget);
    }
}
