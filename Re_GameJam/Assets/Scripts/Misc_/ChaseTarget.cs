using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseTarget : MonoBehaviour
{
    public Transform target;
    [SerializeField]float travelSpeed;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (travelSpeed <= 0f) travelSpeed = 1f; 
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 dirToTarget = (target.position - transform.position).normalized;
        rb.velocity = dirToTarget * (travelSpeed * Time.deltaTime);
        
    }
}
