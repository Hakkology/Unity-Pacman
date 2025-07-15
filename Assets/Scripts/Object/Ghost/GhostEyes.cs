using UnityEngine;

public class GhostEyes : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public CharacterMovementController movementController;
    public Sprite eyesUp;
    public Sprite eyesDown;
    public Sprite eyesRight;
    public Sprite eyesLeft;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        movementController = GetComponentInParent<CharacterMovementController>();
    }

    void Update()
    {
        Vector2 dir = movementController.charCurrentDirection;

        if (dir == Vector2.right)
            spriteRenderer.sprite = eyesRight;
        else if (dir == Vector2.left)
            spriteRenderer.sprite = eyesLeft;
        else if (dir == Vector2.up)
            spriteRenderer.sprite = eyesUp;
        else if (dir == Vector2.down)
            spriteRenderer.sprite = eyesDown;
    }
}