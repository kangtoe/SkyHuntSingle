using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStandrad : MonoBehaviour
{
    [SerializeField] bool moveManually;

    public float MovePower = 10f;
    Rigidbody2D rbody;

    //TrailEffect trailEffect;
    //FlameEffect flameEffect;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();

        //trailEffect = GetComponentInChildren<TrailEffect>();
        //flameEffect = GetComponentInChildren<FlameEffect>();
    }

    private void FixedUpdate()
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
        rbody.AddForce(transform.up * MovePower * rbody.mass);
    }
}
