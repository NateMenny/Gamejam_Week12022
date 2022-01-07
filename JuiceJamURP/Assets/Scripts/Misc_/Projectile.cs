using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public Vector2 direction;
    public float speed;

    // Update is called once per frame
    protected virtual void Update()
    {
        // Moves projectile at constant speed in direction
        GetComponent<Rigidbody2D>().velocity = direction * (speed * Time.timeScale);
    }
}
