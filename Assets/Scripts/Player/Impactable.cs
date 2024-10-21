using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// collider - collider 충돌 시, 부딪힌 대상에게 피해와 힘을 가한다.
public class Impactable : MonoBehaviour
{
    [SerializeField] GameObject bumpEffect;

    [Header("Amount")]
    public float impactDamageOther = 10;
    public float impactDamageSelf = 10;
    public float impactPowerOther = 10;
    public float impactPowerSelf = 10;

    Damageable m_damageable;
    Rigidbody2D m_rbody;

    private void Start()
    {
        m_damageable = GetComponent<Damageable>();
        m_rbody = GetComponent<Rigidbody2D>();
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        Impact(coll);
    }
    void Impact(Collision2D coll)
    {
        // take damage
        if (m_damageable)
        {
            m_damageable.GetDamaged(impactDamageSelf);
        }

        // do damage
        Damageable damageable = coll.transform.GetComponent<Damageable>();       
        if (damageable)
        {
            damageable.GetDamaged(impactDamageOther);            
        }

        Vector2 dir = coll.transform.position - transform.position;

        // add force other
        Rigidbody2D rbody = coll.transform.GetComponent<Rigidbody2D>();
        if (rbody)
        {            
            rbody.AddForce(dir * impactPowerOther, ForceMode2D.Impulse);
        }

        if (m_rbody)
        {
            m_rbody.AddForce(-1 * dir * impactPowerSelf, ForceMode2D.Impulse);
        }


        // effect
        if (bumpEffect)
        {
            Vector2 normal = coll.contacts[0].normal;
            float angle = Mathf.Atan2(normal.y, normal.x) * Mathf.Rad2Deg - 90; 
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            Vector2 impactPoint = coll.contacts[0].point;
            Instantiate(bumpEffect, impactPoint, rotation);
        }
    }
}
