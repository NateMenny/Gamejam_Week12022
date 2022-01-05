using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleScript : MonoBehaviour {
LineRenderer line;

[SerializeField] LayerMask grappleableMask;
[SerializeField] float maxDistance = 10f;
[SerializeField] float grapplePullForce = 7f;
[SerializeField] float grappleShootSpeed = 20f;

bool isGrappling = false;
[HideInInspector] public bool retracting = false;

Vector2 target;
private void Start() {
    line = GetComponent<LineRenderer>();
}

    private void FixedUpdate() {
        if (Input.GetMouseButtonDown(0) && !isGrappling) {
            StartGrapple();
        }
    }
   
    void StopGrapple()
    {
        isGrappling = false;
        line.enabled = false;
    }
    private void StartGrapple() 
    {
        
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, maxDistance, grappleableMask);

        if (hit.collider != null) 
        {
            isGrappling = true;
            target = hit.point;
            line.enabled = true;
            line.positionCount = 2;

            StartCoroutine(Grapple());
        }

        IEnumerator Grapple() 
        {
            float t = 0;
            float maxGrappleTime = 10;

            // DONT TOUCH THIS COMMENT
            /*
            float timeCount = 0f;
            float grabTime = 1f;

            Vector2 newPos;
            while (timeCount < grabTime)
            {
                newPos = Vector2.Lerp(transform.position, target, timeCount);
                line.SetPosition(0, transform.position);
                line.SetPosition(1, newPos);
                timeCount += Time.deltaTime;
            }
            */
            
            while (Input.GetMouseButton(0))
            {
                GetComponent<Rigidbody2D>().AddForce((target - new Vector2(transform.position.x, transform.position.y)).normalized * grapplePullForce);
                line.SetPosition(0, transform.position);
                line.SetPosition(1, target);
                yield return null;
                t += Time.deltaTime;
                if (t > maxGrappleTime)
                {
                    break;
                }
            }
            StopGrapple();
        }
    }
}

