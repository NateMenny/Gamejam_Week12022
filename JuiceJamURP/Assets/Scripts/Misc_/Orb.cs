using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ChaseTarget))]
public class Orb : MonoBehaviour
{
    ChaseTarget ct;

    void Start()
    {
        ct = GetComponent<ChaseTarget>();
        ct.target = GameManager.instance.playerInstance.transform;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //destroy itself
            Destroy(gameObject);
        }
    }
}
