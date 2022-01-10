using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyChaser : Enemy
{
    Rigidbody2D rb;

    [SerializeField] float maxSpeed;

    public override void Death(int drops)
    {
        Debug.Log("Enemy is dying");
        base.Death(drops);
        rb.velocity = Vector2.zero;
        DestroyObj(0f);
    }

    public override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();

        if (maxSpeed <= 0f) maxSpeed = 7f;
    }
    // Update is called once per frame
    void Update()
    {
        Vector2 dirToPlayer = (GameManager.instance.playerInstance.transform.position - transform.position).normalized;
        //rb.AddForce(dirToPlayer * speed);
        rb.velocity = dirToPlayer * (maxSpeed * Time.timeScale);
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }
}
