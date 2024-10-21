using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStandard : MonoBehaviour
{
    [SerializeField] bool moveManually;
    [SerializeField] protected float movePower = 10f;

    protected Rigidbody2D rbody;

    //TrailEffect trailEffect;
    //FlameEffect flameEffect;

    // Start is called before the first frame update
    protected void Start()
    {
        rbody = GetComponent<Rigidbody2D>();

        //trailEffect = GetComponentInChildren<TrailEffect>();
        //flameEffect = GetComponentInChildren<FlameEffect>();
    }

    protected void FixedUpdate()
    {
        if (!moveManually)
        {
            Move();
        }

        //float TrailVelocity = 0.5f;
        //if (rbody.velocity.magnitude <= TrailVelocity) trailEffect.TrailDistach();
        //else trailEffect.TrailAttach();
    }


    public void Move()
    {
        rbody.AddForce(transform.up * movePower * rbody.mass);
    }
}
