using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : Projectile
{
    public int damage;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        transform.Rotate(new Vector3(0f,2f,0f));
        base.Update();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player") && !collision.CompareTag("EnemyProjectile") && !collision.CompareTag("PlayerProjectile"))
        {
            speed = 0f;
            Enemy e = collision.gameObject.GetComponent<Enemy>();
            if (e)
            {
                e.TakeDamage(damage);
            }
            if (anim)
            {
                anim.SetTrigger("Collision");
                AnimatorClipInfo[] temp = anim.GetCurrentAnimatorClipInfo(0);
                Destroy(gameObject, temp.Length);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
