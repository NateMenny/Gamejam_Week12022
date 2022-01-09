using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : Projectile
{
    public int damage;
    // Update is called once per frame

    private void Start()
    {
        if (damage <= 0) Debug.Log("There aint no damage to deal on " + name);
    }

    void Update()
    {
        base.Update();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") && !collision.CompareTag("Projectile"))
        {
            PlayerMovement2D pm = collision.gameObject.GetComponent<PlayerMovement2D>();
            if(pm)
            {
                pm.maxVel -= damage;
            }
            Destroy(gameObject);
        }
    }
}
