using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{    
    public GameObject diePrefab;
       
    [HideInInspector]
    public UnityEvent onDead = new UnityEvent();

    [HideInInspector]
    public UnityEvent onDamaged = new UnityEvent();

    [SerializeField] float maxHealth = 100;
    [SerializeField] float currHealth;

    public float MaxHealth => maxHealth;
    public float CurrHealth => currHealth;
    
    bool isDead;

    // Start is called before the first frame update
    protected void Start()
    {
        currHealth = maxHealth;

        // 사망 시 이벤트 체인 등록
        // TODO :
        // 적 기체 파괴시 점수 증가 함수 추가 등록
        // 플레이어 기체 파괴시 게임 오버 함수 추가 등록
        {
            onDead.AddListener(delegate {
                //Debug.Log("onDeadLocal");

                // 사망 효과
                //SoundManager.Instance.PlaySound("Explosion");
                if (diePrefab)
                {
                    Instantiate(diePrefab, transform.position, diePrefab.transform.rotation);                    
                }
                
                Destroy(gameObject);
            });
        }        
    }

    public void Init(float health)
    {
        maxHealth = health;
        currHealth = maxHealth;
    }

    virtual public void GetDamaged(float damage, GameObject attacker = null)
    {
        if (isDead) return;
        
        currHealth -= damage;
        if (currHealth < 0) currHealth = 0;
        onDamaged.Invoke();

        // dead check
        if (currHealth == 0)
        {
            isDead = true;
            onDead.Invoke();
        } 
        
    }
}
