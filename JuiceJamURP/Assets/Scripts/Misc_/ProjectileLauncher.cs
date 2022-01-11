using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    [SerializeField] GameObject projectile;

    [Header("Sound Settings")]
    public AudioClip shootSFX;
    MainSounds ms;

    [Header("Launch Settings")]
    [SerializeField] Transform launchPosition;
    public float launchInterval;
    public float launchSpeed;
    float timeSinceLastLaunch;
    float timeCap;

    // Start is called before the first frame update
    void Start()
    {
        if (launchSpeed <= 0f) launchSpeed = 2f;
        ms = GetComponent<MainSounds>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeSinceLastLaunch <= launchInterval) timeSinceLastLaunch += Time.deltaTime;
    }

    // Spawns a projectile at launch position then shoots it toward direction at launchspeed
    // Direction is normalized
    public void LaunchProjectileToward(Vector3 direction)
    {
        if (timeSinceLastLaunch >= launchInterval)
        {
            if(shootSFX)
            ms.Play(shootSFX);

            Projectile launchedProj = Instantiate(projectile, launchPosition.position, transform.rotation).GetComponent<Projectile>();
            launchedProj.direction = direction.normalized;
            launchedProj.speed = launchSpeed;
            timeSinceLastLaunch = 0f;
        }
    }
}
