using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerShip : MonoBehaviour
{
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

        if (Input.GetMouseButton(1))
        {
            rbody.AddForce(transform.up * MovePower * rbody.mass);
            
        }
    }

    // Update is called once per frame
    void Update()
    {

        //float TrailVelocity = 0.5f;
        //if (rbody.velocity.magnitude <= TrailVelocity) trailEffect.TrailDistach();
        //else trailEffect.TrailAttach();

    }
}
