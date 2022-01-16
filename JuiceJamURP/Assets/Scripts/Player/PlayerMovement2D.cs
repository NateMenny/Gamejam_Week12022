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

    delegate void Accelerate(Vector3 v);

    Accelerate accelStyle;
    public bool debugVelocityTimeScale;
    [SerializeField] float cameraZoomDelay;
    bool isMoving;

    [Header("Move Settings")]
    [Tooltip("Factor of max velocity")] public float accelFactor;
    public float maxVel;
    [SerializeField] MoveType moveType = MoveType.FORCE;

    Vector2 mousePos;
    Rigidbody2D rb2d;
    float axisX;
    float axisY;
    float timeSinceKeyRelease;

    public bool IsMoving { get => isMoving; }

    [SerializeField] float playerSlowDown;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        if (moveType == MoveType.FORCE) accelStyle = AccelerateUsingForce;
        else if (moveType == MoveType.VELOCITY) accelStyle = AccelerateUsingVelocity;
        
        if (maxVel <= 0f) maxVel = 25f;
        if (cameraZoomDelay <= 0f) cameraZoomDelay = 0.25f;
    }


    // Update is called once per frames
    void Update()
    {
        if (maxVel < 1f)
        {
            maxVel = 0f;
            accelFactor = 0f;
        }

        axisX = Input.GetAxisRaw("Horizontal");
        axisY = Input.GetAxisRaw("Vertical");

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float accelForce = maxVel / accelFactor;
        
        accelStyle(new Vector2(axisX, axisY).normalized);

        // If player isnt pressing any movement keys
        if (axisX == 0f && axisY == 0f)
        {
            if (timeSinceKeyRelease > cameraZoomDelay)
            {
                isMoving = false;
            }
           
            accelStyle(-rb2d.velocity.normalized);

            if (rb2d.velocity.magnitude < 0.2f) rb2d.velocity = Vector2.zero;
            timeSinceKeyRelease += Time.unscaledDeltaTime;
        }
        else
        {
            if (timeSinceKeyRelease > cameraZoomDelay)
            {
                isMoving = true;
            }
            timeSinceKeyRelease = 0f;
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
        return f + f / Time.timeScale;
    }

    void AccelerateUsingForce(Vector3 direction_)
    {
        rb2d.AddForce(direction_ * (maxVel / accelFactor) / Time.unscaledDeltaTime);
    }

    void AccelerateUsingVelocity(Vector3 direction_)
    {
        rb2d.velocity += (Vector2)direction_ * (accelFactor * Time.unscaledDeltaTime);
    }

     void FixedUpdate()
    {
        Vector2 lookDir = mousePos - rb2d.position;
        float angle = Mathf.Atan2(lookDir.y,lookDir.x) * Mathf.Rad2Deg - 90f;
        rb2d.rotation = angle;
    }
}
