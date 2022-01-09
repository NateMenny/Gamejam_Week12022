using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement2D : MonoBehaviour
{
    public bool debugVelocityTimeScale;
    bool isMoving;
    public float startMaxVel = 12f;
    public float maxVel;
    Vector2 mousePos;

    Rigidbody2D rb2d;
    float axisX;
    float axisY;
    float timeSlowFactor;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        
        if (maxVel <= 0f) maxVel = 25f;
    }


    // Update is called once per frames
    void Update()
    {
        // Calculate Slowdown factor
        if (rb2d.velocity.magnitude / maxVel > 1) timeSlowFactor = 0.5f;
        else timeSlowFactor = 1f - (rb2d.velocity.magnitude / maxVel) / 2;
        

        axisX = Input.GetAxisRaw("Horizontal");
        axisY = Input.GetAxisRaw("Vertical");

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float accelForce = maxVel / 5;
        // Add force acceleration and account for time slow
        rb2d.AddForce(new Vector2(axisX, axisY).normalized * TimeFactoredFloat(accelForce));
        if(axisX == 0f && axisY == 0f)
        {
            rb2d.AddForce(-rb2d.velocity.normalized * TimeFactoredFloat(accelForce));
            if (rb2d.velocity.magnitude < 0.1f) rb2d.velocity = Vector2.zero;
        }
        else
        // Clamp velocity on player
        if (rb2d.velocity.magnitude > maxVel)
        {
            rb2d.velocity = rb2d.velocity.normalized* maxVel;
        }

        if(debugVelocityTimeScale)
        Debug.Log("Velocity: " + rb2d.velocity.magnitude + "Timescale: " + Time.timeScale);
    }

    // Returns a float with reversed time scaling
    public float TimeFactoredFloat(float f)
    {
        return f + f / timeSlowFactor;
    }

     void FixedUpdate()
    {
        Vector2 lookDir = mousePos - rb2d.position;
        float angle = Mathf.Atan2(lookDir.y,lookDir.x) * Mathf.Rad2Deg - 90f;
        rb2d.rotation = angle;
    }

    // Create default player script and move there
    private void LateUpdate()
    {
        if(maxVel <= 0f)
        {
           // Die();
        }
    }
}
