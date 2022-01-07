using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyChaser : Enemy
{
    Rigidbody2D rb;

    [SerializeField] float speed;
    [SerializeField] float maxSpeed;

    public override void Death(int drops)
    {
        base.Death(drops);
        rb.velocity = Vector2.zero;
        Destroy(gameObject);
    }

    public override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();

        if (speed <= 0)
            speed = 5.0f;
        if (maxSpeed <= 0f) maxSpeed = 7f;
    }
    // Update is called once per frame
    void Update()
    {
        Vector2 dirToPlayer = (GameManager.instance.playerInstance.transform.position - transform.position).normalized;
        rb.AddForce(dirToPlayer * speed);
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }
}
