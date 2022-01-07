using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ProjectileLauncher))]
public class PlayerShoot : MonoBehaviour
{
    ProjectileLauncher pl;
    // Start is called before the first frame update
    void Start()
    {
        pl = GetComponent<ProjectileLauncher>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            pl.LaunchProjectileToward(direction);
        }
    }
}
