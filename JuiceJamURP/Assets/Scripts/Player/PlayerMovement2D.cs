using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement2D : MonoBehaviour
{
    enum MoveType
    {
        FORCE,
        VELOCITY
    }

    public bool debugVelocityTimeScale;
    bool isMoving;

    [Header("Move Settings")]
    [Tooltip("Factor of max velocity")] public float accelFactor;
    public float maxVel;
    [SerializeField] MoveType moveType = MoveType.FORCE;

    Vector2 mousePos;
    Rigidbody2D rb2d;
    float axisX;
    float axisY;
    float timeSlowFactor;

    public bool IsMoving { get => isMoving; }

    [SerializeField] float playerSlowDown;

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

        float accelForce = maxVel / accelFactor;
        // Add force acceleration and account for time slow
        switch (moveType)
        {
            case MoveType.FORCE:
                rb2d.AddForce(new Vector2(axisX, axisY).normalized * TimeFactoredFloat(accelForce));
                break;
            case MoveType.VELOCITY:
                // This is the velocity and acceleration version (Try it out and see which one you like better)
                rb2d.velocity += new Vector2(axisX, axisY).normalized * (accelFactor * Time.unscaledDeltaTime);
                break;
            default: break;
        }

        if (axisX == 0f && axisY == 0f)
        {
            isMoving = false;
            switch(moveType)
            {
                case MoveType.FORCE:
                    rb2d.AddForce(-rb2d.velocity.normalized * TimeFactoredFloat(accelForce));
                    break;
                case MoveType.VELOCITY:
                    // This is the velocity and acceleration version (Try it out and see which one you like better)
                    rb2d.velocity += -rb2d.velocity.normalized * (accelFactor * Time.unscaledDeltaTime);
                    break;
                default: break;
            }
            if (rb2d.velocity.magnitude < 0.2f) rb2d.velocity = Vector2.zero;
        }
        else
        {
            isMoving = true;
        }
        // Clamp velocity on player
        if (rb2d.velocity.magnitude > maxVel)
        {
            rb2d.velocity -= rb2d.velocity.normalized*maxVel*Time.deltaTime*playerSlowDown;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Orb")
        {
            maxVel++;
        }
    }
}
