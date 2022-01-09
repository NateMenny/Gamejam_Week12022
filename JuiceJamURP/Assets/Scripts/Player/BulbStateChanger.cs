using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Experimental.Rendering.Universal;

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
    [SerializeField] Light2D[] playerLights;

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
                playerLights[0].pointLightOuterRadius = 0.5f;
                playerLights[1].pointLightOuterRadius = 1f;
                playerLights[2].pointLightOuterRadius = 1.5f;
            }
            else if (playerVelocity <= changePercentage * 2 * pm.maxVel)
            {
                anim.SetInteger("glowState", (int)GLOW_STATE.LOW);
                playerLights[0].pointLightOuterRadius = 2f;
                playerLights[1].pointLightOuterRadius = 2.3f;
                playerLights[2].pointLightOuterRadius = 2.5f;
            }
            else if (playerVelocity <= changePercentage * 3 * pm.maxVel)
            {
                anim.SetInteger("glowState", (int)GLOW_STATE.MED);
                playerLights[0].pointLightOuterRadius = 3f;
                playerLights[1].pointLightOuterRadius = 4f;
                playerLights[2].pointLightOuterRadius = 7f;
            }
            else if (playerVelocity <= changePercentage * 4 * pm.maxVel)
            {
                anim.SetInteger("glowState", (int)GLOW_STATE.HIGH);
                playerLights[0].pointLightOuterRadius = 7f;
                playerLights[1].pointLightOuterRadius = 6;
                playerLights[2].pointLightOuterRadius = 8f;
            }
            else
            {
                anim.SetInteger("glowState", (int)GLOW_STATE.MAX);
                playerLights[0].pointLightOuterRadius = 8f;
                playerLights[1].pointLightOuterRadius = 7f;
                playerLights[2].pointLightOuterRadius = 9f;
            }
        }
        else Debug.Log("The " + name + " does not have an animator component.");
    }
}
