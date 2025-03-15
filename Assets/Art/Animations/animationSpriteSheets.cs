using UnityEngine;

public class AnimationSpriteSheets : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite[] frames;
    
    [Tooltip("Si activé, l'animation se répète en boucle. Si désactivé, l'objet sera détruit après la dernière frame.")]
    public bool loop = true;
    
    [Tooltip("Durée commune pour toutes les frames")]
    public float frameDuration = 0.1f;
    
    private int currentFrame = 0;
    private float timer = 0;
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        // Vérification que nous avons au moins une frame
        if (frames == null || frames.Length == 0)
        {
            Debug.LogError("Aucune frame n'a été attribuée à l'animation");
            enabled = false;
            return;
        }
        
        // Afficher la première frame
        spriteRenderer.sprite = frames[0];
    }
    
    void Update()
    {
        timer += Time.deltaTime;
        
        if (timer >= frameDuration)
        {
            timer = 0;
            currentFrame++;
            
            // Vérifier si on a atteint la fin de l'animation
            if (currentFrame >= frames.Length)
            {
                if (loop)
                {
                    // Recommencer l'animation depuis le début
                    currentFrame = 0;
                }
                else
                {
                    // Détruire l'objet car l'animation est terminée
                    Destroy(gameObject);
                    return;
                }
            }
            
            // Afficher la frame actuelle
            spriteRenderer.sprite = frames[currentFrame];
        }
    }
}