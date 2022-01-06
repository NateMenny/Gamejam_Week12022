using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement2D : MonoBehaviour
{
    // Movement Keys
    public KeyCode moveLeft;
    public KeyCode moveRight;
    public KeyCode moveUp;
    public KeyCode moveDown;

    // Movement checks
    bool isMoving;
   
    Rigidbody2D rb2d;
    [SerializeField] float accel;
    public float startMaxVel = 12f;
    public float maxVel = 12f;
    float axisX;
    float axisY;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        if(accel <= 0f) accel = 4f;
        if (maxVel <= 0f) maxVel = 50f;
        rb2d.drag = accel * 0.25f;
    }

    // Update is called once per frames
    void Update()
    {
        
        if(rb2d.velocity.magnitude / maxVel > 1)
        {
            Time.timeScale = 0.5f;
            Time.fixedDeltaTime = Time.timeScale * .02f;
        }
        else
        {
            Time.timeScale = 1f - (rb2d.velocity.magnitude / maxVel) /2;
            Time.fixedDeltaTime = Time.timeScale * .02f;
        }

        axisX = Input.GetAxisRaw("Horizontal");
        axisY = Input.GetAxisRaw("Vertical");

        // we need something to zero the movement when no movement keys are down
        rb2d.AddForce(new Vector2(axisX, axisY).normalized * maxVel/ 5 * (1/Time.timeScale));
        if(rb2d.velocity.magnitude > maxVel)
        {
            rb2d.velocity = rb2d.velocity.normalized* maxVel;
        }
        Debug.Log("Velocity: " + rb2d.velocity.magnitude + "Timescale: " + Time.timeScale);
    }
}
