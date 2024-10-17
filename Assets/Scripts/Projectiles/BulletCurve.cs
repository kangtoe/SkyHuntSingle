using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �����̷� ������ �׸��� ���ư� 
// ȸ������ ������ ���� -> ȸ������ ������ ��ӿ = �� ����� ����� ����
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
        // ȸ�� ����
        int dir;
        if (curveRightOrLeft) dir = -1;
        else dir = 1;

        // ȸ�� ��
        float rotateAmount = dir * currentCurve * Time.deltaTime;
        if(currentCurve > 0) currentCurve -= Time.deltaTime * curveDecSpeed; 

        rbody.velocity = Quaternion.Euler(0, 0, rotateAmount) * rbody.velocity;
    }
}