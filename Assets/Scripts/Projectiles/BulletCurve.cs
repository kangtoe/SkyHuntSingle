using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 스파이럴 궤적을 그리며 날아감 
// 회전각을 서서히 감소 -> 회전각이 일정한 등속운동 = 원 운동에서 벗어나기 위함
public class BulletCurve : BulletBase
{
    [Header("Curve")]
    public bool curveRightOrLeft;
    public float curveAmount = 10f;
    public float curveDecSpeed = 1f;
    float currentCurve;

    override protected void Start()
    {
        base.Start();
        currentCurve = curveAmount;
    }

    // Update is called once per frame
    void Update()
    {
        // 회전 방향
        int dir = curveRightOrLeft? 1 : -1;

        // 회전 각
        float rotateAmount = dir * currentCurve * Time.deltaTime;
        currentCurve = Mathf.Lerp(currentCurve, 0, Time.deltaTime * curveDecSpeed);
        //if (currentCurve > 0) currentCurve -= Time.deltaTime * curveDecSpeed; 

        rbody.velocity = Quaternion.Euler(0, 0, rotateAmount) * rbody.velocity;
    }
}
