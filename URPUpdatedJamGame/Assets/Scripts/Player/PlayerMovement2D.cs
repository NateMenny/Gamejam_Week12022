using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement2D : MonoBehaviour
{
    bool isMoving;
    public float startMaxVel = 12f;
    public float maxVel;

    Rigidbody2D rb2d;
    float axisX;
    float axisY;
    public float timeSlowFactor;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.drag = 1f;
        
        if (maxVel <= 0f) maxVel = 50f;
    }

    // Update is called once per frames
    void Update()
    {
        // Calculate Slowdown factor
        if (rb2d.velocity.magnitude / maxVel > 1) timeSlowFactor = 0.5f;
        else timeSlowFactor = 1f - (rb2d.velocity.magnitude / maxVel) / 2;
        

        axisX = Input.GetAxisRaw("Horizontal");
        axisY = Input.GetAxisRaw("Vertical");

        // Add force acceleration and account for time slow
        rb2d.AddForce(new Vector2(axisX, axisY).normalized * TimeFactoredFloat(maxVel/3));
        // Clamp velocity on player
        if (rb2d.velocity.magnitude > maxVel)
        {
            rb2d.velocity = rb2d.velocity.normalized* maxVel;
        }

        Debug.Log("Velocity: " + rb2d.velocity.magnitude + "Timescale: " + Time.timeScale);
    }

    public float TimeFactoredFloat(float f)
    {
        return f + f / timeSlowFactor;
    }
}