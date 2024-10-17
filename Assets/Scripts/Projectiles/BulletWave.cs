using UnityEngine;

public class BulletWave : BulletBase
{
    [Header("Wave Info")]
    public float amplitude = 1f;  // y�� ����
    public float frequency = 1f;  // �ֱ� (�ӵ�)
    public float speed = 1f;      // x�� �̵� �ӵ�

    Vector3 startPosition;
    Quaternion rotation;

    float ellapsedTime = 0;

    bool waveInverse; // y�� ���� ����
    [HideInInspector] public bool bSpawnTwin = true;

    override protected void Start()
    {
        base.Start();
        rbody.velocity = Vector2.zero;

        // twin(�ݴ�� �����̴� �߻�ü) ����
        if (bSpawnTwin)
        {
            BulletWave twin = Instantiate(this);
            twin.Init(targetLayer, damage, impact, movePower, liveTime);
            twin.waveInverse = !waveInverse;
            twin.bSpawnTwin = false;
        }

        // ���� ��ġ �� ȸ�� ���� ����
        startPosition = transform.position;
        rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + 90);  // z������ 90�� ȸ��
    }

    void FixedUpdate()
    {
        ellapsedTime += Time.fixedDeltaTime * movePower;
        MoveWave();

        Debug.Log("FixedUpdate");
    }

    void MoveWave()
    {
        // x���� ������ �ӵ��� �̵�
        float x = ellapsedTime;

        // y���� ���� �Լ��� ���� �̵�
        float y = Mathf.Sin(ellapsedTime * frequency) * amplitude;
        if (waveInverse) y *= -1;

        // ȸ�� ���� ����
        Vector3 rotatedVector = rotation * new Vector3(x, y, 0);

        // ������Ʈ ��ġ ���� (MovePosition ���)       
        //transform.position = startPosition + rotatedVector;
        rbody.MovePosition(startPosition + rotatedVector);
    }
}
