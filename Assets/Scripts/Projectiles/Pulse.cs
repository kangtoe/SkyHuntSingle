using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 점차 확대되고, 흐릿해짐
public class Pulse : BulletBase
{
    [Header("Pulse Info")]        
    public float expansionSpeed = 1f; // 확대 속도
    public float expansionScale = 1f; // 최대 확대 스케일
    float currentExpansion = 0;
    float attackableRatio = 0.8f; // 어느정도  확대 후, 희미하게 사라져 갈때 쯤은 공격 판정을 지운다.  

    public float ExpansionRadius => Coll.bounds.size.x * expansionScale;

    // Start is called before the first frame update
    override protected void Start()
    {
        Sprite.transform.localScale = Vector2.one * currentExpansion;

        //Debug.Log("expansionMax : " + expansionMax);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentExpansion < expansionScale)
        {
            currentExpansion += Time.deltaTime * expansionSpeed;
        }
        else
        {
            Destroy(gameObject);
        }

        //Debug.Log("currentExpansion : " + currentExpansion);

        Sprite.transform.localScale = Vector2.one * currentExpansion;
        
        float fadeStartRatio = 0.5f;
        float t;
        if (currentExpansion / expansionScale < fadeStartRatio) t = 0f;
        else t = currentExpansion / expansionScale / fadeStartRatio - 2 * fadeStartRatio;
        //Debug.Log("t : " + t);

        Color color = Sprite.color;
        color.a = Mathf.Lerp(1f, 0f, t);
        Sprite.color = color;
    }

    override protected void OnTriggerEnter2D(Collider2D other)
    {
        if (currentExpansion / expansionScale > attackableRatio) return;

        base.OnTriggerEnter2D(other);       
    }

    //private void OnDrawGizmos()
    //{
    //    if(!sprite) sprite = GetComponent<SpriteRenderer>();
    //    float size = sprite.sprite.bounds.size.x;

    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, size * expansionMax / 2f);
    //}
}
