using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Player : MonoBehaviour
{
    [SerializeField] PlayerMovement2D pm;
    [SerializeField] Light2D ppl; // player point light
    
    // Start is called before the first frame update
    void Start()
    {
        pm = GetComponent<PlayerMovement2D>();
        ppl = GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        ppl.pointLightOuterRadius = pm.maxVel / 4;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("PlayerProjectile") && !collision.gameObject.CompareTag("Bulb"))
        {
            Camera.main.GetComponent<CameraEffects>().Shake();
        }
    }
}
