using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class GrappleScript : MonoBehaviour {
LineRenderer line;

[SerializeField] LayerMask grappleableMask;
[SerializeField] float maxDistance = 10f;
[SerializeField] float grapplePullForce;
[SerializeField] float grappleShootSpeed = 20f;

bool isGrappling = false;
[HideInInspector] public bool retracting = false;

Transform target;
private void Start() {
        if (grapplePullForce < 1f) grapplePullForce = 7f;
    line = GetComponent<LineRenderer>();
}

    private void Update() {
        if (Input.GetMouseButtonDown(0) && !isGrappling) {
            StartGrapple();
        }
    }
   
    // Sets all grappling values to false
    void StopGrapple()
    {
        isGrappling = false;
        line.enabled = false;
    }

    // Initiates connection and raycast then runs grapple coroutine that pulls player
    private void StartGrapple() 
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, maxDistance, grappleableMask);
       

        if (hit.collider != null) 
        {
            isGrappling = true;
            target = hit.collider.transform;
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
                // Add pull force on a player
                GetComponent<Rigidbody2D>().AddForce((target.position - transform.position).normalized * grapplePullForce);

                // Gets the closest point to the player along the enemy collider 
                Vector2 grabSpot = target.GetComponent<Collider2D>().ClosestPoint(transform.position);

                // Set line renderer start and end positions
                line.SetPosition(0, transform.position);
                line.SetPosition(1, new Vector3(grabSpot.x, grabSpot.y, -0.1f));
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

