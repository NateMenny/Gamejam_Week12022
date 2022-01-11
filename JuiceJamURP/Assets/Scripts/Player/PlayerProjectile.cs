using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : Projectile
{
    public int damage;
    void Update()
    {
        transform.Rotate(new Vector3(0f,2f,0f));
        base.Update();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player") && !collision.CompareTag("EnemyProjectile") && !collision.CompareTag("PlayerProjectile"))
        {
            Enemy e = collision.gameObject.GetComponent<Enemy>();
            if (e)
            {
                e.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
