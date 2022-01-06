using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    [SerializeField] GameObject projectile;

    [Header("Launch Settings")]
    [SerializeField] Transform launchPosition;
    public float launchInterval;
    public float launchSpeed;
    float timeSinceLastLaunch;
    // Start is called before the first frame update
    void Start()
    {
        if (launchSpeed <= 0f) launchSpeed = 2f;
        
    }

    // Update is called once per frame
    void Update()
    {
      
        if (timeSinceLastLaunch > launchInterval)
        {
            timeSinceLastLaunch = 0f;
            Vector2 dirToPlayer = (GameManager.instance.playerInstance.transform.position - transform.position).normalized;
            LaunchProjectileToward(dirToPlayer);
        }
       
        timeSinceLastLaunch += Time.deltaTime;
    }

    // Spawns a projectile at launch position then shoots it toward direction at launchspeed
    void LaunchProjectileToward(Vector3 direction)
    {
        Projectile launchedProj = Instantiate(projectile, launchPosition.position, transform.rotation).GetComponent<Projectile>();
        launchedProj.direction = direction;
        launchedProj.speed = launchSpeed;
    }
}
