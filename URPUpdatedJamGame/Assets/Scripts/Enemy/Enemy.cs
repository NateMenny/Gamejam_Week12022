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
    [SerializeField] public int drops;
    protected int _health;

    protected SpriteRenderer sr;
    protected Animator anim;
    protected MainSounds ms;
    public AudioClip dieClip;
    public AudioMixerGroup soundFXGroup;

    public GameObject orbPrefab;


    public int health
    {
        get { return _health; }
        set 
        { 
            _health = value;

            if (_health > maxHealth)
                _health = maxHealth;

            if (_health <= 0)
                Death(drops);

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

        if (drops<=0)
        {
            drops = 1;
        }

        health = maxHealth;
    }

    public virtual void Death(int drops)
    {
        for (int i = 0; i < drops; i++)
        {
            Instantiate(orbPrefab, this.transform.position, this.transform.rotation);
        }
        if (dieClip)
        {
            ms.Play(dieClip, soundFXGroup);
            if (verbose)
                Debug.Log("Can be overriden in child classes to implement their own game over.");
        }
    }
}
