using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector2 direction;
    public float speed;

    // Update is called once per frame
    void Update()
    {
        // Moves projectile at constant speed in direction
        GetComponent<Rigidbody2D>().velocity = direction * (speed * Time.timeScale);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Do damage stuff to player
       
        if (!collision.CompareTag("Enemy") && !collision.CompareTag("Projectile"))
        {
            Destroy(gameObject);
        }
    }
}
