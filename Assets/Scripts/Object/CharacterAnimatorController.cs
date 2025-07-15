using UnityEngine;

public class CharacterAnimatorController : MonoBehaviour
{
    CharacterMovementController charMovementController;
    SpriteRenderer charSpriteRenderer;

    public int charAnimationFrame { get; private set; }
    public Sprite[] charSprites;

    [SerializeField] private float pacmanAnimationTime = 0.25f;
    [SerializeField] private bool charAnimationRotation = true;
    private bool charAnimationLoop = true;

    void Awake()
    {
        charMovementController = GetComponentInParent<CharacterMovementController>();
        charSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        InvokeRepeating(nameof(CharPlayAnimation), pacmanAnimationTime, pacmanAnimationTime);
    }

    void Update()
    {
        if (charAnimationRotation)
        {
            CharUpdateDirection();
        }
    }

    private void CharPlayAnimation()
    {
        if (!charSpriteRenderer.enabled) return;

        charAnimationFrame++;

        if (charAnimationFrame >= charSprites.Length && charAnimationLoop)
            charAnimationFrame = 0;

        if (charAnimationFrame >= 0 && charAnimationFrame < charSprites.Length)
            charSpriteRenderer.sprite = charSprites[charAnimationFrame];
    }

    public void CharRestartAnimation()
    {
        charAnimationFrame = -1;
        CharPlayAnimation();
    }

    private void CharUpdateDirection()
    {
        Vector2 dir = charMovementController.charCurrentDirection;

        if (dir.x > 0) // sağ
        {
            charSpriteRenderer.flipX = false;
            charSpriteRenderer.transform.rotation = Quaternion.identity;
        }
        else if (dir.x < 0) // sol
        {
            charSpriteRenderer.flipX = true;
            charSpriteRenderer.transform.rotation = Quaternion.identity;
        }
        else if (dir.y > 0) // yukarı
        {
            charSpriteRenderer.flipX = false;
            charSpriteRenderer.transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if (dir.y < 0) // aşağı
        {
            charSpriteRenderer.flipX = false;
            charSpriteRenderer.transform.rotation = Quaternion.Euler(0, 0, -90);
        }
    }
    
}