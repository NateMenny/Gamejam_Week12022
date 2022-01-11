using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ProjectileLauncher), typeof(MainSounds))]
public class EnemyStationaryShooter : Enemy
{
    public AudioClip shootSFX;
    ProjectileLauncher pl;

    // Start is called before the first frame update
    public override void Death(int drops)
    {
        base.Death(drops);
        base.DestroyObj(0f);
    }

    public override void Start()
    {
        pl = GetComponent<ProjectileLauncher>();
        ms = GetComponent<MainSounds>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = GameManager.instance.playerInstance.transform.position - transform.position;
        Shoot(direction);
    }

    void Shoot(Vector2 direction)
    {
        pl.LaunchProjectileToward(direction);
        ms.Play(shootSFX);
    }
}
