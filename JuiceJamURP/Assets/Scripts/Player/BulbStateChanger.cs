using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class BulbStateChanger : MonoBehaviour
{
    enum GLOW_STATE
    {
        DIM = 0,
        LOW,
        MED,
        HIGH,
        MAX
    }

    float changePercentage; // Its in decimal form - 1% = 0.01
    Animator anim;
    Rigidbody2D rb;
    PlayerMovement2D pm;

    // Start is called before the first frame update
    void Start()
    {
        changePercentage = 1f / 5f; // 1/number of glow levels
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        pm = GetComponent<PlayerMovement2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float playerVelocity = rb.velocity.magnitude;
        if (anim)
        {
            if (playerVelocity <= changePercentage * pm.maxVel)
            {
                anim.SetInteger("glowState", (int)GLOW_STATE.DIM);
                // Inside the ifs is where to place light radius changes
            }
            else if (playerVelocity <= changePercentage * 2 * pm.maxVel)
            {
                anim.SetInteger("glowState", (int)GLOW_STATE.LOW);
            }
            else if (playerVelocity <= changePercentage * 3 * pm.maxVel)
            {
                anim.SetInteger("glowState", (int)GLOW_STATE.MED);
            }
            else if (playerVelocity <= changePercentage * 4 * pm.maxVel)
            {
                anim.SetInteger("glowState", (int)GLOW_STATE.HIGH);
            }
            else
            {
                anim.SetInteger("glowState", (int)GLOW_STATE.MAX);
            }
        }
        else Debug.Log("The " + name + " does not have an animator component.");
    }
}
