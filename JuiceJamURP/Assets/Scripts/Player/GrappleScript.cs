using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class GrappleScript : MonoBehaviour 
{
    LineRenderer line;

    [SerializeField] GameObject grappleTip;
    [SerializeField] LayerMask grappleableMask;
    [SerializeField] float maxDistance = 10f;
    [SerializeField] float grapplePullForce;
    [SerializeField] float grappleShootSpeed = 20f;

    bool isGrappling = false;
    [HideInInspector] public bool retracting = false;

    Transform target;
    private void Start() 
    {
        if (grapplePullForce < 1f) grapplePullForce = 7f;
        line = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && !isGrappling)
        {
            StartGrapple();
        }

    }
   
    // Sets all grappling values to false
    void SetGrappling(bool deployed_)
    {
       // Debug.Log ("Stopping Grapple")
        isGrappling = deployed_;
        line.enabled = deployed_;
        grappleTip.GetComponent<SpriteRenderer>().enabled = deployed_;
    }

    // Initiates connection and raycast then runs grapple coroutine that pulls player
    private void StartGrapple() 
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, maxDistance, grappleableMask);
       

        if (hit.collider != null) 
        {
            // Debug.Log ("Starting Grapple");
            target = hit.collider.transform;
            SetGrappling(true);
            line.positionCount = 2;

            StartCoroutine(Grapple());
        }

        IEnumerator Grapple() 
        {
            float t = 0;
            float maxGrappleTime = 10;
            PlayerMovement2D player = GetComponent<PlayerMovement2D>();

            while (Input.GetMouseButton(1))
            {
                if (!target) break;
                

                // Add pull force on a player
                GetComponent<Rigidbody2D>().AddForce((target.position - transform.position).normalized * player.TimeFactoredFloat(grapplePullForce));

                // Gets the closest point to the player along the enemy collider 
                Vector2 grabSpot = target.GetComponent<Collider2D>().ClosestPoint(transform.position);

                // Set line renderer start and end positions
                line.SetPosition(0, transform.position);
                line.SetPosition(1, new Vector3(grabSpot.x, grabSpot.y, -0.1f));
                grappleTip.transform.position = line.GetPosition(1);
                yield return null;

                t += Time.deltaTime;
                if (t > maxGrappleTime)
                {
                    break;
                }
            }
            SetGrappling(false);
        }
    }
}

