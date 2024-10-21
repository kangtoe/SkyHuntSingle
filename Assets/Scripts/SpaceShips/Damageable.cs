using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{    
    public GameObject diePrefab;    
    public float maxHealth = 100;    

    [SerializeField]
    protected float currnetHealth;
    //Rigidbody2D rbody;        

    [HideInInspector]
    public UnityEvent onDead;
    

    // Start is called before the first frame update
    protected void Start()
    {
        currnetHealth = maxHealth;
        //rbody = GetComponent<Rigidbody2D>()

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
    virtual public void GetDamaged(float damage, GameObject attacker = null)
    {        
        // 피격 및 사망 처리
        currnetHealth -= damage;
        if (currnetHealth < 0) currnetHealth = 0;
        if (currnetHealth == 0) onDead.Invoke();
    }
}
