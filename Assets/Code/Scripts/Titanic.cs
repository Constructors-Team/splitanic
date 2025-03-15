using System;
using Unity.VisualScripting;
using UnityEngine;

public class Titanic : MonoBehaviour
{
    [SerializeField] private AudioClip boatCollisionSound;
    [SerializeField] private AudioClip icebergCollisionSound;

    private AudioSource audioSource;
    
    [SerializeField] private int maxHealth = 1000;
    private int currentHealth;
    private Flash flash;

    private CameraShake cameraShake;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        
        flash = GetComponent<Flash>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        cameraShake = Camera.main.GetComponent<CameraShake>();

    }

    private void PlayCollisionSound()
    {
        if (audioSource != null && boatCollisionSound != null)
        {
            audioSource.PlayOneShot(boatCollisionSound);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        Debug.Log("[+] Titanic is hurt! -" + damage);
        
        cameraShake.Shake();
        StartCoroutine(flash.FlashRoutine());
        
        if (currentHealth <= 0)
        {
            Debug.Log("[+] Titanic is sinking!");
            Die();
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerBoat"))
        {
            Debug.Log("[+] Player Boat collided with Titanic!");
            // Make a weird sound
            PlayCollisionSound();
            return;
        }
        TakeDamage(100);
    }

    public void Die()
    {
        Debug.Log("[+] Titanic died, you loosed :(");
        Destroy(gameObject);
    }
    
}
