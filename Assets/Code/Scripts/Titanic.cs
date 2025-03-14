using System;
using Unity.VisualScripting;
using UnityEngine;

public class Titanic : MonoBehaviour
{
    [SerializeField] private int maxHealth = 1000;
    private int currentHealth;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;

    }

    public void takeDamage(int damage)
    {
        currentHealth -= damage;

        Debug.Log("[+] Titanic is hurt! -" + damage);
        
        if (currentHealth <= 0)
        {
            Debug.Log("[+] Titanic is sinking!");
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("[+] Titanic died, you loosed :(");
        Destroy(gameObject);
    }
    
}
