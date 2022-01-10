using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ProjectileLauncher))]
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
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = GameManager.instance.playerInstance.transform.position - transform.position;
        pl.LaunchProjectileToward(direction);
    }
}
