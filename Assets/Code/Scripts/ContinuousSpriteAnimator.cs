using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuousSpriteAnimator : MonoBehaviour
{
    [SerializeField] 
    private List<Sprite> sprites;
    [SerializeField]
    private float frameDuration = 0.1f;
    
    private SpriteRenderer spriteRenderer;
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(AnimateSprite());
    }

    IEnumerator AnimateSprite()
    {
        int currentSpriteIdx = 0;
        while (true)
        {
            spriteRenderer.sprite = sprites[currentSpriteIdx];
            currentSpriteIdx = (currentSpriteIdx + 1) % sprites.Count;
            yield return new WaitForSeconds(frameDuration);
        }   
    }
}
