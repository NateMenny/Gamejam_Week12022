using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyChaser : Enemy
{
    Rigidbody2D rb;

    [SerializeField] float speed;

    public override void Death()
    {
        base.Death();
        rb.velocity = Vector2.zero;
        Destroy(gameObject);
    }

    public override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();

        if (speed <= 0)
            speed = 5.0f;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
