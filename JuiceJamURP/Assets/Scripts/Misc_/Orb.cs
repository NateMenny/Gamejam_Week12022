using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ChaseTarget))]
public class Orb : Collectible
{
    ChaseTarget ct;

    void Start()
    {
        if(value <= 0f) value = 1f;
        ct = GetComponent<ChaseTarget>();
        ct.target = GameManager.instance.playerInstance.transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Collect(collision.gameObject);   
        }
    }

    public override void Collect(GameObject player_)
    {
        player_.GetComponent<PlayerMovement2D>().maxVel++;
        Destroy(gameObject);
    }
}
