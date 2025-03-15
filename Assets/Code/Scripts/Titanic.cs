using System;
using Unity.VisualScripting;
using UnityEngine;

public class Titanic : MonoBehaviour
{
    [SerializeField] private int maxHealth = 1000;
    private int currentHealth;
    private Flash flash;

    private CameraShake cameraShake;

    private void Awake()
    {
        flash = GetComponent<Flash>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        cameraShake = Camera.main.GetComponent<CameraShake>();

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
        TakeDamage(100);
        Destroy(other.gameObject);
    }

    public void Die()
    {
        Debug.Log("[+] Titanic died, you loosed :(");
        Destroy(gameObject);
    }
    
}
