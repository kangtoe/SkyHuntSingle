using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 탄환이 사라지는 조건 3가지
// 1. 생성 후 일정 시간이 경과.
// 2. 화면 밖으로 나감
// 3. 물체 충돌
public class BulletBase : MonoBehaviour
{
    [Header("임시 비활성화 컴포넌트들")]    
    public Renderer[] renderers;
    //public ParticleSystem[] pss;

    [Space]
    public GameObject hitEffect;    
    public LayerMask targetLayer; // 해당 오브젝트와 충돌을 검사할 레이어        
    public int damage;
    public int impact;
    public float movePower;

    public float liveTime = 10;
    float spwanedTime = 0;
     
    protected Rigidbody2D rbody;
    protected SpriteRenderer sprite;
    TrailRenderer trail;

    //public int ownerActor;   

    // Start is called before the first frame update
    virtual protected void Start()
    {   
        //sprite = GetComponent<SpriteRenderer>();
        //trail = GetComponentInChildren<TrailRenderer>();
        
        rbody = GetComponent<Rigidbody2D>();
        rbody.velocity = transform.up * movePower;
        //Debug.Log("velocity : " + rbody.velocity);
    }

    private void Update()
    {  
        spwanedTime += Time.deltaTime;
        if (liveTime < spwanedTime) Destroy(gameObject);      
    }

    virtual protected void OnTriggerEnter2D(Collider2D other)
    {
        // targetLayer 검사
        if (1 << other.gameObject.layer == targetLayer.value)
        {
            OnHit(other);
        }
    }

    private void OnValidate()
    {
        renderers = GetComponentsInChildren<Renderer>();
        //pss = GetComponentsInChildren<ParticleSystem>();
    }   

    // 탄환 수치를 사격당 설정할필요 있나? -> TODO : 수치변경시만  static한 값을 수정?
    // shooter에서 생성 시 호출 -> 초기화
    // hitEffect는 GameObject 직렬화가 불가능한 관계로 발사체 프리펩에서 지정할 것 
    public void Init(int targetLayer, int damage, int impact, float movePower, float liveTime) // float colorR, float colorG, float colorB
    {
        //Debug.Log("init");        
        this.targetLayer = targetLayer;
        this.damage = damage;
        this.impact = impact;
        this.movePower = movePower;
        this.liveTime = liveTime;

        sprite = GetComponent<SpriteRenderer>();        
        trail = GetComponentInChildren<TrailRenderer>();

        //ColorCtrl colorCtrl = GetComponent<ColorCtrl>();
        //Color color = new Color(colorR, colorG, colorB);
        //colorCtrl.SetColor(color);
    }    

    protected void OnHit(Collider2D other)
    {
        //Debug.Log("OnHit : " + other);

        // 피해주기
        Damageable damageable = other.GetComponent<Damageable>();
        if (damageable)
        {
            damageable.GetDamaged(damage, gameObject);
        }

        // 힘 가하기       
        Rigidbody2D rbody = other.attachedRigidbody;
        if (rbody)
        {
            //Vector2 dir = coll.transform.position - transform.position;
            Vector2 dir = transform.up;
            rbody.AddForce(dir * impact, ForceMode2D.Impulse);
            //Debug.Log("AddForce" + dir * impact);
        }

        if (trail)
        {
            //Debug.Log("trail disttach");
            trail.transform.parent = null;
            trail.autodestruct = true;                                 
        }

        if (hitEffect)
        {
            //Debug.Log("Instantiate hitEffect");
            //string str = "Projectiles/" + hitEffect.name;
            GameObject go = Instantiate(hitEffect, transform.position, transform.rotation);
        }

        // 음향효과 (플레이어는 PlayerDamageable에서 음향효과 처리)
        //SoundManager.Instance.PlaySound("OnHit");

        Destroy(gameObject);        
    }
}
