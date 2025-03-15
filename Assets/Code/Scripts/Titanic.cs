using System.Collections;
using UnityEngine;

public class Titanic : MonoBehaviour
{
    [SerializeField] private AudioClip[] FunnyBoatCollisionSound;
    [SerializeField] private AudioClip[] FunnyIcebergCollisionSound;

    [SerializeField] private AudioClip BaseBoatCollisionSound;
    [SerializeField] private AudioClip BaseIcebergCollisionSound;

    [SerializeField] private AudioClip DieSound;

    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private int numberOfExplosions = 5;
    [SerializeField] private float explosionDuration = 3f; // Time window for explosions

    private AudioSource audioSource;

    [SerializeField] private float maxHealth;
    [SerializeField] private float nonDamagingSize = 0.1f;
    public float currentHealth;
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
        // maxHealth = calculateDamage(GameObject.FindFirstObjectByType<IcebergFactory>().maxIcebergSize) * 7;
        maxHealth = 1;
        currentHealth = maxHealth;
        cameraShake = Camera.main.GetComponent<CameraShake>();
    }

    private void PlayBoatCollisionSound()
    {
        if (audioSource != null && BaseBoatCollisionSound != null && FunnyBoatCollisionSound.Length != 0)
        {
            // Pick a random sound from the AudioClip array
            int randomIndex = Random.Range(0, FunnyBoatCollisionSound.Length);
            AudioClip chosenClip = FunnyBoatCollisionSound[randomIndex];

            audioSource.PlayOneShot(chosenClip);

            // Play the base sound at lower volume (e.g., 0.3f for 30% volume)
            audioSource.PlayOneShot(BaseBoatCollisionSound, 0.3f);
        }
    }

    private void PlayIcebergCollisionSound()
    {
        if (audioSource != null && BaseIcebergCollisionSound != null && FunnyIcebergCollisionSound.Length != 0)
        {
            // Pick a random sound from the AudioClip array
            int randomIndex = Random.Range(0, FunnyIcebergCollisionSound.Length);
            AudioClip chosenClip = FunnyIcebergCollisionSound[randomIndex];

            audioSource.PlayOneShot(chosenClip);

            // Play the base sound at lower volume (e.g., 0.3f for 30% volume)
            audioSource.PlayOneShot(BaseIcebergCollisionSound, 0.3f);
        }
    }

    public void TakeDamage(float damage)
    {
        PlayIcebergCollisionSound();

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
        if (other.CompareTag("Player") || other.CompareTag("IceBreaker"))
        {
            Debug.Log("[+] Player Boat collided with Titanic!");
            // Make a weird sound
            PlayBoatCollisionSound();
            return;
        }
        
        if (other.gameObject.CompareTag("Iceberg"))
        {
            float size = other.gameObject.transform.localScale.x;
            if (size > nonDamagingSize)
            {
                TakeDamage(calculateDamage(size));
            }
            else
            {
                Debug.Log("[+] Titanic is strong enough for this baby iceberg!!");
            }
            Destroy(other.gameObject);
        }
    }

    public void Die()
    {
        Debug.Log("[+] Titanic died, you loosed :(");
        
        if (audioSource != null && DieSound != null)
        {
            Debug.Log("[+] Play dieSound");
            audioSource.PlayOneShot(DieSound);
            StartCoroutine(SpawnExplosions());
            Destroy(gameObject, DieSound.length);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator SpawnExplosions()
    {
        float timeBetweenExplosions = explosionDuration / numberOfExplosions; // Evenly space out explosions

        for (int i = 0; i < numberOfExplosions; i++)
        {
            Debug.Log("[+] Titanic is exploding!");
            // Randomize the position around the Titanic
            Vector3 randomOffset = new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), 0f);
            float randomScale = Random.Range(0.5f, 1.5f);

            // Instantiate the explosion
            GameObject explosion = Instantiate(explosionPrefab, transform.position + randomOffset, Quaternion.identity);
            explosion.transform.localScale = new Vector3(randomScale, randomScale, 1f);

            // Wait for the next explosion
            yield return new WaitForSeconds(timeBetweenExplosions);
        }
    }

    private float calculateDamage(float size) {
        return Mathf.Pow(size * 100, 2);
    }
}