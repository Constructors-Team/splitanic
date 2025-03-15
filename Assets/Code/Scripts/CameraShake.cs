using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float shakeDuration = 0.2f;
    [SerializeField] private float shakeAmount = 0.2f;

    private Vector3 originalPosition;

    private void Awake()
    {
        originalPosition = transform.position;
    }

    public void Shake()
    {
        StartCoroutine(ShakeRoutine());
    }

    /*
     * Coroutine that applies a shaking effect by slightly displacing the camera.
     * It moves the camera randomly within a small range (`shakeMagnitude`).
     * After the duration (`shakeDuration`), it resets the camera back to its original position.
     */
    private IEnumerator ShakeRoutine()
    {
        float timeElapsed = 0f;

        while (timeElapsed < shakeDuration)
        {
            // Generate a small random offset in 3D space
            // The camera in Unity is always in a 3D space, even if it's a 2D game
            Vector3 randomOffset = Random.insideUnitSphere * shakeAmount;
            transform.position = originalPosition + randomOffset;
            
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPosition;
    }
}
