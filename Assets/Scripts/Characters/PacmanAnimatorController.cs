using UnityEngine;

public class PacmanAnimatorController : MonoBehaviour
{
    public SpriteRenderer spriteRenderer { get; private set; }
    public int pacmanAnimationFrame { get; private set; }
    public Sprite[] pacmanSprites;
    public Sprite[] pacmanEatenSprites;

    private float pacmanAnimationTime = 0.25f;
    private bool pacmanAnimationLoop = true;
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        InvokeRepeating(nameof(PlayAnimation), pacmanAnimationTime, pacmanAnimationTime);
    }

    private void PlayAnimation()
    {
        if (!spriteRenderer.enabled) return;

        pacmanAnimationFrame++;

        if (pacmanAnimationFrame >= pacmanSprites.Length && pacmanAnimationLoop)
            pacmanAnimationFrame = 0;

        // if (pacmanAnimationFrame >= 0 && pacmanAnimationFrame < pacmanSprites.Length)
        // {
        spriteRenderer.sprite = pacmanSprites[pacmanAnimationFrame];
        // }
        
    }


    
}