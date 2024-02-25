using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
   
    [SerializeField] private GameOverM gameOverManager;
    [Header("Health")]
    [SerializeField] private float startingHealth; 
    public float currentHealth { get; private set; } 
    private bool dead = false; 
    private Animator anim; 

 
    [Header("iFrames")]
    [SerializeField] private float iFrameDuration;
    [SerializeField] private int numberOfFlashes; 
    private SpriteRenderer spriteRend; 

    private AudioSource audioSource; 
    [SerializeField] private AudioClip hurtSound; 
    [SerializeField] private AudioClip dieSound;
    
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        currentHealth = startingHealth; 
        anim = GetComponent<Animator>(); 
        spriteRend = GetComponent<SpriteRenderer>(); 
    }

    
    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(1);
        }
    }

    
    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth); 
        if (currentHealth > 0)
        {
            StartCoroutine(Invunerability()); 
            anim.SetTrigger("hurt"); 
            audioSource.PlayOneShot(hurtSound);
        }
        else
        {
            if (!dead)
            {
                anim.SetTrigger("die"); 
                GetComponent<PlayerController>().enabled = false; 
                dead = true; 
                audioSource.PlayOneShot(dieSound);
                gameOverManager.ShowGameOverCanva();
            }
        }
    }

    
    public void AddHealth(float value)
    {
        currentHealth = Mathf.Clamp(currentHealth + value, 0, startingHealth); 
    }

    private IEnumerator Invunerability()
    {
        Physics2D.IgnoreLayerCollision(10, 11, true);       
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f); 
            yield return new WaitForSeconds(iFrameDuration / (numberOfFlashes * 2)); 
            spriteRend.color = Color.white; 
            yield return new WaitForSeconds(iFrameDuration / (numberOfFlashes * 2)); 
        }

        Physics2D.IgnoreLayerCollision(10, 11, false); 
    }
}