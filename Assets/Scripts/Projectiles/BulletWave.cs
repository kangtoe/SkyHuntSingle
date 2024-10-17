using UnityEngine;

public class BulletWave : BulletBase
{
    [Header("Wave Info")]
    public float amplitude = 1f;  // y축 진폭
    public float frequency = 1f;  // 주기 (속도)
    public float speed = 1f;      // x축 이동 속도

    Vector3 startPosition;
    Quaternion rotation;

    float ellapsedTime = 0;

    bool waveInverse; // y축 반전 여부
    [HideInInspector] public bool bSpawnTwin = true;

    override protected void Start()
    {
        base.Start();
        rbody.velocity = Vector2.zero;

        // twin(반대로 움직이는 발사체) 스폰
        if (bSpawnTwin)
        {
            BulletWave twin = Instantiate(this);
            twin.Init(targetLayer, damage, impact, movePower, liveTime);
            twin.waveInverse = !waveInverse;
            twin.bSpawnTwin = false;
        }

        // 시작 위치 및 회전 정보 설정
        startPosition = transform.position;
        rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + 90);  // z축으로 90도 회전
    }

    void FixedUpdate()
    {
        ellapsedTime += Time.fixedDeltaTime * movePower;
        MoveWave();

        Debug.Log("FixedUpdate");
    }

    void MoveWave()
    {
        // x축은 일정한 속도로 이동
        float x = ellapsedTime;

        // y축은 사인 함수에 따라 이동
        float y = Mathf.Sin(ellapsedTime * frequency) * amplitude;
        if (waveInverse) y *= -1;

        // 회전 벡터 적용
        Vector3 rotatedVector = rotation * new Vector3(x, y, 0);

        // 오브젝트 위치 갱신 (MovePosition 사용)       
        //transform.position = startPosition + rotatedVector;
        rbody.MovePosition(startPosition + rotatedVector);
    }
}
