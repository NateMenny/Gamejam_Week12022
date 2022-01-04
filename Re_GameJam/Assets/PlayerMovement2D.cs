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
    
    // 
    Rigidbody2D rb2d;
    [SerializeField] float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        if (moveLeft == KeyCode.None)
        {
            moveLeft = KeyCode.A;
        }
        if (moveRight == KeyCode.None)
        {
            moveRight = KeyCode.D;
        }
        if (moveUp == KeyCode.None)
        {
            moveUp = KeyCode.W;
        }
        if (moveDown == KeyCode.None)
        {
            moveDown = KeyCode.S;
        }

        rb2d = GetComponent<Rigidbody2D>();
        moveSpeed = 2f;
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(moveRight))
        {
            rb2d.AddForce(Vector2.right * moveSpeed);
        }
    }
}
