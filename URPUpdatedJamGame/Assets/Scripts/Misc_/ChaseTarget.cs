using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseTarget : MonoBehaviour
{
    public Transform target;
    public float travelSpeed;
    Rigidbody2D rb;
    [SerializeField] float chaseDistance;

    public Transform _target
    {
        get { return target; }
        set { _target = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (travelSpeed <= 0f) travelSpeed = 1f;
        if (chaseDistance <= 0f)
        {
            chaseDistance = 3f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ((target.position - transform.position).magnitude > chaseDistance)
        {
            Vector2 dirToTarget = (target.position - transform.position).normalized;
            rb.velocity = dirToTarget * (travelSpeed * Time.deltaTime);
        }
    }
}
