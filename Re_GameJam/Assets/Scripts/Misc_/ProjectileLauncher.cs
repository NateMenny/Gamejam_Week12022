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
        
    }

    // Update is called once per frame
    void Update()
    {
      
            if (timeSinceLastLaunch > launchInterval)
            {
                timeSinceLastLaunch = 0f;
                LaunchProjectile();
            }
       
        timeSinceLastLaunch += Time.deltaTime;
    }

    void LaunchProjectile()
    {
        Projectile launchedProj = Instantiate(projectile, launchPosition.position, transform.rotation).GetComponent<Projectile>();
        //proj.transform.position = launchPosition.position;
        Vector2 dirToPlayer = (GameManager.instance.playerInstance.transform.position - transform.position).normalized;
        launchedProj.direction = dirToPlayer;
        launchedProj.speed = launchSpeed;
    }
}
