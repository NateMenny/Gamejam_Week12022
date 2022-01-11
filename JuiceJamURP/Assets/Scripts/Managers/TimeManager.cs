using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.playerInstance)
        {
            Rigidbody2D rb = GameManager.instance.playerInstance.GetComponent<Rigidbody2D>();
            float playerMaxVel = GameManager.instance.playerInstance.GetComponent<PlayerMovement2D>().maxVel;

            if (playerMaxVel >= 1f)
            {
                if (rb.velocity.magnitude / playerMaxVel > 0.97f)
                {

                    Time.timeScale = 0.5f;

                }
                else
                {
                    Time.timeScale = 1f - (rb.velocity.magnitude / playerMaxVel) / 2;
                }
                Time.fixedDeltaTime = Time.timeScale * .02f;
            }
            else
            {
                Time.timeScale = 1f;
            }
        }
        
    }
}
