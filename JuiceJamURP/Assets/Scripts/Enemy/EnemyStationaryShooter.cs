using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ProjectileLauncher), typeof(MainSounds))]
public class EnemyStationaryShooter : Enemy
{
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
        if (hasAttackRadius)
        {
            if ((GameManager.instance.playerInstance.transform.position - transform.position).magnitude < attackRadius)
            {
                Vector2 direction = GameManager.instance.playerInstance.transform.position - transform.position;
                Shoot(direction);
            }
        }
        else
        {
            Vector2 direction = GameManager.instance.playerInstance.transform.position - transform.position;
            Shoot(direction);
        }
    }

    void Shoot(Vector2 direction)
    {
        pl.LaunchProjectileToward(direction);
    }
}
