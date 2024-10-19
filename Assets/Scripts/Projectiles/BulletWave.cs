using UnityEngine;

public class BulletWave : BulletBase
{
    [Header("Wave Info")]
    public float amplitude = 1f; 
    public float frequency = 1f; 
    public float speed = 1f;      

    Vector3 startPosition;
    Quaternion rotation;

    float ellapsedTime = 0;

    bool waveInverse;
    [HideInInspector] public bool bSpawnTwin = true;

    override protected void Start()
    {
        base.Start();
        rbody.velocity = Vector2.zero;

        // twin 생성
        if (bSpawnTwin)
        {
            BulletWave twin = Instantiate(this);
            twin.Init(targetLayer, damage, impact, movePower, liveTime);
            twin.waveInverse = !waveInverse;
            twin.bSpawnTwin = false;
        }
        
        startPosition = transform.position;
        rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + 90);
    }

    void FixedUpdate()
    {
        ellapsedTime += Time.fixedDeltaTime * movePower;
        MoveWave();        
    }

    void MoveWave()
    {        
        float x = ellapsedTime;        
        float y = Mathf.Sin(ellapsedTime * frequency) * amplitude;
        if (waveInverse) y *= -1;

        Vector3 rotatedVector = rotation * new Vector3(x, y, 0);
             
        //transform.position = startPosition + rotatedVector;
        rbody.MovePosition(startPosition + rotatedVector);
    }
}
