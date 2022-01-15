using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBulbObjective : MonoBehaviour
{
    Animator anim;

    bool isOn = false;

    public bool IsOn { get => isOn; }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnOn()
    {
        isOn = true;
        if (anim) anim.SetBool("isOn", true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if (!isOn)
            {
                GameManager.instance.currentLevel.BulbCollected();
            }
            TurnOn();   
        }
    }
}
