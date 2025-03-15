using System.Collections;
using UnityEngine;

public abstract class BaseIceberg : MonoBehaviour
{
    protected abstract AudioSource AudioSource { get; }
    protected abstract AudioClip AudioClip { get; }
    
    protected IcebergFactory factory; // Reference to IcebergFactory
    private bool canCollide = false; // Initially prevent collisions
    private Collider2D icebergCollider;

    void Start()
    {
        factory = FindObjectOfType<IcebergFactory>(); // Find the IcebergFactory
        icebergCollider = GetComponent<Collider2D>();

        if (icebergCollider != null)
        {
            icebergCollider.enabled = false; // Disable collisions at start
        }

        StartCoroutine(EnableCollisionAfterDelay(factory.initialCollisionDelay));
    }

    private IEnumerator EnableCollisionAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        canCollide = true; // Allow collisions
        if (icebergCollider != null)
        {
            icebergCollider.enabled = true; // Enable the collider
        }
    }

    public virtual void SplitIceberg()
    {
        if (!canCollide || factory == null) return;

        float minSizeForSplitting = factory.minSizeForSplitting;
        float sizeReductionFactor = factory.sizeReductionFactor;
        float icebergOffsetX = factory.icebergOffsetXForSplit;

        if (transform.localScale.x < minSizeForSplitting)
        {
            return; // No further splitting
        }
        
        // Play the split sound
        if (AudioSource != null && AudioClip != null)
        {
            // I'm rushing, but this is a quick way to play a sound without having to worry about the AudioSource being
            // destroyed when the iceberg object is destroyed
            GameObject tempAudio = new GameObject("TempAudio");
            AudioSource tempSource = tempAudio.AddComponent<AudioSource>();
            tempSource.clip = AudioClip;
            tempSource.volume = AudioSource.volume;
            tempSource.spatialBlend = AudioSource.spatialBlend; 
            tempSource.Play();
            Destroy(tempAudio, AudioClip.length); // Destroy temp object after sound finishes
        }

        float newSize = transform.localScale.x * sizeReductionFactor;
        transform.localScale = new Vector3(newSize, newSize, 1f);

        Vector3 newPosition = transform.position + new Vector3(icebergOffsetX, 0f, 0f);
        factory.SpawnIceberg(newPosition, newSize);
        factory.SpawnIceberg(newPosition, newSize);
        Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (canCollide && other.gameObject.CompareTag("IceBreaker"))
        {
            SplitIceberg();
        }
    }
}
