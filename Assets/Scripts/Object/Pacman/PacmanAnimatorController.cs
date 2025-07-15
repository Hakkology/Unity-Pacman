using UnityEngine;

public class PacmanAnimatorController : MonoBehaviour
{
    PacmanMovementController pacmanMovementController;
    SpriteRenderer pacmanSpriteRenderer;

    public int pacmanAnimationFrame { get; private set; }
    public Sprite[] pacmanSprites;
    public Sprite[] pacmanEatenSprites;

    [SerializeField] private float pacmanAnimationTime = 0.25f;
    private bool pacmanAnimationLoop = true;
    void Awake()
    {
        pacmanMovementController = GetComponent<PacmanMovementController>();
        pacmanSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        InvokeRepeating(nameof(PlayAnimation), pacmanAnimationTime, pacmanAnimationTime);
    }

    void Update()
    {
        UpdatePacmanDirection();
    }

    private void PlayAnimation()
    {
        if (!pacmanSpriteRenderer.enabled) return;

        pacmanAnimationFrame++;

        if (pacmanAnimationFrame >= pacmanSprites.Length && pacmanAnimationLoop)
            pacmanAnimationFrame = 0;

        if (pacmanAnimationFrame >= 0 && pacmanAnimationFrame < pacmanSprites.Length)
            pacmanSpriteRenderer.sprite = pacmanSprites[pacmanAnimationFrame];
    }

    private void RestartAnimation()
    {
        pacmanAnimationFrame = -1;
        PlayAnimation();
    }

    private void UpdatePacmanDirection()
    {
        Vector2 dir = pacmanMovementController.pacmanCurrentDirection;

        if (dir.x > 0) // sağ
        {
            pacmanSpriteRenderer.flipX = false;
            pacmanSpriteRenderer.transform.rotation = Quaternion.identity;
        }
        else if (dir.x < 0) // sol
        {
            pacmanSpriteRenderer.flipX = true;
            pacmanSpriteRenderer.transform.rotation = Quaternion.identity;
        }
        else if (dir.y > 0) // yukarı
        {
            pacmanSpriteRenderer.flipX = false;
            pacmanSpriteRenderer.transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if (dir.y < 0) // aşağı
        {
            pacmanSpriteRenderer.flipX = false;
            pacmanSpriteRenderer.transform.rotation = Quaternion.Euler(0, 0, -90);
        }
    }
    
}