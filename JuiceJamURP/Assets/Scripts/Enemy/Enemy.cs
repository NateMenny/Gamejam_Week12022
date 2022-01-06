using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
[RequireComponent(typeof(MainSounds))]
public class Enemy : MonoBehaviour
{
    public bool verbose = false;
    [SerializeField] protected int maxHealth;
    protected int _health;

    protected SpriteRenderer sr;
    protected Animator anim;
    protected MainSounds ms;
    public AudioClip dieClip;
    public AudioMixerGroup soundFXGroup;


    public int health
    {
        get { return _health; }
        set 
        { 
            _health = value;

            if (_health > maxHealth)
                _health = maxHealth;

            if (_health <= 0)
                Death();

        }
    }

    

    public virtual void TakeDamage(int damage)
    {
        health -= damage;
    }

    public void DestroyObj()
    {
        Destroy(gameObject);
    }

    public virtual void Start()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        ms = Camera.main.gameObject.GetComponent<MainSounds>();

        if (maxHealth <= 0)
            maxHealth = 10;

        health = maxHealth;
    }

    public virtual void Death()
    {
        if (dieClip)
        {
            ms.Play(dieClip, soundFXGroup);
            if (verbose)
                Debug.Log("Can be overriden in child classes to implement their own game over.");
        }
    }
}
