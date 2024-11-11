using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        int dir = curveRightOrLeft? 1 : -1;
        float rotateAmount = dir * currentCurve * Time.deltaTime;
        currentCurve = Mathf.Lerp(currentCurve, 0, Time.deltaTime * curveDecSpeed);
        //if (currentCurve > 0) currentCurve -= Time.deltaTime * curveDecSpeed; 

        RBody.velocity = Quaternion.Euler(0, 0, rotateAmount) * RBody.velocity;
    }
}
