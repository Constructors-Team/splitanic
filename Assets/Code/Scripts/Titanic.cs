using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Titanic : MonoBehaviour
{
    [SerializeField] private AudioClip BoatKlaxonSound;
    
    [SerializeField] private AudioClip[] FunnyBoatCollisionSound;
    [SerializeField] private AudioClip[] FunnyIcebergCollisionSound;

    [SerializeField] private AudioClip BaseBoatCollisionSound;
    [SerializeField] private AudioClip BaseIcebergCollisionSound;

    [SerializeField] private AudioClip DieSound;

    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private int numberOfExplosions = 5;
    [SerializeField] private float explosionDuration = 2f; // Time window for explosions

    private AudioSource audioSource;

    public float maxHealth;
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
        maxHealth = calculateDamage(GameObject.FindFirstObjectByType<IcebergFactory>().maxIcebergSize) * 7;
        currentHealth = maxHealth;
        cameraShake = Camera.main.GetComponent<CameraShake>();
        PlayBoatKlaxonSound();
    }

    private void PlayBoatKlaxonSound()
    {
        if (audioSource != null && BoatKlaxonSound != null)
        {
            audioSource.PlayOneShot(BoatKlaxonSound);
        }
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
            StartCoroutine(DelayedEndUiDisplay(DieSound.length + 1.5f)); // Switch scene after sound ends + seconds
        }
    }
    
    private IEnumerator DelayedEndUiDisplay(float delay)
    {
        yield return new WaitForSeconds(delay); 
        EndScoreTextManager.DisplayEndScoreText();
    }

    private IEnumerator SpawnExplosions()
    {
        // Store initial rotation and calculate target
        Quaternion initialRotation = transform.rotation;
        Quaternion
            targetRotation = initialRotation * Quaternion.Euler(0, 0, 45f); // 45 degrees left from the initial rotation

        // Timing values
        float totalRotationTime = explosionDuration + 3f; // Rotation continues 3s after explosions end
        float rotationSpeed = 45f / totalRotationTime; // Degrees per second

        // Explosion timing
        float timeBetweenExplosions = explosionDuration / numberOfExplosions;
        float explosionTimer = 0f;
        int explosionsSpawned = 0;

        // Rotation progress tracking
        float startTime = Time.time;

        // Run until rotation completes AND all explosions are spawned
        while (transform.rotation != targetRotation || explosionsSpawned < numberOfExplosions)
        {
            float deltaTime = Time.deltaTime;

            // Rotate the Titanic smoothly toward the target
            if (transform.rotation != targetRotation)
            {
                transform.rotation = Quaternion.RotateTowards(
                    transform.rotation,
                    targetRotation,
                    rotationSpeed * deltaTime // Degrees to rotate this frame
                );
            }

            // Spawn explosions only during the initial explosionDuration window
            if (Time.time - startTime < explosionDuration && explosionsSpawned < numberOfExplosions)
            {
                explosionTimer += deltaTime;
                if (explosionTimer >= timeBetweenExplosions)
                {
                    // Spawn explosion
                    Vector3 randomOffset = new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), 0f);
                    float randomScale = Random.Range(0.5f, 1.5f);
                    GameObject explosion = Instantiate(explosionPrefab, transform.position + randomOffset,
                        Quaternion.identity);
                    explosion.transform.localScale = new Vector3(randomScale, randomScale, 1f);

                    explosionsSpawned++;
                    explosionTimer = 0f;
                    Debug.Log("[+] Titanic is exploding!");
                }
            }

            yield return null;
        }

        // Final cleanup (redundant but safe)
        transform.rotation = targetRotation;
    }

    private float calculateDamage(float size) {
        return Mathf.Pow(size * 100, 2);
    }
}